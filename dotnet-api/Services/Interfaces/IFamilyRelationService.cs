using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.DTOs.PUT;

namespace dotnet_api.Services.Interfaces
{
    public interface IFamilyRelationService
    {
        Task<IEnumerable<FamilyRelationDTO>> GetAllFamilyRelationsAsync();
        Task<IEnumerable<FamilyRelationDTO>> GetFamilyRelationsByEmployeeIdAsync(string employeeId);
        Task<FamilyRelationDTO> GetFamilyRelationByIdAsync(int id);
        Task<FamilyRelationDTO> CreateFamilyRelationAsync(FamilyRelationDTOPOST familyRelationDTO);
        Task<FamilyRelationDTO> UpdateFamilyRelationAsync(FamilyRelationDTOPUT familyRelationDTO);
        Task<bool> DeleteFamilyRelationAsync(int id);
    }
}
