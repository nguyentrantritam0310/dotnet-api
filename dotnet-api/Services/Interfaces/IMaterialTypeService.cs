using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;

namespace dotnet_api.Services.Interfaces
{
    public interface IMaterialTypeService
    {
        Task<MaterialTypeDTO> GetMaterialTypeByIdAsync(int id);
        Task<IEnumerable<MaterialTypeDTO>> GetAllMaterialTypeAsync();

    }
}