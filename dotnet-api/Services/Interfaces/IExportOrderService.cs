using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_api.DTOs;

namespace dotnet_api.Services.Interfaces
{
    public interface IExportOrderService
    {
        Task<IEnumerable<ExportOrderDTO>> GetAllExportOrders();
        Task<ExportOrderDTO> GetExportOrderById(int id);
        Task<ExportOrderDTO> CreateExportOrder(ExportOrderDTO exportOrderDTO);
        Task<ExportOrderDTO> UpdateExportOrder(int id, ExportOrderDTO exportOrderDTO);
        Task<bool> DeleteExportOrder(int id);
    }
} 