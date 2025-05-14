using dotnet_api.Data.Entities;
using dotnet_api.DTOs;

namespace dotnet_api.Services.Interfaces
{

    public interface IMaterialPlanService
    {
        Task<MaterialPlanDTOPOST> CreateMaterialPlanAsync(MaterialPlanDTOPOST MaterialPlan);
        Task<IEnumerable<MaterialPlanDTO>> GetMaterialPlanByIdAsync(int importOrderId);
        Task<MaterialPlanDTOPOST> UpdateMaterialPlanQuantityAndNoteAsync(MaterialPlanDTOPOST dto);
        //Task<IEnumerable<MaterialPlanDTO>> GetAllMaterialPlanAsync();
        //Task<MaterialPlanDTO> UpdateMaterialPlanAsync(MaterialPlanDTO MaterialPlan);
        //Task<bool> DeleteMaterialPlanAsync(int id);
    }
}
