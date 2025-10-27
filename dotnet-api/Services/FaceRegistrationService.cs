using Microsoft.EntityFrameworkCore;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using System.Text.Json;

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

        public async Task<FaceRegistrationResultDTO> RegisterFaceAsync(CreateFaceRegistrationDTO request)
        {
            try
            {
                // Check if user exists
                var user = await _context.ApplicationUsers
                    .FirstOrDefaultAsync(u => u.Id == request.EmployeeId);
                
                if (user == null)
                {
                    return new FaceRegistrationResultDTO
                    {
                        Success = false,
                        Message = "Người dùng không tồn tại"
                    };
                }

                // Check if user already has too many face registrations (limit to 5)
                var existingCount = await _context.FaceRegistrations
                    .CountAsync(fr => fr.EmployeeId == request.EmployeeId && fr.IsActive);
                
                if (existingCount >= 5)
                {
                    return new FaceRegistrationResultDTO
                    {
                        Success = false,
                        Message = "Bạn đã đăng ký tối đa 5 khuôn mặt. Vui lòng xóa một khuôn mặt cũ trước khi đăng ký mới."
                    };
                }

                // Generate unique face ID
                var faceId = $"FACE_{request.EmployeeId}_{DateTime.Now:yyyyMMddHHmmss}";

                // Save image to file system
                var imagePath = await SaveImageToFileSystem(request.ImageBase64, faceId);

                // For now, we'll use a mock embedding data
                // In production, you would call a face recognition service here
                var mockEmbedding = GenerateMockEmbedding();
                var confidence = 0.95f; // Mock confidence

                var faceRegistration = new FaceRegistration
                {
                    EmployeeId = request.EmployeeId,
                    FaceId = faceId,
                    ImagePath = imagePath,
                    EmbeddingData = JsonSerializer.Serialize(mockEmbedding),
                    Confidence = confidence,
                    RegisteredDate = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow,
                    IsActive = true,
                    RegisteredBy = request.EmployeeId,
                    Notes = request.Notes ?? ""
                };

                _context.FaceRegistrations.Add(faceRegistration);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Face registration successful for user {request.EmployeeId} with face ID {faceId}");

                return new FaceRegistrationResultDTO
                {
                    Success = true,
                    Message = "Đăng ký khuôn mặt thành công",
                    FaceRegistration = new FaceRegistrationDTO
                    {
                        ID = faceRegistration.ID,
                        EmployeeId = faceRegistration.EmployeeId,
                        FaceId = faceRegistration.FaceId,
                        ImagePath = faceRegistration.ImagePath,
                        EmbeddingData = faceRegistration.EmbeddingData,
                        Confidence = faceRegistration.Confidence,
                        RegisteredDate = faceRegistration.RegisteredDate,
                        LastUpdated = faceRegistration.LastUpdated,
                        IsActive = faceRegistration.IsActive,
                        RegisteredBy = faceRegistration.RegisteredBy,
                        Notes = faceRegistration.Notes,
                        EmployeeName = $"{user.FirstName} {user.LastName}",
                        EmployeeEmail = user.Email
                    },
                    Confidence = confidence,
                    FaceId = faceId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error registering face for user {request.EmployeeId}");
                return new FaceRegistrationResultDTO
                {
                    Success = false,
                    Message = "Có lỗi xảy ra khi đăng ký khuôn mặt"
                };
            }
        }

        public async Task<FaceVerificationResultDTO> VerifyFaceAsync(FaceVerificationRequestDTO request)
        {
            try
            {
                // Get user's face registrations
                var faceRegistrations = await _context.FaceRegistrations
                    .Where(fr => fr.EmployeeId == request.EmployeeId && fr.IsActive)
                    .ToListAsync();

                if (!faceRegistrations.Any())
                {
                    return new FaceVerificationResultDTO
                    {
                        Success = false,
                        Message = "Người dùng chưa đăng ký khuôn mặt",
                        IsMatch = false
                    };
                }

                // For now, we'll use a mock verification
                // In production, you would compare embeddings here
                var mockConfidence = 0.88f;
                var isMatch = mockConfidence > 0.8f; // Threshold for match

                var user = await _context.ApplicationUsers
                    .FirstOrDefaultAsync(u => u.Id == request.EmployeeId);

                return new FaceVerificationResultDTO
                {
                    Success = true,
                    Message = isMatch ? "Nhận diện khuôn mặt thành công" : "Không nhận diện được khuôn mặt",
                    Confidence = mockConfidence,
                    IsMatch = isMatch,
                    MatchedFaceId = isMatch ? faceRegistrations.First().FaceId : null,
                    EmployeeName = user != null ? $"{user.FirstName} {user.LastName}" : ""
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error verifying face for user {request.EmployeeId}");
                return new FaceVerificationResultDTO
                {
                    Success = false,
                    Message = "Có lỗi xảy ra khi nhận diện khuôn mặt",
                    IsMatch = false
                };
            }
        }

        public async Task<List<FaceRegistrationListDTO>> GetUserFaceRegistrationsAsync(string employeeId)
        {
            try
            {
                var registrations = await _context.FaceRegistrations
                    .Where(fr => fr.EmployeeId == employeeId && fr.IsActive)
                    .Include(fr => fr.Employee)
                    .OrderByDescending(fr => fr.RegisteredDate)
                    .Select(fr => new FaceRegistrationListDTO
                    {
                        ID = fr.ID,
                        FaceId = fr.FaceId,
                        Confidence = fr.Confidence,
                        RegisteredDate = fr.RegisteredDate,
                        IsActive = fr.IsActive,
                        Notes = fr.Notes,
                        EmployeeName = $"{fr.Employee.FirstName} {fr.Employee.LastName}"
                    })
                    .ToListAsync();

                return registrations;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting face registrations for user {employeeId}");
                return new List<FaceRegistrationListDTO>();
            }
        }

        public async Task<bool> DeleteFaceRegistrationAsync(int faceRegistrationId, string employeeId)
        {
            try
            {
                var registration = await _context.FaceRegistrations
                    .FirstOrDefaultAsync(fr => fr.ID == faceRegistrationId && fr.EmployeeId == employeeId);

                if (registration == null)
                    return false;

                registration.IsActive = false;
                registration.LastUpdated = DateTime.UtcNow;
                
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting face registration {faceRegistrationId} for user {employeeId}");
                return false;
            }
        }

        public async Task<FaceRegistrationDTO> GetFaceRegistrationByIdAsync(int id)
        {
            try
            {
                var registration = await _context.FaceRegistrations
                    .Include(fr => fr.Employee)
                    .FirstOrDefaultAsync(fr => fr.ID == id && fr.IsActive);

                if (registration == null)
                    return null;

                return new FaceRegistrationDTO
                {
                    ID = registration.ID,
                    EmployeeId = registration.EmployeeId,
                    FaceId = registration.FaceId,
                    ImagePath = registration.ImagePath,
                    EmbeddingData = registration.EmbeddingData,
                    Confidence = registration.Confidence,
                    RegisteredDate = registration.RegisteredDate,
                    LastUpdated = registration.LastUpdated,
                    IsActive = registration.IsActive,
                    RegisteredBy = registration.RegisteredBy,
                    Notes = registration.Notes,
                    EmployeeName = $"{registration.Employee.FirstName} {registration.Employee.LastName}",
                    EmployeeEmail = registration.Employee.Email
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting face registration {id}");
                return null;
            }
        }

        public async Task<bool> UpdateFaceRegistrationAsync(int id, string notes, string employeeId)
        {
            try
            {
                var registration = await _context.FaceRegistrations
                    .FirstOrDefaultAsync(fr => fr.ID == id && fr.EmployeeId == employeeId && fr.IsActive);

                if (registration == null)
                    return false;

                registration.Notes = notes;
                registration.LastUpdated = DateTime.UtcNow;
                
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating face registration {id} for user {employeeId}");
                return false;
            }
        }

        private async Task<string> SaveImageToFileSystem(string base64Image, string faceId)
        {
            try
            {
                // Remove data URL prefix if present
                if (base64Image.Contains(","))
                {
                    base64Image = base64Image.Split(',')[1];
                }

                var imageBytes = Convert.FromBase64String(base64Image);
                var fileName = $"{faceId}.jpg";
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "face_registrations");
                
                // Create directory if it doesn't exist
                if (!Directory.Exists(uploadsPath))
                {
                    Directory.CreateDirectory(uploadsPath);
                }

                var filePath = Path.Combine(uploadsPath, fileName);
                await File.WriteAllBytesAsync(filePath, imageBytes);

                return $"uploads/face_registrations/{fileName}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error saving image for face ID {faceId}");
                throw;
            }
        }

        private List<float> GenerateMockEmbedding()
        {
            // Generate a mock 512-dimensional embedding
            var random = new Random();
            var embedding = new List<float>();
            
            for (int i = 0; i < 512; i++)
            {
                embedding.Add((float)(random.NextDouble() * 2 - 1)); // Random values between -1 and 1
            }
            
            return embedding;
        }
    }
}