using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_api.DTOs;

namespace dotnet_api.Services.Interfaces
{
    public interface IImportOrderService
    {
        Task<IEnumerable<ImportOrderDTO>> GetAllImportOrdersByManager();
        Task<IEnumerable<ImportOrderDTO>> GetAllImportOrdersByDirector();
        Task<ImportOrderDTO> GetImportOrderById(int id);
        Task<ImportOrderDTOPOST> CreateImportOrder(ImportOrderDTOPOST ImportOrderDTO);
        Task<ImportOrderDTO> UpdateImportOrderStatusAsync(int id, int status);
        //Task<ImportOrderDTO> UpdateImportOrder(int id, ImportOrderDTO ImportOrderDTO);
        //Task<bool> DeleteImportOrder(int id);
    }
} 