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

        // Approval workflow methods
        Task<PayrollAdjustmentDTO> SubmitPayrollAdjustmentForApprovalAsync(string voucherNo, string submitterId, string? notes);
        Task<PayrollAdjustmentDTO> ApprovePayrollAdjustmentAsync(string voucherNo, string approverId, string? notes);
        Task<PayrollAdjustmentDTO> RejectPayrollAdjustmentAsync(string voucherNo, string approverId, string? notes);
        Task<PayrollAdjustmentDTO> ReturnPayrollAdjustmentAsync(string voucherNo, string approverId, string? notes);
    }
}
