using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using Microsoft.AspNetCore.Http;

namespace dotnet_api.Services.Interfaces
{
    public interface IPayrollAdjustmentService
    {
        Task<PayrollAdjustmentDTO> CreatePayrollAdjustmentAsync(PayrollAdjustmentDTO PayrollAdjustmentDTO);
        Task<PayrollAdjustmentDTO> GetPayrollAdjustmentByIdAsync(string id);
        Task<IEnumerable<PayrollAdjustmentDTO>> GetAllPayrollAdjustmentsAsync();
        Task<PayrollAdjustmentDTO> UpdatePayrollAdjustmentAsync(PayrollAdjustmentDTO PayrollAdjustmentDTO);
        Task<PayrollAdjustmentDTO> DeletePayrollAdjustmentAsync(string id);
    }
}
