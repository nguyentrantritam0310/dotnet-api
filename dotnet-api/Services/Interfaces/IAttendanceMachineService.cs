using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using Microsoft.AspNetCore.Http;

namespace dotnet_api.Services.Interfaces
{
    public interface IAttendanceMachineService
    {
        Task<AttendanceMachineDTO> CreateAttendanceMachineAsync(AttendanceMachineDTO attendanceMachineDTO);
        Task<AttendanceMachineDTO> GetAttendanceMachineByIdAsync(int id);
        Task<IEnumerable<AttendanceMachineDTO>> GetAllAttendanceMachinesAsync();
        Task<AttendanceMachineDTO> UpdateAttendanceMachineAsync(AttendanceMachineDTO attendanceMachineDTO);
        Task<AttendanceMachineDTO> DeleteAttendanceMachineAsync(int id);
    }
}
