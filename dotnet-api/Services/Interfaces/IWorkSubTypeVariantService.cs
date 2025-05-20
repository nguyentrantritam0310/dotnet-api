using dotnet_api.DTOs;

namespace dotnet_api.Services.Interfaces
{
    public interface IWorkSubTypeVariantService
    {
        Task<IEnumerable<WorkSubTypeVariantDTO>> GetAllWorkSubTypeVariantsAsync();
        Task<WorkSubTypeVariantDTO> GetWorkSubTypeVariantByIdAsync(int id);
    }
} 