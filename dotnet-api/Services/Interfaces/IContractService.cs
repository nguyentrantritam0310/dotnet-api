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

        // Approve/Reject Contract methods
        Task<ContractDTO> ApproveContractAsync(int contractId);
        Task<ContractDTO> RejectContractAsync(int contractId);
        Task<ContractDTO> PendingContractAsync(int contractId);
    }
}