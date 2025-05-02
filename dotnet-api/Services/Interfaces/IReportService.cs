using dotnet_api.Data.Entities;
using dotnet_api.DTOs;

namespace dotnet_api.Services.Interfaces
{
    
    public interface IReportService
    {
        Task<ReportDTO> CreateReportAsync(ReportDTO report);
        Task<ReportDTO> GetReportByIdAsync(int id);
        Task<IEnumerable<ReportDTO>> GetAllReportAsync();
        Task<ReportDTO> UpdateReportAsync(ReportDTO report);
        Task<bool> DeleteReportAsync(int id);
    }
}
