using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using Microsoft.AspNetCore.Http;

namespace dotnet_api.Services.Interfaces
{
    public interface IWorkShiftService
    {
        //Task<ConstructionDTO> CreateWorkShiftAsync(ConstructionCreateDTO constructionDTO);
        Task<WorkShiftDTO> GetWorkShiftByIdAsync(int id);
        Task<IEnumerable<WorkShiftDTO>> GetAllWorkShiftsAsync();
        //Task<ConstructionDTO> UpdateConstructionAsync(ConstructionUpdateDTO constructionDTO);
        //Task<ConstructionDTO> UpdateConstructionStatusAsync(int id, int status);
        //Task<string> SaveDesignBlueprintAsync(IFormFile file);
        //Task DeleteDesignBlueprintAsync(string filePath);
    }
}
