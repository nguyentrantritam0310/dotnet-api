using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.DTOs.PUT;

namespace dotnet_api.Services.Interfaces
{
    public interface IContractService
    {
        Task<IEnumerable<ContractDTO>> GetAllContractsAsync();
        Task<ContractDTO> GetContractByIdAsync(int id);
        Task<ContractDTO> CreateContractAsync(ContractDTOPOST contractDTO);
        Task<ContractDTO> UpdateContractAsync(ContractDTOPUT contractDTO);
        Task<bool> DeleteContractAsync(int id);
        Task<IEnumerable<ContractTypeDTO>> GetContractTypesAsync();
       
        Task<IEnumerable<AllowanceDTO>> GetAllowancesAsync();

        // Approval workflow methods
        Task<ContractDTO> SubmitContractForApprovalAsync(int contractId, string submitterId, string? notes);
        Task<ContractDTO> ApproveContractAsync(int contractId, string approverId, string? notes);
        Task<ContractDTO> RejectContractAsync(int contractId, string approverId, string? notes);
        Task<ContractDTO> ReturnContractAsync(int contractId, string approverId, string? notes);
    }
}