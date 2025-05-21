using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_api.DTOs;

namespace dotnet_api.Services.Interfaces
{
    public interface IImportOrderEmployeeService
    {
        Task<ImportOrderEmployeeDTOPOST> CreateImportOrderEmployee(ImportOrderEmployeeDTOPOST ImportOrderEmployeeDTO);
        Task<ImportOrderEmployeeDTO> GetImportOrderEmployeeById(int importOrderID, string employeeID);
    }
} 