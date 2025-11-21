using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.Data.Enums;
using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.DTOs.PUT;
using dotnet_api.Helpers;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text.Json;

namespace dotnet_api.Services
{
    public class EmployeeRequestService : IEmployeeRequestService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly IApprovalHistoryService _approvalHistoryService;
        private const string UPLOAD_DIRECTORY = "uploads/blueprints";

        public EmployeeRequestService(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment environment, IApprovalHistoryService approvalHistoryService)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
            _approvalHistoryService = approvalHistoryService;
        }


        public async Task<IEnumerable<EmployeeRequestDTO>> GetAllEmployeeRequestsAsync()
        {
            var employeeRequests = await _context.EmployeeRequests
                .Include(io => io.Employee)
                .Include(io => io.LeaveType)
                .Include(io => io.OvertimeType)
                .Include(io => io.OvertimeForm)
                .ToListAsync();

            return _mapper.Map<IEnumerable<EmployeeRequestDTO>>(employeeRequests);
        }

        public async Task<EmployeeRequestDTO> GetEmployeeRequestByIdAsync(string VoucherCode)
        {
            var machine = await _context.EmployeeRequests.FirstOrDefaultAsync(x => x.VoucherCode == VoucherCode);
            if (machine == null) return null;
            return _mapper.Map<EmployeeRequestDTO>(machine);
        }

        public async Task<EmployeeRequestDTO> CreateEmployeeRequestAsync(EmployeeRequestDTO employeeRequestDTO)
        {
            var entity = _mapper.Map<EmployeeRequests>(employeeRequestDTO);
            _context.EmployeeRequests.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<EmployeeRequestDTO>(entity);
        }

        public async Task<EmployeeRequestDTO> UpdateEmployeeRequestAsync(EmployeeRequestDTO dto)
        {
            var entity = await _context.EmployeeRequests.FirstOrDefaultAsync(x => x.VoucherCode == dto.VoucherCode);
            if (entity == null) return null;

            // Map dữ liệu mới vào entity hiện có
            _mapper.Map(dto, entity);

            _context.EmployeeRequests.Update(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<EmployeeRequestDTO>(entity);
        }

        public async Task<EmployeeRequestDTO> DeleteEmployeeRequestAsync(string voucherCode)
        {
            var entity = await _context.EmployeeRequests.FirstOrDefaultAsync(x => x.VoucherCode == voucherCode);
            if (entity == null) return null;

            _context.EmployeeRequests.Remove(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<EmployeeRequestDTO>(entity);
        }

        // Leave Request specific implementations
        public async Task<LeaveRequestDTO> CreateLeaveRequestAsync(LeaveRequestDTOPOST leaveRequestDTO)
        {
            var entity = _mapper.Map<EmployeeRequests>(leaveRequestDTO);
            _context.EmployeeRequests.Add(entity);
            await _context.SaveChangesAsync();

            // Reload with related data for proper mapping
            var createdEntity = await _context.EmployeeRequests
                .Include(e => e.Employee)
                .Include(e => e.LeaveType)
                .Include(e => e.WorkShift)
                .FirstOrDefaultAsync(e => e.VoucherCode == entity.VoucherCode);

            return _mapper.Map<LeaveRequestDTO>(createdEntity);
        }

        public async Task<LeaveRequestDTO> GetLeaveRequestByIdAsync(string voucherCode)
        {
            var entity = await _context.EmployeeRequests
                .Include(e => e.Employee)
                .Include(e => e.LeaveType)
                .Include(e => e.WorkShift)
                .FirstOrDefaultAsync(x => x.VoucherCode == voucherCode && (x.RequestType == "Leave" || x.RequestType == "Nghỉ phép"));
            
            if (entity == null) return null;
            return _mapper.Map<LeaveRequestDTO>(entity);
        }

        public async Task<IEnumerable<LeaveRequestDTO>> GetAllLeaveRequestsAsync()
        {
            var leaveRequests = await _context.EmployeeRequests
                .Include(e => e.Employee)
                .Include(e => e.LeaveType)
                .Include(e => e.WorkShift)
                .Where(e => e.RequestType == "Leave" || e.RequestType == "Nghỉ phép")
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();

            return _mapper.Map<IEnumerable<LeaveRequestDTO>>(leaveRequests);
        }

        public async Task<LeaveRequestDTO> UpdateLeaveRequestAsync(LeaveRequestDTOPUT leaveRequestDTO)
        {
            var entity = await _context.EmployeeRequests
                .FirstOrDefaultAsync(x => x.VoucherCode == leaveRequestDTO.VoucherCode && (x.RequestType == "Leave" || x.RequestType == "Nghỉ phép"));
            
            if (entity == null) return null;

            // Map dữ liệu mới vào entity hiện có
            _mapper.Map(leaveRequestDTO, entity);

            _context.EmployeeRequests.Update(entity);
            await _context.SaveChangesAsync();

            // Reload with related data for proper mapping
            var updatedEntity = await _context.EmployeeRequests
                .Include(e => e.Employee)
                .Include(e => e.LeaveType)
                .Include(e => e.WorkShift)
                .FirstOrDefaultAsync(e => e.VoucherCode == entity.VoucherCode);

            return _mapper.Map<LeaveRequestDTO>(updatedEntity);
        }

        public async Task<LeaveRequestDTO> DeleteLeaveRequestAsync(string voucherCode)
        {
            var entity = await _context.EmployeeRequests
                .FirstOrDefaultAsync(x => x.VoucherCode == voucherCode && (x.RequestType == "Leave" || x.RequestType == "Nghỉ phép"));
            
            if (entity == null) return null;

            _context.EmployeeRequests.Remove(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<LeaveRequestDTO>(entity);
        }

        // Overtime Request specific methods
        public async Task<OvertimeRequestDTO> CreateOvertimeRequestAsync(OvertimeRequestDTOPOST overtimeRequestDTO)
        {
            var entity = _mapper.Map<EmployeeRequests>(overtimeRequestDTO);
            _context.EmployeeRequests.Add(entity);
            await _context.SaveChangesAsync();

            var createdEntity = await _context.EmployeeRequests
                .Include(e => e.Employee)
                .Include(e => e.OvertimeType)
                .Include(e => e.OvertimeForm)
                .FirstOrDefaultAsync(e => e.VoucherCode == entity.VoucherCode);

            return _mapper.Map<OvertimeRequestDTO>(createdEntity);
        }

        public async Task<OvertimeRequestDTO> GetOvertimeRequestByIdAsync(string voucherCode)
        {
            var entity = await _context.EmployeeRequests
                .Include(e => e.Employee)
                .Include(e => e.OvertimeType)
                .Include(e => e.OvertimeForm)
                .FirstOrDefaultAsync(x => x.VoucherCode == voucherCode && x.RequestType == "Tăng ca");
            
            if (entity == null) return null;
            return _mapper.Map<OvertimeRequestDTO>(entity);
        }

        public async Task<IEnumerable<OvertimeRequestDTO>> GetAllOvertimeRequestsAsync()
        {
            var overtimeRequests = await _context.EmployeeRequests
                .Include(e => e.Employee)
                .Include(e => e.OvertimeType)
                .Include(e => e.OvertimeForm)
                .Where(e => e.RequestType == "Tăng ca")
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OvertimeRequestDTO>>(overtimeRequests);
        }

        public async Task<OvertimeRequestDTO> UpdateOvertimeRequestAsync(OvertimeRequestDTOPUT overtimeRequestDTO)
        {
            var entity = await _context.EmployeeRequests
                .FirstOrDefaultAsync(x => x.VoucherCode == overtimeRequestDTO.VoucherCode && x.RequestType == "Tăng ca");
            
            if (entity == null) return null;

            _mapper.Map(overtimeRequestDTO, entity);

            _context.EmployeeRequests.Update(entity);
            await _context.SaveChangesAsync();

            var updatedEntity = await _context.EmployeeRequests
                .Include(e => e.Employee)
                .Include(e => e.OvertimeType)
                .Include(e => e.OvertimeForm)
                .FirstOrDefaultAsync(e => e.VoucherCode == entity.VoucherCode);

            return _mapper.Map<OvertimeRequestDTO>(updatedEntity);
        }

        public async Task<OvertimeRequestDTO> DeleteOvertimeRequestAsync(string voucherCode)
        {
            var entity = await _context.EmployeeRequests
                .FirstOrDefaultAsync(x => x.VoucherCode == voucherCode && x.RequestType == "Tăng ca");
            
            if (entity == null) return null;

            _context.EmployeeRequests.Remove(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<OvertimeRequestDTO>(entity);
        }

        // Approval workflow methods for Leave Requests
        public async Task<LeaveRequestDTO> SubmitLeaveRequestForApprovalAsync(string voucherCode, string submitterId, string? notes)
        {
            var entity = await _context.EmployeeRequests
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(x => x.VoucherCode == voucherCode && (x.RequestType == "Leave" || x.RequestType == "Nghỉ phép"));

            if (entity == null) throw new Exception("Không tìm thấy đơn nghỉ phép");
            if (entity.ApproveStatus != ApproveStatusEnum.Created) throw new Exception("Đơn đã được gửi duyệt hoặc đã xử lý");

            var submitter = await _context.ApplicationUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == submitterId);
            if (submitter == null) throw new Exception("Không tìm thấy người gửi");

            var oldStatus = entity.ApproveStatus;
            entity.ApproveStatus = ApproveStatusEnum.Pending;

            await _context.SaveChangesAsync();

            // Create approval history
            await _approvalHistoryService.CreateHistoryAsync(
                "LeaveRequest",
                voucherCode,
                submitterId,
                $"{submitter.FirstName} {submitter.LastName}",
                "Submit",
                oldStatus,
                ApproveStatusEnum.Pending,
                notes
            );

            return await GetLeaveRequestByIdAsync(voucherCode);
        }

        public async Task<LeaveRequestDTO> ApproveLeaveRequestAsync(string voucherCode, string approverId, string? notes)
        {
            var entity = await _context.EmployeeRequests
                .Include(e => e.Employee)
                    .ThenInclude(emp => emp.Role)
                .FirstOrDefaultAsync(x => x.VoucherCode == voucherCode && (x.RequestType == "Leave" || x.RequestType == "Nghỉ phép"));

            if (entity == null) throw new Exception("Không tìm thấy đơn nghỉ phép");
            if (entity.ApproveStatus != ApproveStatusEnum.Pending) throw new Exception("Đơn không ở trạng thái chờ duyệt");

            var approver = await _context.ApplicationUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == approverId);
            if (approver == null) throw new Exception("Không tìm thấy người duyệt");

            // Người cùng cấp với người submit không được duyệt/từ chối/trả lại
            // Phải đi theo quy trình duyệt (cấp trên mới được duyệt)
            if (entity.Employee?.Role != null && approver.Role != null)
            {
                if (entity.Employee.Role.ID == approver.Role.ID)
                {
                    throw new Exception("Bạn không thể duyệt đơn của người cùng cấp. Phải đi theo quy trình duyệt");
                }
            }

            // Check if approver has permission based on workflow
            var canApprove = await CanApproveLeaveRequestAsync(entity, approver);
            if (!canApprove) throw new Exception("Bạn không có quyền duyệt đơn này ở giai đoạn hiện tại");

            var oldStatus = entity.ApproveStatus;
            var nextStatus = await GetNextApprovalStatusForLeaveRequestAsync(entity, approver);
            
            entity.ApproveStatus = nextStatus;

            await _context.SaveChangesAsync();

            // Create approval history
            await _approvalHistoryService.CreateHistoryAsync(
                "LeaveRequest",
                voucherCode,
                approverId,
                $"{approver.FirstName} {approver.LastName}",
                "Approve",
                oldStatus,
                nextStatus,
                notes
            );

            return await GetLeaveRequestByIdAsync(voucherCode);
        }

        public async Task<LeaveRequestDTO> RejectLeaveRequestAsync(string voucherCode, string approverId, string? notes)
        {
            var entity = await _context.EmployeeRequests
                .Include(e => e.Employee)
                    .ThenInclude(emp => emp.Role)
                .FirstOrDefaultAsync(x => x.VoucherCode == voucherCode && (x.RequestType == "Leave" || x.RequestType == "Nghỉ phép"));

            if (entity == null) throw new Exception("Không tìm thấy đơn nghỉ phép");
            if (entity.ApproveStatus != ApproveStatusEnum.Pending) throw new Exception("Đơn không ở trạng thái chờ duyệt");

            var approver = await _context.ApplicationUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == approverId);
            if (approver == null) throw new Exception("Không tìm thấy người duyệt");

            // Người cùng cấp với người submit không được duyệt/từ chối/trả lại
            if (entity.Employee?.Role != null && approver.Role != null)
            {
                if (entity.Employee.Role.ID == approver.Role.ID)
                {
                    throw new Exception("Bạn không thể từ chối đơn của người cùng cấp. Phải đi theo quy trình duyệt");
                }
            }

            var oldStatus = entity.ApproveStatus;
            entity.ApproveStatus = ApproveStatusEnum.Rejected;

            await _context.SaveChangesAsync();

            // Create approval history
            await _approvalHistoryService.CreateHistoryAsync(
                "LeaveRequest",
                voucherCode,
                approverId,
                $"{approver.FirstName} {approver.LastName}",
                "Reject",
                oldStatus,
                ApproveStatusEnum.Rejected,
                notes
            );

            return await GetLeaveRequestByIdAsync(voucherCode);
        }

        public async Task<LeaveRequestDTO> ReturnLeaveRequestAsync(string voucherCode, string approverId, string? notes)
        {
            var entity = await _context.EmployeeRequests
                .Include(e => e.Employee)
                    .ThenInclude(emp => emp.Role)
                .FirstOrDefaultAsync(x => x.VoucherCode == voucherCode && (x.RequestType == "Leave" || x.RequestType == "Nghỉ phép"));

            if (entity == null) throw new Exception("Không tìm thấy đơn nghỉ phép");
            if (entity.ApproveStatus != ApproveStatusEnum.Pending) throw new Exception("Đơn không ở trạng thái chờ duyệt");

            var approver = await _context.ApplicationUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == approverId);
            if (approver == null) throw new Exception("Không tìm thấy người duyệt");

            // Người cùng cấp với người submit không được duyệt/từ chối/trả lại
            if (entity.Employee?.Role != null && approver.Role != null)
            {
                if (entity.Employee.Role.ID == approver.Role.ID)
                {
                    throw new Exception("Bạn không thể trả lại đơn của người cùng cấp. Phải đi theo quy trình duyệt");
                }
            }

            var oldStatus = entity.ApproveStatus;
            entity.ApproveStatus = ApproveStatusEnum.Created; // Return về "Tạo mới"

            await _context.SaveChangesAsync();

            // Create approval history
            await _approvalHistoryService.CreateHistoryAsync(
                "LeaveRequest",
                voucherCode,
                approverId,
                $"{approver.FirstName} {approver.LastName}",
                "Return",
                oldStatus,
                ApproveStatusEnum.Created,
                notes
            );

            return await GetLeaveRequestByIdAsync(voucherCode);
        }

        // Approval workflow methods for Overtime Requests
        public async Task<OvertimeRequestDTO> SubmitOvertimeRequestForApprovalAsync(string voucherCode, string submitterId, string? notes)
        {
            var entity = await _context.EmployeeRequests
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(x => x.VoucherCode == voucherCode && x.RequestType == "Tăng ca");

            if (entity == null) throw new Exception("Không tìm thấy đơn tăng ca");
            if (entity.ApproveStatus != ApproveStatusEnum.Created) throw new Exception("Đơn đã được gửi duyệt hoặc đã xử lý");

            var submitter = await _context.ApplicationUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == submitterId);
            if (submitter == null) throw new Exception("Không tìm thấy người gửi");

            var oldStatus = entity.ApproveStatus;
            entity.ApproveStatus = ApproveStatusEnum.Pending;

            await _context.SaveChangesAsync();

            // Create approval history
            await _approvalHistoryService.CreateHistoryAsync(
                "OvertimeRequest",
                voucherCode,
                submitterId,
                $"{submitter.FirstName} {submitter.LastName}",
                "Submit",
                oldStatus,
                ApproveStatusEnum.Pending,
                notes
            );

            return await GetOvertimeRequestByIdAsync(voucherCode);
        }

        public async Task<OvertimeRequestDTO> ApproveOvertimeRequestAsync(string voucherCode, string approverId, string? notes)
        {
            var entity = await _context.EmployeeRequests
                .Include(e => e.Employee)
                    .ThenInclude(emp => emp.Role)
                .FirstOrDefaultAsync(x => x.VoucherCode == voucherCode && x.RequestType == "Tăng ca");

            if (entity == null) throw new Exception("Không tìm thấy đơn tăng ca");
            if (entity.ApproveStatus != ApproveStatusEnum.Pending) throw new Exception("Đơn không ở trạng thái chờ duyệt");

            var approver = await _context.ApplicationUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == approverId);
            if (approver == null) throw new Exception("Không tìm thấy người duyệt");

            // Người cùng cấp với người submit không được duyệt/từ chối/trả lại
            if (entity.Employee?.Role != null && approver.Role != null)
            {
                if (entity.Employee.Role.ID == approver.Role.ID)
                {
                    throw new Exception("Bạn không thể duyệt đơn của người cùng cấp. Phải đi theo quy trình duyệt");
                }
            }

            // Check if approver has permission based on workflow
            var canApprove = await CanApproveOvertimeRequestAsync(entity, approver);
            if (!canApprove) throw new Exception("Bạn không có quyền duyệt đơn này ở giai đoạn hiện tại");

            var oldStatus = entity.ApproveStatus;
            var nextStatus = await GetNextApprovalStatusForOvertimeRequestAsync(entity, approver);
            
            entity.ApproveStatus = nextStatus;

            await _context.SaveChangesAsync();

            // Create approval history
            await _approvalHistoryService.CreateHistoryAsync(
                "OvertimeRequest",
                voucherCode,
                approverId,
                $"{approver.FirstName} {approver.LastName}",
                "Approve",
                oldStatus,
                nextStatus,
                notes
            );

            return await GetOvertimeRequestByIdAsync(voucherCode);
        }

        public async Task<OvertimeRequestDTO> RejectOvertimeRequestAsync(string voucherCode, string approverId, string? notes)
        {
            var entity = await _context.EmployeeRequests
                .Include(e => e.Employee)
                    .ThenInclude(emp => emp.Role)
                .FirstOrDefaultAsync(x => x.VoucherCode == voucherCode && x.RequestType == "Tăng ca");

            if (entity == null) throw new Exception("Không tìm thấy đơn tăng ca");
            if (entity.ApproveStatus != ApproveStatusEnum.Pending) throw new Exception("Đơn không ở trạng thái chờ duyệt");

            var approver = await _context.ApplicationUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == approverId);
            if (approver == null) throw new Exception("Không tìm thấy người duyệt");

            // Người cùng cấp với người submit không được duyệt/từ chối/trả lại
            if (entity.Employee?.Role != null && approver.Role != null)
            {
                if (entity.Employee.Role.ID == approver.Role.ID)
                {
                    throw new Exception("Bạn không thể từ chối đơn của người cùng cấp. Phải đi theo quy trình duyệt");
                }
            }

            var oldStatus = entity.ApproveStatus;
            entity.ApproveStatus = ApproveStatusEnum.Rejected;

            await _context.SaveChangesAsync();

            // Create approval history
            await _approvalHistoryService.CreateHistoryAsync(
                "OvertimeRequest",
                voucherCode,
                approverId,
                $"{approver.FirstName} {approver.LastName}",
                "Reject",
                oldStatus,
                ApproveStatusEnum.Rejected,
                notes
            );

            return await GetOvertimeRequestByIdAsync(voucherCode);
        }

        public async Task<OvertimeRequestDTO> ReturnOvertimeRequestAsync(string voucherCode, string approverId, string? notes)
        {
            var entity = await _context.EmployeeRequests
                .Include(e => e.Employee)
                    .ThenInclude(emp => emp.Role)
                .FirstOrDefaultAsync(x => x.VoucherCode == voucherCode && x.RequestType == "Tăng ca");

            if (entity == null) throw new Exception("Không tìm thấy đơn tăng ca");
            if (entity.ApproveStatus != ApproveStatusEnum.Pending) throw new Exception("Đơn không ở trạng thái chờ duyệt");

            var approver = await _context.ApplicationUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == approverId);
            if (approver == null) throw new Exception("Không tìm thấy người duyệt");

            // Người cùng cấp với người submit không được duyệt/từ chối/trả lại
            if (entity.Employee?.Role != null && approver.Role != null)
            {
                if (entity.Employee.Role.ID == approver.Role.ID)
                {
                    throw new Exception("Bạn không thể trả lại đơn của người cùng cấp. Phải đi theo quy trình duyệt");
                }
            }

            var oldStatus = entity.ApproveStatus;
            entity.ApproveStatus = ApproveStatusEnum.Created; // Return về "Tạo mới"

            await _context.SaveChangesAsync();

            // Create approval history
            await _approvalHistoryService.CreateHistoryAsync(
                "OvertimeRequest",
                voucherCode,
                approverId,
                $"{approver.FirstName} {approver.LastName}",
                "Return",
                oldStatus,
                ApproveStatusEnum.Created,
                notes
            );

            return await GetOvertimeRequestByIdAsync(voucherCode);
        }

        // Helper methods for approval workflow
        private async Task<bool> CanApproveLeaveRequestAsync(EmployeeRequests request, ApplicationUser approver)
        {
            var requesterRoleId = request.Employee?.RoleID ?? 0;
            var approverRoleId = approver.RoleID;

            // Worker (4) → Manager (2)
            if (requesterRoleId == 4 && approverRoleId == 2) return true;
            
            // Manager (2) or Technician (1) → HREmployee (6)
            if ((requesterRoleId == 2 || requesterRoleId == 1) && approverRoleId == 6) return true;
            
            // HREmployee (6) → HRManager (5)
            if (requesterRoleId == 6 && approverRoleId == 5) return true;
            
            // HRManager (5) → Director (3)
            if (requesterRoleId == 5 && approverRoleId == 3) return true;

            // Director can always approve (final step)
            if (approverRoleId == 3)
            {
                // Check if request is at HRManager level (last step before director)
                var history = await _context.ApprovalHistories
                    .Where(h => h.RequestType == "LeaveRequest" && h.RequestID == request.VoucherCode)
                    .OrderByDescending(h => h.CreatedAt)
                    .FirstOrDefaultAsync();
                
                if (history != null && history.NewStatus == ApproveStatusEnum.Pending)
                {
                    var lastApprover = await _context.ApplicationUsers
                        .Include(u => u.Role)
                        .FirstOrDefaultAsync(u => u.Id == history.ApproverID);
                    return lastApprover?.RoleID == 5; // HRManager
                }
            }

            return false;
        }

        private async Task<ApproveStatusEnum> GetNextApprovalStatusForLeaveRequestAsync(EmployeeRequests request, ApplicationUser approver)
        {
            var approverRoleId = approver.RoleID;

            // If Director approves, it's final approval
            if (approverRoleId == 3) return ApproveStatusEnum.Approved;

            // Otherwise, still pending for next level
            return ApproveStatusEnum.Pending;
        }

        private async Task<bool> CanApproveOvertimeRequestAsync(EmployeeRequests request, ApplicationUser approver)
        {
            // Same logic as leave request
            return await CanApproveLeaveRequestAsync(request, approver);
        }

        private async Task<ApproveStatusEnum> GetNextApprovalStatusForOvertimeRequestAsync(EmployeeRequests request, ApplicationUser approver)
        {
            // Same logic as leave request
            return await GetNextApprovalStatusForLeaveRequestAsync(request, approver);
        }
    }
}
