using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using System.Diagnostics;
using System.Text.Json;
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
        }

        public async Task<FaceRegistrationResult> RegisterFaceAsync(string employeeId, byte[] imageBytes)
        {
            _logger.LogWarning("RegisterFaceAsync is deprecated. Use RegisterFaceEmbeddingAsync with embedding from frontend instead.");
            return new FaceRegistrationResult
            {
                Success = false,
                Message = "Method này đã bị deprecated. Vui lòng sử dụng endpoint RegisterFaceEmbedding với embedding từ frontend."
            };
        }

        public async Task<FaceRecognitionResult> RecognizeFaceAsync(byte[] imageBytes)
        {
            try
            {
                var tempImagePath = await SaveTempImageAsync(imageBytes, $"recognize_{DateTime.Now:yyyyMMddHHmmss}.jpg");
                try
                {
                    var pythonArgs = $"recognize {tempImagePath}";
                    var result = await RunPythonScriptAsync(pythonArgs);

                    if (result.Success && !string.IsNullOrEmpty(result.EmployeeId))
                    {
                        var employeeName = await GetEmployeeNameAsync(result.EmployeeId);
                        
                        _logger.LogInformation("Face recognition successful - EmployeeId: {EmployeeId}, Confidence: {Confidence}",
                            result.EmployeeId, result.Confidence);
                        return new FaceRecognitionResult
                        {
                            Success = true,
                            Message = "Nhận dạng thành công",
                            EmployeeId = result.EmployeeId,
                            EmployeeName = employeeName,
                            Confidence = result.Confidence
                        };
                    }

                    return new FaceRecognitionResult
                    {
                        Success = false,
                        Message = result.Message ?? "Không thể nhận dạng khuôn mặt",
                        Confidence = result.Confidence
                    };
                }
                finally
                {
                    DeleteTempFile(tempImagePath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error recognizing face");
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
                var registrationEntity = await _context.FaceRegistrations
                    .FirstOrDefaultAsync(fr => fr.EmployeeId == employeeId && fr.IsActive);
                
                if (registrationEntity != null)
                {
                    await _faceRegistrationService.DeleteFaceRegistrationAsync(registrationEntity.ID, employeeId);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error unregistering face - EmployeeId: {EmployeeId}", employeeId);
                return false;
            }
        }

        public async Task<List<RegisteredEmployee>> GetRegisteredEmployeesAsync()
        {
            try
            {
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
                _logger.LogError(ex, "Error getting registered employees");
                return new List<RegisteredEmployee>();
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
                _logger.LogError(ex, "Error checking if employee is registered - EmployeeId: {EmployeeId}", employeeId);
                return false;
            }
        }

        public async Task<bool> DetectFaceAsync(byte[] imageBytes)
        {
            try
            {
                var tempImagePath = await SaveTempImageAsync(imageBytes, $"detect_{DateTime.Now:yyyyMMddHHmmss}.jpg");
                try
                {
                    var pythonArgs = $"detect {tempImagePath}";
                    var result = await RunPythonScriptAsync(pythonArgs);
                    return result.Success;
                }
                finally
                {
                    DeleteTempFile(tempImagePath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error detecting face");
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
                    return JsonSerializer.Deserialize<PythonResult>(output) ?? 
                           new PythonResult { Success = false, Message = "Không thể parse kết quả từ Python" };
                }

                _logger.LogError("Python script error - ExitCode: {ExitCode}, Error: {Error}", process.ExitCode, error);
                return new PythonResult { Success = false, Message = error };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error running Python script");
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
                _logger.LogError(ex, "Error getting employee name - EmployeeId: {EmployeeId}", employeeId);
                return $"Nhân viên {employeeId}";
            }
        }

        private async Task<string> SaveTempImageAsync(byte[] imageBytes, string fileName)
        {
            var tempDir = Path.Combine(Path.GetTempPath(), "face_recognition");
            Directory.CreateDirectory(tempDir);
            var tempImagePath = Path.Combine(tempDir, fileName);
            await File.WriteAllBytesAsync(tempImagePath, imageBytes);
            return tempImagePath;
        }

        private void DeleteTempFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error deleting temp file: {FilePath}", filePath);
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
