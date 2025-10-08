using dotnet_api.DTOs;

namespace dotnet_api.Services.Interfaces
{
    public interface ILeaveTypeService
    {
        Task<IEnumerable<LeaveTypeDTO>> GetAllLeaveTypesAsync();
        Task<LeaveTypeDTO> GetLeaveTypeByIdAsync(int id);
    }
}
