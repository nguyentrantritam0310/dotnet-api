using dotnet_api.DTOs;

namespace dotnet_api.Services.Interfaces
{
    public interface IAdjustmentItemService
    {
        Task<IEnumerable<AdjustmentItemDTO>> GetAllAdjustmentItemsAsync();
        Task<IEnumerable<AdjustmentItemDTO>> GetAdjustmentItemsByTypeIdAsync(int adjustmentTypeId);
        Task<AdjustmentItemDTO> GetAdjustmentItemByIdAsync(int id);
    }
}

