using dotnet_api.DTOs;

namespace dotnet_api.Services.Interfaces
{
    public interface IConstructionItemService
    {
        Task<ConstructionItemDTO> CreateConstructionItemAsync(ConstructionItemDTO constructionItem);
        Task<ConstructionItemDTO> GetConstructionItemByIdAsync(int id);
        Task<IEnumerable<ConstructionItemDTO>> GetAllConstructionsItemAsync();
        Task<ConstructionItemDTO> UpdateConstructionItemAsync(ConstructionItemDTO constructionItem);
        Task<bool> DeleteConstructionItemAsync(int id);
    }
}
