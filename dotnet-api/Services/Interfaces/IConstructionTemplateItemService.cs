using dotnet_api.DTOs;

namespace dotnet_api.Services.Interfaces
{
    public interface IConstructionTemplateItemService
    {
        Task<IEnumerable<ConstructionTemplateItemDTO>> GetConstructionTemplateItemByConstructionTypeIdAsync(int id);
        Task<IEnumerable<ConstructionTemplateItemDTO>> GetAllConstructionsTemplateItemAsync();
    }
}
