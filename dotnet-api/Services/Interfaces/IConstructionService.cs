using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using Microsoft.AspNetCore.Http;

namespace dotnet_api.Services.Interfaces
{
    public interface IConstructionService
    {
        Task<ConstructionDTO> CreateConstructionAsync(ConstructionCreateDTO constructionDTO);
        Task<ConstructionDTO> GetConstructionByIdAsync(int id);
        Task<IEnumerable<ConstructionDTO>> GetAllConstructionsAsync();
        Task<ConstructionDTO> UpdateConstructionAsync(ConstructionUpdateDTO constructionDTO);
        Task<ConstructionDTO> UpdateConstructionStatusAsync(int id, int status);
        Task<string> SaveDesignBlueprintAsync(IFormFile file);
        Task DeleteDesignBlueprintAsync(string filePath);
    }
}
