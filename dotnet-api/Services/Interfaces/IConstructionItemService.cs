using dotnet_api.DTOs;

namespace dotnet_api.Services.Interfaces
{
    public interface IConstructionItemService
    {
        Task<ConstructionItemDTO> CreateConstructionItemAsync(ConstructionItemCreateDTO itemDTO);
        Task<ConstructionItemDTO> GetConstructionItemByIdAsync(int id);
        Task<IEnumerable<ConstructionItemDTO>> GetConstructionItemsByConstructionIdAsync(int constructionId);
        Task<ConstructionItemDTO> UpdateConstructionItemAsync(ConstructionItemUpdateDTO itemDTO);
        Task<ConstructionItemDTO> UpdateConstructionItemStatusAsync(int id, int status);
        Task DeleteConstructionItemAsync(int id);
    }
}
