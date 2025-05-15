using dotnet_api.Data.Entities;
using dotnet_api.DTOs;

namespace dotnet_api.Services.Interfaces
{
    
    public interface IMaterial_ExportOrderService
    {
        Task<IEnumerable<Material_ExportOrderDTO>> GetMaterial_ExportOrderById(int id);
        Task<Material_ExportOrderDTO> CreateMaterial_ExportOrderAsync(Material_ExportOrderDTO report);
    }
}
