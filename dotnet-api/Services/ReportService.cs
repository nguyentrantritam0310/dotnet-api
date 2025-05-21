using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.Data.Enums;
using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace dotnet_api.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private const string UPLOAD_DIRECTORY = "uploads/reports";

        public ReportService(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task<ReportDTO> CreateReportAsync(ReportCreateDTO reportDTO, string employeeId)
        {
            try
            {
                if (string.IsNullOrEmpty(employeeId))
                    throw new ArgumentException("EmployeeId không được để trống");

                var report = new Report
                {
                    EmployeeID = employeeId,
                    ConstructionID = reportDTO.ConstructionID,
                    ReportDate = DateTime.UtcNow,
                    ReportType = reportDTO.ReportType,
                    Content = reportDTO.Content,
                    Level = reportDTO.Level
                };

                _context.Reports.Add(report);
                await _context.SaveChangesAsync();

                // Handle image uploads
                if (reportDTO.Images != null && reportDTO.Images.Any())
                {
                    foreach (var image in reportDTO.Images)
                    {
                        if (image != null && image.Length > 0)
                        {
                            var imagePath = await SaveImageAsync(image);
                            var attachment = new ReportAttachment
                            {
                                ReportID = report.ID,
                                FilePath = imagePath,
                                UploadDate = DateTime.UtcNow
                            };
                            _context.ReportAttachments.Add(attachment);
                        }
                    }
                    await _context.SaveChangesAsync();
                }

                // Add initial status log
                var statusLog = new ReportStatusLog
                {
                    ReportID = report.ID,
                    Status = ReportStatusEnum.Pending,
                    ReportDate = DateTime.UtcNow,
                    Note = "Báo cáo mới được tạo"
                };
                _context.ReportStatusLogs.Add(statusLog);
                await _context.SaveChangesAsync();

                return await GetReportByIdAsync(report.ID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Lỗi khi tạo báo cáo: {ex.Message}", ex);
            }
        }

        public async Task<ReportDTO> UpdateReportAsync(ReportUpdateDTO reportDTO)
        {
            var report = await _context.Reports
                .Include(r => r.ReportAttachments)
                .FirstOrDefaultAsync(r => r.ID == reportDTO.ID);

            if (report == null)
            {
                return null;
            }

            // Update basic information
            report.Content = reportDTO.Content;
            report.Level = reportDTO.Level;

            // Handle deleted images only if DeletedImagePaths is provided
            if (reportDTO.DeletedImagePaths != null && reportDTO.DeletedImagePaths.Any())
            {
                foreach (var imagePath in reportDTO.DeletedImagePaths)
                {
                    var attachment = report.ReportAttachments.FirstOrDefault(a => a.FilePath == imagePath);
                    if (attachment != null)
                    {
                        _context.ReportAttachments.Remove(attachment);
                        await DeleteImageAsync(imagePath);
                    }
                }
            }

            // Handle new images only if NewImages is provided
            if (reportDTO.NewImages != null && reportDTO.NewImages.Any())
            {
                foreach (var image in reportDTO.NewImages)
                {
                    var imagePath = await SaveImageAsync(image);
                    var attachment = new ReportAttachment
                    {
                        ReportID = report.ID,
                        FilePath = imagePath,
                        UploadDate = DateTime.UtcNow
                    };
                    _context.ReportAttachments.Add(attachment);
                }
            }

            await _context.SaveChangesAsync();
            return await GetReportByIdAsync(report.ID);
        }

        public async Task<string> SaveImageAsync(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    throw new ArgumentException("Không có file được tải lên");

                // Tạo đường dẫn tới thư mục uploads trong thư mục hiện tại
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "reports");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Validate file extension
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {
                    throw new ArgumentException($"Loại file không được hỗ trợ. Chỉ chấp nhận: {string.Join(", ", allowedExtensions)}");
                }

                // Generate unique filename
                var fileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(uploadPath, fileName);

                // Save file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Trả về đường dẫn tương đối
                return Path.Combine("uploads", "reports", fileName).Replace("\\", "/");
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Lỗi khi lưu file: {ex.Message}", ex);
            }
        }

        public async Task DeleteImageAsync(string imagePath)
        {
            try
            {
                if (string.IsNullOrEmpty(imagePath))
                    return;

                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), imagePath.TrimStart('/'));
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Lỗi khi xóa file: {ex.Message}", ex);
            }
        }

        public async Task<ReportDTO> GetReportByIdAsync(int id)
        {
            var report = await _context.Reports
                .Include(r => r.Construction)
                .Include(r => r.Employee)
                .Include(r => r.ReportAttachments)
                .Include(r => r.ReportStatusLogs)
                .FirstOrDefaultAsync(r => r.ID == id);

            return report == null ? null : _mapper.Map<ReportDTO>(report);
        }

        public async Task<IEnumerable<ReportDTO>> GetAllReportAsync()
        {
            var reports = await _context.Reports
                .Include(r => r.Construction)
                .Include(r => r.Employee)
                .Include(r => r.ReportAttachments)
                .Include(r => r.ReportStatusLogs)
                .OrderByDescending(r => r.ReportDate)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ReportDTO>>(reports);
        }

        public async Task<IEnumerable<ReportDTO>> GetAllReportByKiThuatAsync()
        {
            var reports = await _context.Reports
                .Include(r => r.Construction)
                .Include(r => r.Employee)
                .Include(r => r.ReportAttachments)
                .Include(r => r.ReportStatusLogs)
                .Where(r => r.ReportType == "Sự cố kĩ thuật")
                .OrderByDescending(r => r.ReportDate)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ReportDTO>>(reports);
        }

        public async Task<IEnumerable<ReportDTO>> GetAllReportByThiCongAsync()
        {
            var reports = await _context.Reports
                .Include(r => r.Construction)
                .Include(r => r.Employee)
                .Include(r => r.ReportAttachments)
                .Include(r => r.ReportStatusLogs)
                .Where(r => r.ReportType == "Sự cố thi công")
                .OrderByDescending(r => r.ReportDate)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ReportDTO>>(reports);
        }

        public async Task<bool> DeleteReportAsync(int id)
        {
            var report = await _context.Reports
                .Include(r => r.ReportAttachments)
                .FirstOrDefaultAsync(r => r.ID == id);

            if (report == null)
            {
                return false;
            }

            // Delete associated images
            foreach (var attachment in report.ReportAttachments)
            {
                await DeleteImageAsync(attachment.FilePath);
            }

            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ReportDTO> UpdateReportStatusAsync(int id, ReportUpdateStatusDTO statusDTO)
        {
            var report = await _context.Reports
                .Include(r => r.ReportStatusLogs)
                .FirstOrDefaultAsync(r => r.ID == id);

            if (report == null)
                return null;

            // Add new status log
            var statusLog = new ReportStatusLog
            {
                ReportID = report.ID,
                Status = statusDTO.Status,
                ReportDate = DateTime.UtcNow,
                Note = statusDTO.Note ?? $"Trạng thái đã được cập nhật thành {statusDTO.Status}"
            };

            _context.ReportStatusLogs.Add(statusLog);
            await _context.SaveChangesAsync();

            return await GetReportByIdAsync(report.ID);
        }
    }
}
