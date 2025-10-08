using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.DTOs.PUT;

namespace dotnet_api.Services.Interfaces
{
    public interface IShiftAssignmentService
    {
        Task<IEnumerable<ShiftAssignmentDTO>> GetAllShiftAssignmentsAsync();
        Task<ShiftAssignmentDTO> GetShiftAssignmentByIdAsync(int id);
        Task<IEnumerable<ShiftAssignmentDTO>> GetShiftAssignmentsByEmployeeIdAsync(string employeeId);
        Task<IEnumerable<ShiftAssignmentDTO>> GetShiftAssignmentsByDateAsync(DateTime date);
        Task<IEnumerable<ShiftAssignmentDTO>> GetShiftAssignmentsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<ShiftScheduleDTO>> GetShiftScheduleByWeekAsync(DateTime weekStartDate);
        Task<ShiftAssignmentDTO> CreateShiftAssignmentAsync(ShiftAssignmentDTOPOST shiftAssignmentDTO);
        Task<ShiftAssignmentDTO> UpdateShiftAssignmentAsync(ShiftAssignmentDTOPUT shiftAssignmentDTO);
        Task<bool> DeleteShiftAssignmentAsync(int id);
    }
}

