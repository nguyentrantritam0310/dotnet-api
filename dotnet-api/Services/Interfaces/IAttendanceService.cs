using dotnet_api.Data.Entities;
using dotnet_api.DTOs;

namespace dotnet_api.Services.Interfaces
{

    public interface IAttendanceService
    {
        Task<AttendanceDTO> CreateAttendanceAsync(AttendanceDTO AttendanceDTO);
        Task<IEnumerable<AttendanceDTOGET>> GetAttendanceByIdAsync(int id);
        Task<IEnumerable<AttendanceDTOGET>> GetAllAttendanceAsync();
        Task<bool> DeleteAttendanceAsync(int id);
        Task<bool> DeleteAttendanceByEmployeeAndTaskAsync(string employeeId, int taskId);
        Task<bool> UpdateAttendanceStatusByEmployeeAsync(UpdateAttendanceStatusDTO dto);
    }
}
