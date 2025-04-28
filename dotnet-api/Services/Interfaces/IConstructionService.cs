using dotnet_api.DTOs;

namespace dotnet_api.Services.Interfaces
{
    public interface IConstructionService
    {
        Task<ConstructionDTO> CreateConstructionAsync(ConstructionDTO construction);
        Task<ConstructionDTO> GetConstructionByIdAsync(int id);
        Task<IEnumerable<ConstructionDTO>> GetAllConstructionsAsync();
        Task<ConstructionDTO> UpdateConstructionAsync(ConstructionDTO construction);
        Task<bool> DeleteConstructionAsync(int id);
    }
}
