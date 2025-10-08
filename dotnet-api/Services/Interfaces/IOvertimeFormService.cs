using dotnet_api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_api.Services.Interfaces
{
    public interface IOvertimeFormService
    {
        Task<IEnumerable<OvertimeFormDTO>> GetAllOvertimeFormsAsync();
        Task<OvertimeFormDTO> GetOvertimeFormByIdAsync(int id);
    }
}




