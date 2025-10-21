using dotnet_api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_api.Services
{
    public interface IPayrollFeedbackService
    {
        Task<IEnumerable<PayrollFeedbackDto>> GetPayrollFeedbacksByEmployeeAsync(string employeeId);
        Task<PayrollFeedbackDto> GetPayrollFeedbackAsync(int payrollId, string employeeId);
        Task<PayrollFeedbackDto> CreatePayrollFeedbackAsync(CreatePayrollFeedbackDto createDto, string employeeId);
        Task<PayrollFeedbackDto> UpdatePayrollFeedbackAsync(UpdatePayrollFeedbackDto updateDto, string employeeId);
        Task<bool> DeletePayrollFeedbackAsync(int payrollId, string employeeId);
        Task<IEnumerable<PayrollFeedbackDto>> GetPayrollFeedbacksByPayrollAsync(int payrollId);
    }
}
