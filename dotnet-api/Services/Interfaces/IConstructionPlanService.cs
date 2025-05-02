using dotnet_api.DTOs;

namespace dotnet_api.Services.Interfaces
{
    public interface IConstructionPlanService
    {
        Task<ConstructionPlanDTO> CreateConstructionPlanAsync(ConstructionPlanDTO constructionPlan);
        Task<ConstructionPlanDTO> GetConstructionPlanByIdAsync(int id);
        Task<IEnumerable<ConstructionPlanDTO>> GetAllConstructionsPlanAsync();
        Task<ConstructionPlanDTO> UpdateConstructionPlanAsync(ConstructionPlanDTO constructionPlan);
        Task<bool> DeleteConstructionPlanAsync(int id);
    }
}
