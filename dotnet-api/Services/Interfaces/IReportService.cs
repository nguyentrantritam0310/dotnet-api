using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using Microsoft.AspNetCore.Http;

namespace dotnet_api.Services.Interfaces
{
    
    public interface IReportService
    {
        Task<ReportDTO> CreateReportAsync(ReportCreateDTO reportDTO, string employeeId);
        Task<ReportDTO> GetReportByIdAsync(int id);
        Task<IEnumerable<ReportDTO>> GetAllReportAsync();
        Task<IEnumerable<ReportDTO>> GetAllReportByThiCongAsync();
        Task<IEnumerable<ReportDTO>> GetAllReportByKiThuatAsync();
        Task<ReportDTO> UpdateReportAsync(ReportUpdateDTO reportDTO);
        Task<bool> DeleteReportAsync(int id);
        Task<string> SaveImageAsync(IFormFile file);
        Task DeleteImageAsync(string imagePath);
    }
}
