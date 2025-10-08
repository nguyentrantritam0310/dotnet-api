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
        private const string UPLOAD_DIRECTORY = "uploads/blueprints";

        public PayrollAdjustmentService(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }


        public async Task<IEnumerable<PayrollAdjustmentDTO>> GetAllPayrollAdjustmentsAsync()
        {
            var PayrollAdjustments = await _context.PayrollAdjustments
                .Include(pa => pa.AdjustmentType)
                .Include(pa => pa.AdjustmentItem)
                .Include(pa => pa.applicationUser_PayrollAdjustment)
                    .ThenInclude(ua => ua.applicationUser)
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
                    ApproveStatus = ApproveStatusEnum.Pending
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
    }
}
