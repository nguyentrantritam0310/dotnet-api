using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using System.Diagnostics;
using System.Text.Json;
using System.Collections.Concurrent;

namespace dotnet_api.Services
{
    public class FaceRecognitionService : IFaceRecognitionService
    {
        private readonly ILogger<FaceRecognitionService> _logger;
        private readonly IConfiguration _configuration;
        private readonly ConcurrentDictionary<string, FaceEmbedding> _faceEmbeddings;
        private readonly string _pythonScriptPath;
        private readonly string _faceDatabasePath;

        public FaceRecognitionService(ILogger<FaceRecognitionService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _faceEmbeddings = new ConcurrentDictionary<string, FaceEmbedding>();
            _pythonScriptPath = Path.Combine(Directory.GetCurrentDirectory(), "MachineLearning", "face_recognition.py");
            _faceDatabasePath = Path.Combine(Directory.GetCurrentDirectory(), "MachineLearning", "face_database.json");
            
            LoadFaceDatabase();
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
                    var embedding = new FaceEmbedding
                    {
                        EmployeeId = employeeId,
                        FaceId = faceId,
                        Embedding = result.Embedding,
                        CreatedDate = DateTime.Now
                    };

                    _faceEmbeddings.AddOrUpdate(employeeId, embedding, (key, oldValue) => embedding);
                    await SaveFaceDatabaseAsync();

                    _logger.LogInformation($"Đăng ký khuôn mặt thành công cho nhân viên: {employeeId}");
                    return new FaceRegistrationResult
                    {
                        Success = true,
                        Message = "Đăng ký khuôn mặt thành công",
                        FaceId = faceId,
                        Confidence = result.Confidence
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

                if (_faceEmbeddings.TryRemove(employeeId, out _))
                {
                    await SaveFaceDatabaseAsync();
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
                var employees = new List<RegisteredEmployee>();
                
                foreach (var embedding in _faceEmbeddings.Values)
                {
                    var employeeName = await GetEmployeeNameAsync(embedding.EmployeeId);
                    employees.Add(new RegisteredEmployee
                    {
                        EmployeeId = embedding.EmployeeId,
                        EmployeeName = employeeName,
                        RegisteredDate = embedding.CreatedDate,
                        FaceId = embedding.FaceId
                    });
                }

                return employees.OrderBy(e => e.EmployeeName).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách nhân viên đã đăng ký");
                return new List<RegisteredEmployee>();
            }
        }

        public async Task<bool> IsEmployeeRegisteredAsync(string employeeId)
        {
            return await Task.FromResult(_faceEmbeddings.ContainsKey(employeeId));
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
            // TODO: Implement database lookup for employee name
            // For now, return a placeholder
            return await Task.FromResult($"Nhân viên {employeeId}");
        }

        private void LoadFaceDatabase()
        {
            try
            {
                if (File.Exists(_faceDatabasePath))
                {
                    var json = File.ReadAllText(_faceDatabasePath);
                    var embeddings = JsonSerializer.Deserialize<Dictionary<string, FaceEmbedding>>(json);
                    
                    if (embeddings != null)
                    {
                        foreach (var embedding in embeddings)
                        {
                            _faceEmbeddings.TryAdd(embedding.Key, embedding.Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi load face database");
            }
        }

        private async Task SaveFaceDatabaseAsync()
        {
            try
            {
                var embeddings = _faceEmbeddings.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                var json = JsonSerializer.Serialize(embeddings, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_faceDatabasePath, json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi save face database");
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
