using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.Data.Enums;
using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.Helpers;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text.Json;

namespace dotnet_api.Services
{
    public class PayrollAdjustmentService : IPayrollAdjustmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly IApprovalHistoryService _approvalHistoryService;
        private const string UPLOAD_DIRECTORY = "uploads/blueprints";

        public PayrollAdjustmentService(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment environment, IApprovalHistoryService approvalHistoryService)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
            _approvalHistoryService = approvalHistoryService;
        }


        public async Task<IEnumerable<PayrollAdjustmentDTO>> GetAllPayrollAdjustmentsAsync()
        {
            var PayrollAdjustments = await _context.PayrollAdjustments
                .Include(pa => pa.AdjustmentType)
                .Include(pa => pa.AdjustmentItem)
                .Include(pa => pa.applicationUser_PayrollAdjustment)
                    .ThenInclude(ua => ua.applicationUser)
                .OrderBy(pa => pa.ApproveStatus) // Sắp xếp theo trạng thái: 0 (Tạo mới) → 1 (Chờ duyệt) → 2 (Đã duyệt) → 3 (Từ chối)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PayrollAdjustmentDTO>>(PayrollAdjustments);
        }

        public async Task<PayrollAdjustmentDTO> GetPayrollAdjustmentByIdAsync(string VoucherCode)
        {
            var payrollAdjustment = await _context.PayrollAdjustments
                .Include(pa => pa.AdjustmentType)
                .Include(pa => pa.AdjustmentItem)
                .Include(pa => pa.applicationUser_PayrollAdjustment)
                    .ThenInclude(ua => ua.applicationUser)
                .FirstOrDefaultAsync(x => x.voucherNo == VoucherCode);
            
            if (payrollAdjustment == null) return null;
            return _mapper.Map<PayrollAdjustmentDTO>(payrollAdjustment);
        }

        public async Task<PayrollAdjustmentDTO> CreatePayrollAdjustmentAsync(PayrollAdjustmentDTO PayrollAdjustmentDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Tạo PayrollAdjustment
                var entity = new PayrollAdjustment
                {
                    voucherNo = PayrollAdjustmentDTO.voucherNo,
                    decisionDate = PayrollAdjustmentDTO.decisionDate.Date, // Chỉ lấy phần ngày, set thời gian về 00:00:00
                    Month = PayrollAdjustmentDTO.Month,
                    Year = PayrollAdjustmentDTO.Year,
                    Reason = PayrollAdjustmentDTO.Reason,
                    AdjustmentTypeID = PayrollAdjustmentDTO.AdjustmentTypeID,
                    AdjustmentItemID = PayrollAdjustmentDTO.AdjustmentItemID > 0 ? PayrollAdjustmentDTO.AdjustmentItemID : null,
                    ApproveStatus = ApproveStatusEnum.Created
                };

                _context.PayrollAdjustments.Add(entity);

                // Tạo các ApplicationUser_PayrollAdjustment
                if (PayrollAdjustmentDTO.Employees != null && PayrollAdjustmentDTO.Employees.Any())
                {
                    foreach (var employee in PayrollAdjustmentDTO.Employees)
                    {
                        var userAdjustment = new ApplicationUser_PayrollAdjustment
                        {
                            PayrollAdjustmentID = entity.voucherNo,
                            EmployeeID = employee.EmployeeID,
                            Value = employee.Value
                        };
                        _context.ApplicationUser_PayrollAdjustments.Add(userAdjustment);
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return await GetPayrollAdjustmentByIdAsync(entity.voucherNo);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<PayrollAdjustmentDTO> UpdatePayrollAdjustmentAsync(PayrollAdjustmentDTO dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var entity = await _context.PayrollAdjustments
                    .Include(p => p.applicationUser_PayrollAdjustment)
                    .FirstOrDefaultAsync(x => x.voucherNo == dto.voucherNo);
                
                if (entity == null) return null;

                // Cập nhật thông tin PayrollAdjustment
                entity.decisionDate = dto.decisionDate.Date; // Chỉ lấy phần ngày, set thời gian về 00:00:00
                entity.Month = dto.Month;
                entity.Year = dto.Year;
                entity.Reason = dto.Reason;
                entity.AdjustmentTypeID = dto.AdjustmentTypeID;
                entity.AdjustmentItemID = dto.AdjustmentItemID > 0 ? dto.AdjustmentItemID : null;

                // Xóa các ApplicationUser_PayrollAdjustment cũ
                _context.ApplicationUser_PayrollAdjustments.RemoveRange(entity.applicationUser_PayrollAdjustment);

                // Thêm các ApplicationUser_PayrollAdjustment mới
                if (dto.Employees != null && dto.Employees.Any())
                {
                    foreach (var employee in dto.Employees)
                    {
                        var userAdjustment = new ApplicationUser_PayrollAdjustment
                        {
                            PayrollAdjustmentID = entity.voucherNo,
                            EmployeeID = employee.EmployeeID,
                            Value = employee.Value
                        };
                        _context.ApplicationUser_PayrollAdjustments.Add(userAdjustment);
                    }
                }

                _context.PayrollAdjustments.Update(entity);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return await GetPayrollAdjustmentByIdAsync(entity.voucherNo);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<PayrollAdjustmentDTO> DeletePayrollAdjustmentAsync(string voucherCode)
        {
            var entity = await _context.PayrollAdjustments.FirstOrDefaultAsync(x => x.voucherNo == voucherCode);
            if (entity == null) return null;

            _context.PayrollAdjustments.Remove(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<PayrollAdjustmentDTO>(entity);
        }

        // Approval workflow methods
        public async Task<PayrollAdjustmentDTO> SubmitPayrollAdjustmentForApprovalAsync(string voucherNo, string submitterId, string? notes)
        {
            var entity = await _context.PayrollAdjustments
                .FirstOrDefaultAsync(x => x.voucherNo == voucherNo);

            if (entity == null) throw new Exception("Không tìm thấy khoản cộng/trừ");
            if (entity.ApproveStatus != ApproveStatusEnum.Created) throw new Exception("Khoản cộng/trừ đã được gửi duyệt hoặc đã xử lý");

            var submitter = await _context.ApplicationUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == submitterId);
            if (submitter == null) throw new Exception("Không tìm thấy người gửi");

            var oldStatus = entity.ApproveStatus;
            entity.ApproveStatus = ApproveStatusEnum.Pending;

            await _context.SaveChangesAsync();

            // Create approval history
            await _approvalHistoryService.CreateHistoryAsync(
                "PayrollAdjustment",
                voucherNo,
                submitterId,
                $"{submitter.FirstName} {submitter.LastName}",
                "Submit",
                oldStatus,
                ApproveStatusEnum.Pending,
                notes
            );

            return await GetPayrollAdjustmentByIdAsync(voucherNo);
        }

        public async Task<PayrollAdjustmentDTO> ApprovePayrollAdjustmentAsync(string voucherNo, string approverId, string? notes)
        {
            var entity = await _context.PayrollAdjustments
                .FirstOrDefaultAsync(x => x.voucherNo == voucherNo);

            if (entity == null) throw new Exception("Không tìm thấy khoản cộng/trừ");
            if (entity.ApproveStatus != ApproveStatusEnum.Pending) throw new Exception("Khoản cộng/trừ không ở trạng thái chờ duyệt");

            var approver = await _context.ApplicationUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == approverId);
            if (approver == null) throw new Exception("Không tìm thấy người duyệt");

            // Kiểm tra người cùng cấp với người submit không được duyệt/từ chối/trả lại
            var submitHistory = await _context.ApprovalHistories
                .Include(h => h.Approver)
                    .ThenInclude(a => a.Role)
                .Where(h => h.RequestType == "PayrollAdjustment" && h.RequestID == voucherNo && h.Action == "Submit")
                .OrderBy(h => h.CreatedAt)
                .FirstOrDefaultAsync();
            
            if (submitHistory != null && submitHistory.Approver?.Role != null && approver.Role != null)
            {
                if (submitHistory.Approver.Role.ID == approver.Role.ID)
                {
                    throw new Exception("Bạn không thể duyệt đơn của người cùng cấp. Phải đi theo quy trình duyệt");
                }
            }

            // Check if approver has permission based on workflow
            // Contract/Adjustment: HREmployee (6) → HRManager (5) → Director (3)
            var canApprove = await CanApprovePayrollAdjustmentAsync(entity, approver);
            if (!canApprove) throw new Exception("Bạn không có quyền duyệt khoản cộng/trừ này ở giai đoạn hiện tại");

            var oldStatus = entity.ApproveStatus;
            var nextStatus = await GetNextApprovalStatusForPayrollAdjustmentAsync(entity, approver);
            
            entity.ApproveStatus = nextStatus;

            await _context.SaveChangesAsync();

            // Create approval history
            await _approvalHistoryService.CreateHistoryAsync(
                "PayrollAdjustment",
                voucherNo,
                approverId,
                $"{approver.FirstName} {approver.LastName}",
                "Approve",
                oldStatus,
                nextStatus,
                notes
            );

            return await GetPayrollAdjustmentByIdAsync(voucherNo);
        }

        public async Task<PayrollAdjustmentDTO> RejectPayrollAdjustmentAsync(string voucherNo, string approverId, string? notes)
        {
            var entity = await _context.PayrollAdjustments
                .FirstOrDefaultAsync(x => x.voucherNo == voucherNo);

            if (entity == null) throw new Exception("Không tìm thấy khoản cộng/trừ");
            if (entity.ApproveStatus != ApproveStatusEnum.Pending) throw new Exception("Khoản cộng/trừ không ở trạng thái chờ duyệt");

            var approver = await _context.ApplicationUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == approverId);
            if (approver == null) throw new Exception("Không tìm thấy người duyệt");

            // Kiểm tra người cùng cấp với người submit không được duyệt/từ chối/trả lại
            var submitHistory = await _context.ApprovalHistories
                .Include(h => h.Approver)
                    .ThenInclude(a => a.Role)
                .Where(h => h.RequestType == "PayrollAdjustment" && h.RequestID == voucherNo && h.Action == "Submit")
                .OrderBy(h => h.CreatedAt)
                .FirstOrDefaultAsync();
            
            if (submitHistory != null && submitHistory.Approver?.Role != null && approver.Role != null)
            {
                if (submitHistory.Approver.Role.ID == approver.Role.ID)
                {
                    throw new Exception("Bạn không thể từ chối đơn của người cùng cấp. Phải đi theo quy trình duyệt");
                }
            }

            var oldStatus = entity.ApproveStatus;
            entity.ApproveStatus = ApproveStatusEnum.Rejected;

            await _context.SaveChangesAsync();

            // Create approval history
            await _approvalHistoryService.CreateHistoryAsync(
                "PayrollAdjustment",
                voucherNo,
                approverId,
                $"{approver.FirstName} {approver.LastName}",
                "Reject",
                oldStatus,
                ApproveStatusEnum.Rejected,
                notes
            );

            return await GetPayrollAdjustmentByIdAsync(voucherNo);
        }

        public async Task<PayrollAdjustmentDTO> ReturnPayrollAdjustmentAsync(string voucherNo, string approverId, string? notes)
        {
            var entity = await _context.PayrollAdjustments
                .FirstOrDefaultAsync(x => x.voucherNo == voucherNo);

            if (entity == null) throw new Exception("Không tìm thấy khoản cộng/trừ");
            if (entity.ApproveStatus != ApproveStatusEnum.Pending) throw new Exception("Khoản cộng/trừ không ở trạng thái chờ duyệt");

            var approver = await _context.ApplicationUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == approverId);
            if (approver == null) throw new Exception("Không tìm thấy người duyệt");

            // Kiểm tra người cùng cấp với người submit không được duyệt/từ chối/trả lại
            var submitHistory = await _context.ApprovalHistories
                .Include(h => h.Approver)
                    .ThenInclude(a => a.Role)
                .Where(h => h.RequestType == "PayrollAdjustment" && h.RequestID == voucherNo && h.Action == "Submit")
                .OrderBy(h => h.CreatedAt)
                .FirstOrDefaultAsync();
            
            if (submitHistory != null && submitHistory.Approver?.Role != null && approver.Role != null)
            {
                if (submitHistory.Approver.Role.ID == approver.Role.ID)
                {
                    throw new Exception("Bạn không thể trả lại đơn của người cùng cấp. Phải đi theo quy trình duyệt");
                }
            }

            var oldStatus = entity.ApproveStatus;
            entity.ApproveStatus = ApproveStatusEnum.Created; // Return về "Tạo mới"

            await _context.SaveChangesAsync();

            // Create approval history
            await _approvalHistoryService.CreateHistoryAsync(
                "PayrollAdjustment",
                voucherNo,
                approverId,
                $"{approver.FirstName} {approver.LastName}",
                "Return",
                oldStatus,
                ApproveStatusEnum.Created,
                notes
            );

            return await GetPayrollAdjustmentByIdAsync(voucherNo);
        }

        // Helper methods for approval workflow
        private async Task<bool> CanApprovePayrollAdjustmentAsync(PayrollAdjustment adjustment, ApplicationUser approver)
        {
            var approverRoleId = approver.RoleID;

            // Check approval history to determine current level
            var history = await _context.ApprovalHistories
                .Where(h => h.RequestType == "PayrollAdjustment" && h.RequestID == adjustment.voucherNo)
                .OrderByDescending(h => h.CreatedAt)
                .FirstOrDefaultAsync();

            // If no history, check who created it (must be HREmployee)
            if (history == null)
            {
                // First approval: HREmployee (6) → HRManager (5)
                return approverRoleId == 5; // HRManager can approve
            }

            // If last approver was HREmployee (6), next is HRManager (5)
            if (history != null)
            {
                var lastApprover = await _context.ApplicationUsers
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Id == history.ApproverID);
                
                if (lastApprover?.RoleID == 6 && approverRoleId == 5) return true; // HREmployee → HRManager
                if (lastApprover?.RoleID == 5 && approverRoleId == 3) return true; // HRManager → Director
            }

            return false;
        }

        private async Task<ApproveStatusEnum> GetNextApprovalStatusForPayrollAdjustmentAsync(PayrollAdjustment adjustment, ApplicationUser approver)
        {
            var approverRoleId = approver.RoleID;

            // If Director approves, it's final approval
            if (approverRoleId == 3) return ApproveStatusEnum.Approved;

            // Otherwise, still pending for next level
            return ApproveStatusEnum.Pending;
        }
    }
}
