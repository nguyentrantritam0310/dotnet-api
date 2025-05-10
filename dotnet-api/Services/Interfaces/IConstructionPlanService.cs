using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;

namespace dotnet_api.Services.Interfaces
{
    public interface IConstructionPlanService
    {
        Task<ConstructionPlanDTO> CreateConstructionPlanAsync(ConstructionPlanDTOPOST constructionPlanDTO);
        Task<ConstructionPlanDTO> GetConstructionPlanByIdAsync(int id);
        Task<IEnumerable<ConstructionPlanDTO>> GetAllConstructionsPlanAsync();
        Task<ConstructionPlanDTO> UpdateConstructionPlanAsync(ConstructionPlanDTOPUT constructionPlanDTO);
        Task<ConstructionPlanDTO> UpdateConstructionPlanStatusAsync(int id, int status);
        Task<bool> DeleteConstructionPlanAsync(int id);
    }
}
