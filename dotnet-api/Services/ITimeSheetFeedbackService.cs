using dotnet_api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_api.Services
{
    public interface ITimeSheetFeedbackService
    {
        Task<IEnumerable<TimeSheetFeedbackDto>> GetTimeSheetFeedbacksByEmployeeAsync(string employeeId);
        Task<TimeSheetFeedbackDto> GetTimeSheetFeedbackAsync(int timeSheetId, string employeeId);
        Task<TimeSheetFeedbackDto> CreateTimeSheetFeedbackAsync(CreateTimeSheetFeedbackDto createDto, string employeeId);
        Task<TimeSheetFeedbackDto> UpdateTimeSheetFeedbackAsync(UpdateTimeSheetFeedbackDto updateDto, string employeeId);
        Task<bool> DeleteTimeSheetFeedbackAsync(int timeSheetId, string employeeId);
        Task<IEnumerable<TimeSheetFeedbackDto>> GetTimeSheetFeedbacksByTimeSheetAsync(int timeSheetId);
    }
}
