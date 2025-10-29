using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using System.Diagnostics;
using System.Text.Json;
using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace dotnet_api.Services
{
    public class FaceRecognitionService : IFaceRecognitionService
    {
        private readonly ILogger<FaceRecognitionService> _logger;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly IFaceRegistrationService _faceRegistrationService;
        private readonly string _pythonScriptPath;
        private readonly string _faceDatabasePath;

        public FaceRecognitionService(
            ILogger<FaceRecognitionService> logger, 
            IConfiguration configuration,
            ApplicationDbContext context,
            IFaceRegistrationService faceRegistrationService)
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;
            _faceRegistrationService = faceRegistrationService;
            _pythonScriptPath = Path.Combine(Directory.GetCurrentDirectory(), "MachineLearning", "face_recognition.py");
            _faceDatabasePath = Path.Combine(Directory.GetCurrentDirectory(), "MachineLearning", "face_database.json");
        }

        public async Task<FaceRegistrationResult> RegisterFaceAsync(string employeeId, byte[] imageBytes)
        {
            try
            {
                _logger.LogInformation($"Bắt đầu đăng ký khuôn mặt cho nhân viên: {employeeId}");

                // Tạo thư mục tạm để lưu ảnh
                var tempDir = Path.Combine(Path.GetTempPath(), "face_recognition");
                Directory.CreateDirectory(tempDir);
                
                var tempImagePath = Path.Combine(tempDir, $"temp_{employeeId}_{DateTime.Now:yyyyMMddHHmmss}.jpg");
                
                // Lưu ảnh tạm
                await File.WriteAllBytesAsync(tempImagePath, imageBytes);

                // Gọi Python script để xử lý
                var pythonArgs = $"register {tempImagePath} {employeeId}";
                var result = await RunPythonScriptAsync(pythonArgs);

                // Xóa file tạm
                if (File.Exists(tempImagePath))
                    File.Delete(tempImagePath);

                if (result.Success)
                {
                    var faceId = Guid.NewGuid().ToString();
                    var embeddingJson = JsonSerializer.Serialize(result.Embedding);
                    
                    // Lưu ảnh
                    var imagePath = await SaveFaceImageAsync(imageBytes, employeeId);
                    
                    // Lưu vào database
                    var faceRegistrationDto = new FaceRegistrationDTO
                    {
                        EmployeeId = employeeId,
                        FaceId = faceId,
                        ImagePath = imagePath,
                        EmbeddingData = embeddingJson,
                        Confidence = result.Confidence,
                        RegisteredBy = "System",
                        Notes = "Auto-registered via face recognition"
                    };

                    var createRequest = new CreateFaceRegistrationDTO
                    {
                        EmployeeId = employeeId,
                        FaceFeatures = JsonSerializer.Serialize(new
                        {
                            bounds = new { x = 0.2, y = 0.3, width = 0.6, height = 0.7 },
                            landmarks = new object[0],
                            contours = new object[0],
                            headEulerAngles = new { x = 0, y = 0, z = 0 },
                            probabilities = new { leftEyeOpenProbability = 0.9, rightEyeOpenProbability = 0.9, smilingProbability = 0.7 }
                        }),
                        Notes = "Auto-registered via face recognition"
                    };

                    // Create a temporary file for the image
                    var tempImageFile = new FormFile(
                        new MemoryStream(imageBytes), 
                        0, 
                        imageBytes.Length, 
                        "imageFile", 
                        $"face_{employeeId}_{DateTime.Now:yyyyMMddHHmmss}.jpg"
                    )
                    {
                        Headers = new Microsoft.AspNetCore.Http.HeaderDictionary(),
                        ContentType = "image/jpeg"
                    };

                    var savedRegistration = await _faceRegistrationService.RegisterFaceAsync(createRequest, tempImageFile);

                    _logger.LogInformation($"Đăng ký khuôn mặt thành công cho nhân viên: {employeeId}");
                    return new FaceRegistrationResult
                    {
                        Success = true,
                        Message = "Đăng ký khuôn mặt thành công",
                        FaceId = faceId,
                        Confidence = result.Confidence,
                        FaceRegistrationId = 1 // Will be updated when we get the actual ID
                    };
                }
                else
                {
                    _logger.LogWarning($"Đăng ký khuôn mặt thất bại cho nhân viên: {employeeId}, Lỗi: {result.Message}");
                    return new FaceRegistrationResult
                    {
                        Success = false,
                        Message = result.Message
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi đăng ký khuôn mặt cho nhân viên: {employeeId}");
                return new FaceRegistrationResult
                {
                    Success = false,
                    Message = $"Lỗi server: {ex.Message}"
                };
            }
        }

        public async Task<FaceRecognitionResult> RecognizeFaceAsync(byte[] imageBytes)
        {
            try
            {
                _logger.LogInformation("Bắt đầu nhận dạng khuôn mặt");

                // Tạo thư mục tạm để lưu ảnh
                var tempDir = Path.Combine(Path.GetTempPath(), "face_recognition");
                Directory.CreateDirectory(tempDir);
                
                var tempImagePath = Path.Combine(tempDir, $"recognize_{DateTime.Now:yyyyMMddHHmmss}.jpg");
                
                // Lưu ảnh tạm
                await File.WriteAllBytesAsync(tempImagePath, imageBytes);

                // Gọi Python script để nhận dạng
                var pythonArgs = $"recognize {tempImagePath}";
                var result = await RunPythonScriptAsync(pythonArgs);

                // Xóa file tạm
                if (File.Exists(tempImagePath))
                    File.Delete(tempImagePath);

                if (result.Success && !string.IsNullOrEmpty(result.EmployeeId))
                {
                    var employeeName = await GetEmployeeNameAsync(result.EmployeeId);
                    
                    _logger.LogInformation($"Nhận dạng thành công: {result.EmployeeId} với độ tin cậy: {result.Confidence}");
                    return new FaceRecognitionResult
                    {
                        Success = true,
                        Message = "Nhận dạng thành công",
                        EmployeeId = result.EmployeeId,
                        EmployeeName = employeeName,
                        Confidence = result.Confidence
                    };
                }
                else
                {
                    _logger.LogWarning($"Nhận dạng thất bại: {result.Message}");
                    return new FaceRecognitionResult
                    {
                        Success = false,
                        Message = result.Message ?? "Không thể nhận dạng khuôn mặt",
                        Confidence = result.Confidence
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi nhận dạng khuôn mặt");
                return new FaceRecognitionResult
                {
                    Success = false,
                    Message = $"Lỗi server: {ex.Message}"
                };
            }
        }

        public async Task<bool> UnregisterFaceAsync(string employeeId)
        {
            try
            {
                _logger.LogInformation($"Xóa khuôn mặt cho nhân viên: {employeeId}");

                // Get the actual entity to get the ID
                var registrationEntity = await _context.FaceRegistrations
                    .FirstOrDefaultAsync(fr => fr.EmployeeId == employeeId && fr.IsActive);
                
                if (registrationEntity != null)
                {
                    await _faceRegistrationService.DeleteFaceRegistrationAsync(registrationEntity.ID, employeeId);
                    _logger.LogInformation($"Xóa khuôn mặt thành công cho nhân viên: {employeeId}");
                    return true;
                }

                _logger.LogWarning($"Không tìm thấy khuôn mặt để xóa cho nhân viên: {employeeId}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi xóa khuôn mặt cho nhân viên: {employeeId}");
                return false;
            }
        }

        public async Task<List<RegisteredEmployee>> GetRegisteredEmployeesAsync()
        {
            try
            {
                // Get all face registrations from database directly
                var registrations = await _context.FaceRegistrations
                    .Where(fr => fr.IsActive)
                    .Include(fr => fr.Employee)
                    .ToListAsync();

                return registrations.Select(fr => new RegisteredEmployee
                {
                    Id = fr.ID,
                    EmployeeId = fr.EmployeeId,
                    EmployeeName = $"{fr.Employee.FirstName} {fr.Employee.LastName}",
                    RegisteredDate = fr.RegisteredDate,
                    FaceId = fr.FaceId,
                    Confidence = fr.Confidence,
                    IsActive = fr.IsActive
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách nhân viên đã đăng ký");
                return new List<RegisteredEmployee>();
            }
        }

        public async Task<bool> IsEmployeeRegisteredAsync(string employeeId)
        {
            try
            {
                // Check if employee has any active face registrations
                var hasRegistration = await _context.FaceRegistrations
                    .AnyAsync(fr => fr.EmployeeId == employeeId && fr.IsActive);
                return hasRegistration;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error checking if employee {employeeId} is registered");
                return false;
            }
        }

        public async Task<bool> DetectFaceAsync(byte[] imageBytes)
        {
            try
            {
                _logger.LogInformation("Bắt đầu phát hiện khuôn mặt");

                // Tạo thư mục tạm để lưu ảnh
                var tempDir = Path.Combine(Path.GetTempPath(), "face_recognition");
                Directory.CreateDirectory(tempDir);
                
                var tempImagePath = Path.Combine(tempDir, $"detect_{DateTime.Now:yyyyMMddHHmmss}.jpg");
                
                // Lưu ảnh tạm
                await File.WriteAllBytesAsync(tempImagePath, imageBytes);

                // Gọi Python script để phát hiện khuôn mặt
                var pythonArgs = $"detect {tempImagePath}";
                var result = await RunPythonScriptAsync(pythonArgs);

                // Xóa file tạm
                if (File.Exists(tempImagePath))
                    File.Delete(tempImagePath);

                return result.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi phát hiện khuôn mặt");
                return false;
            }
        }

        private async Task<PythonResult> RunPythonScriptAsync(string arguments)
        {
            try
            {
                var pythonPath = _configuration["PythonPath"] ?? "python";
                var startInfo = new ProcessStartInfo
                {
                    FileName = pythonPath,
                    Arguments = $"\"{_pythonScriptPath}\" {arguments}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = new Process { StartInfo = startInfo };
                process.Start();

                var output = await process.StandardOutput.ReadToEndAsync();
                var error = await process.StandardError.ReadToEndAsync();
                
                await process.WaitForExitAsync();

                if (process.ExitCode == 0)
                {
                    return JsonSerializer.Deserialize<PythonResult>(output) ?? new PythonResult { Success = false, Message = "Không thể parse kết quả từ Python" };
                }
                else
                {
                    _logger.LogError($"Python script lỗi: {error}");
                    return new PythonResult { Success = false, Message = error };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi chạy Python script");
                return new PythonResult { Success = false, Message = ex.Message };
            }
        }

        private async Task<string> GetEmployeeNameAsync(string employeeId)
        {
            try
            {
                var employee = await _context.Users.FindAsync(employeeId);
                return employee?.UserName ?? $"Nhân viên {employeeId}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting employee name for {employeeId}");
                return $"Nhân viên {employeeId}";
            }
        }

        private async Task<string> SaveFaceImageAsync(byte[] imageBytes, string employeeId)
        {
            try
            {
                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "faces");
                Directory.CreateDirectory(uploadsDir);

                var fileName = $"{employeeId}_{DateTime.Now:yyyyMMddHHmmss}.jpg";
                var filePath = Path.Combine(uploadsDir, fileName);

                await File.WriteAllBytesAsync(filePath, imageBytes);
                return $"/uploads/faces/{fileName}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error saving face image for employee: {employeeId}");
                return string.Empty;
            }
        }


        private class PythonResult
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public string EmployeeId { get; set; }
            public float Confidence { get; set; }
            public float[] Embedding { get; set; }
        }
    }
}
