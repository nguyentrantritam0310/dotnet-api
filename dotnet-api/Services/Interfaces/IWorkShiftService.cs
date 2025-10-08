using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using Microsoft.AspNetCore.Http;

namespace dotnet_api.Services.Interfaces
{
    public interface IWorkShiftService
    {
        Task<WorkShiftDTO> CreateWorkShiftAsync(WorkShiftDTOPOST constructionDTO);
        Task<WorkShiftDTO> GetWorkShiftByIdAsync(int id);
        Task<IEnumerable<WorkShiftDTO>> GetAllWorkShiftsAsync();
        Task<WorkShiftDTO> UpdateWorkShiftAsync(WorkShiftDTOPUT dto);
        Task<bool> DeleteWorkShiftAsync(int id);
        //Task<ConstructionDTO> UpdateConstructionAsync(ConstructionUpdateDTO constructionDTO);
        //Task<WorkShiftDTO> UpdateWorkShiftsStatusAsync(int id);
        //Task<string> SaveDesignBlueprintAsync(IFormFile file);
        //Task DeleteDesignBlueprintAsync(string filePath);
    }
}
