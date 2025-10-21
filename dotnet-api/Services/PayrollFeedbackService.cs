using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_api.Services
{
    public class PayrollFeedbackService : IPayrollFeedbackService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PayrollFeedbackService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PayrollFeedbackDto>> GetPayrollFeedbacksByEmployeeAsync(string employeeId)
        {
            var feedbacks = await _context.PayrollFeedbacks
                .Where(f => f.EmployeeID == employeeId)
                .Include(f => f.Employee)
                .OrderByDescending(f => f.PayrollFeedbackDate)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PayrollFeedbackDto>>(feedbacks);
        }

        public async Task<PayrollFeedbackDto> GetPayrollFeedbackAsync(int payrollId, string employeeId)
        {
            var feedback = await _context.PayrollFeedbacks
                .Where(f => f.PayrollID == payrollId && f.EmployeeID == employeeId)
                .Include(f => f.Employee)
                .FirstOrDefaultAsync();

            if (feedback == null)
                return null;

            return _mapper.Map<PayrollFeedbackDto>(feedback);
        }

        public async Task<PayrollFeedbackDto> CreatePayrollFeedbackAsync(CreatePayrollFeedbackDto createDto, string employeeId)
        {
            // Check if feedback already exists for this payroll and employee
            var existingFeedback = await _context.PayrollFeedbacks
                .FirstOrDefaultAsync(f => f.PayrollID == createDto.PayrollID && f.EmployeeID == employeeId);

            if (existingFeedback != null)
            {
                throw new InvalidOperationException("Phản ánh cho bảng lương này đã tồn tại.");
            }

            // Check if Payroll exists
            var payroll = await _context.Payrolls
                .FirstOrDefaultAsync(p => p.ID == createDto.PayrollID);

            if (payroll == null)
            {
                throw new InvalidOperationException("Bảng lương không tồn tại.");
            }

            var feedback = _mapper.Map<PayrollFeedback>(createDto);
            feedback.EmployeeID = employeeId;

            _context.PayrollFeedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            // Reload with navigation properties
            var createdFeedback = await _context.PayrollFeedbacks
                .Where(f => f.PayrollID == createDto.PayrollID && f.EmployeeID == employeeId)
                .Include(f => f.Employee)
                .FirstOrDefaultAsync();

            return _mapper.Map<PayrollFeedbackDto>(createdFeedback);
        }

        public async Task<PayrollFeedbackDto> UpdatePayrollFeedbackAsync(UpdatePayrollFeedbackDto updateDto, string employeeId)
        {
            var feedback = await _context.PayrollFeedbacks
                .FirstOrDefaultAsync(f => f.PayrollID == updateDto.PayrollID && f.EmployeeID == employeeId);

            if (feedback == null)
            {
                throw new InvalidOperationException("Phản ánh không tồn tại.");
            }

            feedback.Title = updateDto.Title;
            feedback.Content = updateDto.Content;

            await _context.SaveChangesAsync();

            // Reload with navigation properties
            var updatedFeedback = await _context.PayrollFeedbacks
                .Where(f => f.PayrollID == updateDto.PayrollID && f.EmployeeID == employeeId)
                .Include(f => f.Employee)
                .FirstOrDefaultAsync();

            return _mapper.Map<PayrollFeedbackDto>(updatedFeedback);
        }

        public async Task<bool> DeletePayrollFeedbackAsync(int payrollId, string employeeId)
        {
            var feedback = await _context.PayrollFeedbacks
                .FirstOrDefaultAsync(f => f.PayrollID == payrollId && f.EmployeeID == employeeId);

            if (feedback == null)
            {
                return false;
            }

            _context.PayrollFeedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PayrollFeedbackDto>> GetPayrollFeedbacksByPayrollAsync(int payrollId)
        {
            var feedbacks = await _context.PayrollFeedbacks
                .Where(f => f.PayrollID == payrollId)
                .Include(f => f.Employee)
                .OrderByDescending(f => f.PayrollFeedbackDate)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PayrollFeedbackDto>>(feedbacks);
        }
    }
}