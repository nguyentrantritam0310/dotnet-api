using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;

namespace dotnet_api.Services.Interfaces
{
    public interface IConstructionService
    {
        Task<ConstructionDTO> CreateConstructionAsync(ConstructionDTOPOST constructionDTO);
        Task<ConstructionDTO> CreateConstructionAsync(ConstructionDTOPOST constructionDTO, Microsoft.AspNetCore.Http.IFormFile designBlueprint);
        Task<ConstructionDTO> GetConstructionByIdAsync(int id);
        Task<IEnumerable<ConstructionDTO>> GetAllConstructionsAsync();
        Task<ConstructionDTO> UpdateConstructionAsync(ConstructionDTO constructionDTO);
        Task<ConstructionDTO> UpdateConstructionAsync(ConstructionDTO constructionDTO, Microsoft.AspNetCore.Http.IFormFile designBlueprint);
        Task<ConstructionDTO> UpdateConstructionStatusAsync(int id, int status);
    }
}
