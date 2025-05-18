using dotnet_api.Data.Entities;
using dotnet_api.DTOs;

namespace dotnet_api.Services.Interfaces
{

    public interface IMaterialService
    {
        Task<MaterialDTO> CreateMaterialAsync(MaterialDTOPOST Material);
        Task<MaterialDTO> GetMaterialByIdAsync(int id);
        Task<IEnumerable<MaterialDTO>> GetAllMaterialAsync();
        Task<MaterialDTO> UpdateMaterialAsync(MaterialDTO Material);
        Task<MaterialUpdateStockQuantityDTO> UpdateStockQuantityMaterialAsync(MaterialUpdateStockQuantityDTO materialUpdateStockQuantityDTO);
        Task<bool> DeleteMaterialAsync(int id);
    }
}
