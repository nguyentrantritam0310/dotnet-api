using dotnet_api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_api.Services.Interfaces
{
    public interface IOvertimeTypeService
    {
        Task<IEnumerable<OvertimeTypeDTO>> GetAllOvertimeTypesAsync();
        Task<OvertimeTypeDTO> GetOvertimeTypeByIdAsync(int id);
    }
}




