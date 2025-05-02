using dotnet_api.Data.Entities;
using dotnet_api.DTOs;

namespace dotnet_api.Services.Interfaces
{

    public interface IMaterialService
    {
        Task<MaterialDTO> CreateMaterialAsync(MaterialDTO Material);
        Task<MaterialDTO> GetMaterialByIdAsync(int id);
        Task<IEnumerable<MaterialDTO>> GetAllMaterialAsync();
        Task<MaterialDTO> UpdateMaterialAsync(MaterialDTO Material);
        Task<bool> DeleteMaterialAsync(int id);
    }
}
