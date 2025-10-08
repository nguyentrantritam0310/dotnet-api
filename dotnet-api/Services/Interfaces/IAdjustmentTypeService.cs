using dotnet_api.DTOs;

namespace dotnet_api.Services.Interfaces
{
    public interface IAdjustmentTypeService
    {
        Task<IEnumerable<AdjustmentTypeDTO>> GetAllAdjustmentTypesAsync();
        Task<AdjustmentTypeDTO> GetAdjustmentTypeByIdAsync(int id);
    }
}

