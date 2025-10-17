using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Services
{
    public class FaceRegistrationService : IFaceRegistrationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FaceRegistrationService> _logger;

        public FaceRegistrationService(ApplicationDbContext context, ILogger<FaceRegistrationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<FaceRegistrationDTO> CreateFaceRegistrationAsync(FaceRegistrationDTO dto)
        {
            try
            {
                // Kiểm tra nhân viên đã đăng ký chưa
                var existingRegistration = await _context.FaceRegistrations
                    .FirstOrDefaultAsync(fr => fr.EmployeeId == dto.EmployeeId && fr.IsActive);

                if (existingRegistration != null)
                {
                    // Cập nhật thông tin đăng ký cũ
                    existingRegistration.FaceId = dto.FaceId;
                    existingRegistration.ImagePath = dto.ImagePath;
                    existingRegistration.EmbeddingData = dto.EmbeddingData;
                    existingRegistration.Confidence = dto.Confidence;
                    existingRegistration.LastUpdated = DateTime.Now;
                    existingRegistration.RegisteredBy = dto.RegisteredBy;
                    existingRegistration.Notes = dto.Notes;

                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Updated face registration for employee: {dto.EmployeeId}");
                    return dto;
                }

                // Tạo đăng ký mới
                var faceRegistration = new FaceRegistration
                {
                    EmployeeId = dto.EmployeeId,
                    FaceId = dto.FaceId,
                    ImagePath = dto.ImagePath,
                    EmbeddingData = dto.EmbeddingData,
                    Confidence = dto.Confidence,
                    RegisteredDate = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    IsActive = true,
                    RegisteredBy = dto.RegisteredBy,
                    Notes = dto.Notes
                };

                _context.FaceRegistrations.Add(faceRegistration);
                await _context.SaveChangesAsync();

                dto.EmployeeId = faceRegistration.EmployeeId; // Return updated info
                _logger.LogInformation($"Created face registration for employee: {dto.EmployeeId}");
                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating face registration for employee: {dto.EmployeeId}");
                throw;
            }
        }

        public async Task<FaceRegistrationDTO?> GetFaceRegistrationByEmployeeIdAsync(string employeeId)
        {
            try
            {
                var registration = await _context.FaceRegistrations
                    .Include(fr => fr.Employee)
                    .FirstOrDefaultAsync(fr => fr.EmployeeId == employeeId && fr.IsActive);

                if (registration == null)
                    return null;

                return new FaceRegistrationDTO
                {
                    EmployeeId = registration.EmployeeId,
                    FaceId = registration.FaceId,
                    ImagePath = registration.ImagePath,
                    EmbeddingData = registration.EmbeddingData,
                    Confidence = registration.Confidence,
                    RegisteredBy = registration.RegisteredBy,
                    Notes = registration.Notes
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting face registration for employee: {employeeId}");
                throw;
            }
        }

        public async Task<List<RegisteredEmployee>> GetAllRegisteredEmployeesAsync()
        {
            try
            {
                var registrations = await _context.FaceRegistrations
                    .Include(fr => fr.Employee)
                    .Where(fr => fr.IsActive)
                    .OrderBy(fr => fr.Employee.UserName)
                    .ToListAsync();

                return registrations.Select(fr => new RegisteredEmployee
                {
                    Id = fr.ID,
                    EmployeeId = fr.EmployeeId,
                    EmployeeName = fr.Employee?.UserName ?? $"Employee {fr.EmployeeId}",
                    RegisteredDate = fr.RegisteredDate,
                    FaceId = fr.FaceId,
                    Confidence = fr.Confidence,
                    IsActive = fr.IsActive
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all registered employees");
                throw;
            }
        }

        public async Task<FaceRegistrationDTO> UpdateFaceRegistrationAsync(int id, FaceRegistrationDTO dto)
        {
            try
            {
                var registration = await _context.FaceRegistrations.FindAsync(id);
                if (registration == null)
                    throw new ArgumentException($"Face registration with ID {id} not found");

                registration.FaceId = dto.FaceId;
                registration.ImagePath = dto.ImagePath;
                registration.EmbeddingData = dto.EmbeddingData;
                registration.Confidence = dto.Confidence;
                registration.LastUpdated = DateTime.Now;
                registration.RegisteredBy = dto.RegisteredBy;
                registration.Notes = dto.Notes;

                await _context.SaveChangesAsync();
                _logger.LogInformation($"Updated face registration ID: {id}");
                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating face registration ID: {id}");
                throw;
            }
        }

        public async Task<bool> DeleteFaceRegistrationAsync(int id)
        {
            try
            {
                var registration = await _context.FaceRegistrations.FindAsync(id);
                if (registration == null)
                    return false;

                registration.IsActive = false;
                registration.LastUpdated = DateTime.Now;
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Deleted face registration ID: {id}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting face registration ID: {id}");
                throw;
            }
        }

        public async Task<bool> IsEmployeeRegisteredAsync(string employeeId)
        {
            try
            {
                return await _context.FaceRegistrations
                    .AnyAsync(fr => fr.EmployeeId == employeeId && fr.IsActive);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error checking if employee {employeeId} is registered");
                throw;
            }
        }
    }
}

