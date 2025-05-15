using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;

namespace dotnet_api.Services.Interfaces
{
    public interface IMaterialNormService
    {
        Task<IEnumerable<MaterialNormDTO>> GetAllMaterialNormByConstructionAsync(int id);
        Task<IEnumerable<MaterialNormItemDTO>> GetAllMaterialNormByConstructionItemAsync(int id);
    }
}
