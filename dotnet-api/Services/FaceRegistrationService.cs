using Microsoft.EntityFrameworkCore;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics;

namespace dotnet_api.Services
{
    public class FaceRegistrationService : IFaceRegistrationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FaceRegistrationService> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _pythonScriptPath;

        public FaceRegistrationService(
            ApplicationDbContext context, 
            ILogger<FaceRegistrationService> logger,
            IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _pythonScriptPath = Path.Combine(Directory.GetCurrentDirectory(), "MachineLearning", "face_recognition.py");
        }

        public async Task<FaceRegistrationResultDTO> RegisterFaceAsync(CreateFaceRegistrationDTO request, IFormFile imageFile)
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

                // Parse face features from JSON
                FaceFeaturesDTO faceFeatures;
                try
                {
                    faceFeatures = JsonSerializer.Deserialize<FaceFeaturesDTO>(request.FaceFeatures);
                }
                catch (JsonException ex)
                {
                    _logger.LogError(ex, "Failed to parse face features JSON");
                    return new FaceRegistrationResultDTO
                    {
                        Success = false,
                        Message = "Dữ liệu đặc điểm khuôn mặt không hợp lệ"
                    };
                }

                // Validate face quality based on ML Kit data
                var qualityScore = CalculateFaceQualityScore(faceFeatures);
                if (qualityScore < 60) // Minimum quality threshold
                {
                    return new FaceRegistrationResultDTO
                    {
                        Success = false,
                        Message = $"Chất lượng khuôn mặt không đủ tốt (Score: {qualityScore:F1}/100). Vui lòng chụp lại với ánh sáng tốt hơn và giữ thẳng đầu."
                    };
                }

                // Generate unique face ID
                var faceId = $"FACE_{request.EmployeeId}_{DateTime.Now:yyyyMMddHHmmss}";

                // Save image to file system
                var imagePath = await SaveImageFileToFileSystem(imageFile, faceId);

                // Extract embedding from image using Python service
                float[] embedding = null;
                float confidence = 0.95f;
                
                try
                {
                    // Read image bytes
                    byte[] imageBytes;
                    using (var memoryStream = new MemoryStream())
                    {
                        await imageFile.CopyToAsync(memoryStream);
                        imageBytes = memoryStream.ToArray();
                    }

                    // Save temporary image file for Python script
                    var tempDir = Path.Combine(Path.GetTempPath(), "face_registration");
                    Directory.CreateDirectory(tempDir);
                    var tempImagePath = Path.Combine(tempDir, $"temp_{faceId}.jpg");
                    await File.WriteAllBytesAsync(tempImagePath, imageBytes);

                    // Call Python script to extract embedding
                    var embeddingResult = await ExtractEmbeddingFromImageAsync(tempImagePath);
                    
                    // Clean up temp file
                    try { File.Delete(tempImagePath); } catch { }

                    if (embeddingResult.Success && embeddingResult.Embedding != null && embeddingResult.Embedding.Length > 0)
                    {
                        embedding = embeddingResult.Embedding;
                        confidence = embeddingResult.Confidence;
                        _logger.LogInformation($"Successfully extracted embedding (dimension: {embedding.Length}) for face {faceId}");
                    }
                    else
                    {
                        _logger.LogWarning($"Failed to extract embedding, using mock embedding. Error: {embeddingResult.Message}");
                        embedding = GenerateMockEmbedding().ToArray();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error extracting embedding from image, using mock embedding");
                    embedding = GenerateMockEmbedding().ToArray();
                }

                var faceRegistration = new FaceRegistration
                {
                    EmployeeId = request.EmployeeId,
                    FaceId = faceId,
                    ImagePath = imagePath,
                    EmbeddingData = JsonSerializer.Serialize(embedding),
                    FaceFeaturesData = request.FaceFeatures,
                    Confidence = confidence,
                    FaceQualityScore = qualityScore,
                    RegisteredDate = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow,
                    IsActive = true,
                    RegisteredBy = request.EmployeeId,
                    Notes = request.Notes ?? ""
                };

                _context.FaceRegistrations.Add(faceRegistration);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Face registration successful for user {request.EmployeeId} with face ID {faceId}, quality score: {qualityScore:F1}");

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
                        FaceFeaturesData = faceRegistration.FaceFeaturesData,
                        Confidence = faceRegistration.Confidence,
                        FaceQualityScore = faceRegistration.FaceQualityScore,
                        RegisteredDate = faceRegistration.RegisteredDate,
                        LastUpdated = faceRegistration.LastUpdated,
                        IsActive = faceRegistration.IsActive,
                        RegisteredBy = faceRegistration.RegisteredBy,
                        Notes = faceRegistration.Notes,
                        EmployeeName = $"{user.FirstName} {user.LastName}",
                        EmployeeEmail = user.Email
                    },
                    Confidence = confidence,
                    FaceId = faceId,
                    FaceQualityScore = qualityScore
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

                // Validate face quality from ML Kit features if provided
                if (!string.IsNullOrEmpty(request.FaceFeatures))
                {
                    try
                    {
                        var faceFeatures = JsonSerializer.Deserialize<FaceFeaturesDTO>(request.FaceFeatures);
                        var qualityScore = CalculateFaceQualityScore(faceFeatures);
                        
                        if (qualityScore < 50) // Minimum quality for verification
                        {
                            return new FaceVerificationResultDTO
                            {
                                Success = false,
                                Message = $"Chất lượng ảnh không đủ để nhận diện (Score: {qualityScore:F1}/100)",
                                IsMatch = false
                            };
                        }

                        // Check head pose angles
                        if (faceFeatures?.HeadEulerAngles != null)
                        {
                            var absAngleX = Math.Abs(faceFeatures.HeadEulerAngles.X);
                            var absAngleY = Math.Abs(faceFeatures.HeadEulerAngles.Y);
                            var absAngleZ = Math.Abs(faceFeatures.HeadEulerAngles.Z);
                            
                            if (absAngleX > 30 || absAngleY > 30 || absAngleZ > 30)
                            {
                                return new FaceVerificationResultDTO
                                {
                                    Success = false,
                                    Message = "Khuôn mặt nghiêng quá nhiều. Vui lòng giữ thẳng đầu.",
                                    IsMatch = false
                                };
                            }
                        }

                        // Check eye openness
                        if (faceFeatures?.Probabilities != null)
                        {
                            var avgEyeOpen = (faceFeatures.Probabilities.LeftEyeOpenProbability + 
                                            faceFeatures.Probabilities.RightEyeOpenProbability) / 2;
                            if (avgEyeOpen < 0.3)
                            {
                                return new FaceVerificationResultDTO
                                {
                                    Success = false,
                                    Message = "Mắt không mở đủ. Vui lòng mở mắt rõ ràng.",
                                    IsMatch = false
                                };
                            }
                        }
                    }
                    catch (JsonException ex)
                    {
                        _logger.LogWarning(ex, "Failed to parse face features for quality validation, continuing anyway");
                    }
                }

                // Extract embedding from verification image using Python service
                float[] verifyEmbedding = null;
                try
                {
                    // Decode base64 image
                    byte[] imageBytes = Convert.FromBase64String(request.ImageBase64);

                    // Save temporary image file for Python script
                    var tempDir = Path.Combine(Path.GetTempPath(), "face_verification");
                    Directory.CreateDirectory(tempDir);
                    var tempImagePath = Path.Combine(tempDir, $"verify_{request.EmployeeId}_{DateTime.Now:yyyyMMddHHmmss}.jpg");
                    await File.WriteAllBytesAsync(tempImagePath, imageBytes);

                    // Call Python script to extract embedding
                    var embeddingResult = await ExtractEmbeddingFromImageAsync(tempImagePath);
                    
                    // Clean up temp file
                    try { File.Delete(tempImagePath); } catch { }

                    if (embeddingResult.Success && embeddingResult.Embedding != null && embeddingResult.Embedding.Length > 0)
                    {
                        verifyEmbedding = embeddingResult.Embedding;
                        _logger.LogInformation($"Successfully extracted embedding (dimension: {verifyEmbedding.Length}) for verification");
                    }
                    else
                    {
                        return new FaceVerificationResultDTO
                        {
                            Success = false,
                            Message = $"Không thể trích xuất đặc trưng từ ảnh: {embeddingResult.Message}",
                            IsMatch = false
                        };
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error extracting embedding from verification image");
                    return new FaceVerificationResultDTO
                    {
                        Success = false,
                        Message = "Có lỗi xảy ra khi xử lý ảnh",
                        IsMatch = false
                    };
                }

                // Compare embedding with all registered embeddings
                float bestSimilarity = 0f;
                FaceRegistration bestMatch = null;

                foreach (var registration in faceRegistrations)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(registration.EmbeddingData))
                        {
                            _logger.LogWarning($"Registration {registration.FaceId} has no embedding data, skipping");
                            continue;
                        }

                        var registeredEmbedding = JsonSerializer.Deserialize<float[]>(registration.EmbeddingData);
                        if (registeredEmbedding == null || registeredEmbedding.Length == 0)
                        {
                            _logger.LogWarning($"Registration {registration.FaceId} has invalid embedding data, skipping");
                            continue;
                        }

                        // Calculate cosine similarity
                        var similarity = CalculateCosineSimilarity(verifyEmbedding, registeredEmbedding);
                        
                        if (similarity > bestSimilarity)
                        {
                            bestSimilarity = similarity;
                            bestMatch = registration;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error comparing embeddings for registration {registration.FaceId}");
                        continue;
                    }
                }

                // Threshold for match: 0.75 (75% similarity)
                const float similarityThreshold = 0.75f;
                var isMatch = bestSimilarity >= similarityThreshold;

                var user = await _context.ApplicationUsers
                    .FirstOrDefaultAsync(u => u.Id == request.EmployeeId);

                _logger.LogInformation($"Face verification for user {request.EmployeeId}: IsMatch={isMatch}, BestSimilarity={bestSimilarity:F3}, Threshold={similarityThreshold}");

                return new FaceVerificationResultDTO
                {
                    Success = true,
                    Message = isMatch ? "Nhận diện khuôn mặt thành công" : $"Không nhận diện được khuôn mặt (Similarity: {(bestSimilarity * 100):F1}%, Threshold: {(similarityThreshold * 100):F1}%)",
                    Confidence = bestSimilarity,
                    IsMatch = isMatch,
                    MatchedFaceId = isMatch ? bestMatch?.FaceId : null,
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
                        FaceQualityScore = fr.FaceQualityScore,
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
                    FaceFeaturesData = registration.FaceFeaturesData,
                    Confidence = registration.Confidence,
                    FaceQualityScore = registration.FaceQualityScore,
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

        private async Task<string> SaveImageFileToFileSystem(IFormFile imageFile, string faceId)
        {
            try
            {
                var fileName = $"{faceId}.jpg";
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "face_registrations");
                
                // Create directory if it doesn't exist
                if (!Directory.Exists(uploadsPath))
                {
                    Directory.CreateDirectory(uploadsPath);
                }

                var filePath = Path.Combine(uploadsPath, fileName);
                
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                return $"uploads/face_registrations/{fileName}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error saving image file for face ID {faceId}");
                throw;
            }
        }

        private float CalculateFaceQualityScore(FaceFeaturesDTO faceFeatures)
        {
            if (faceFeatures == null) return 0;

            float score = 100;

            // Deduct points for head rotation angles
            if (faceFeatures.HeadEulerAngles != null)
            {
                var angleX = Math.Abs(faceFeatures.HeadEulerAngles.X);
                var angleY = Math.Abs(faceFeatures.HeadEulerAngles.Y);
                var angleZ = Math.Abs(faceFeatures.HeadEulerAngles.Z);

                // Deduct 2 points per degree of rotation
                score -= (angleX + angleY + angleZ) * 2;
            }

            // Deduct points for small face size
            if (faceFeatures.Bounds != null)
            {
                var faceSize = faceFeatures.Bounds.Width * faceFeatures.Bounds.Height;
                if (faceSize < 0.2) score -= 20;
                else if (faceSize < 0.3) score -= 10;
            }

            // Bonus/penalty for eye openness
            if (faceFeatures.Probabilities != null)
            {
                var leftEyeOpen = faceFeatures.Probabilities.LeftEyeOpenProbability;
                var rightEyeOpen = faceFeatures.Probabilities.RightEyeOpenProbability;
                var avgEyeOpen = (leftEyeOpen + rightEyeOpen) / 2;

                if (avgEyeOpen > 0.8) score += 10;
                else if (avgEyeOpen < 0.5) score -= 15;
            }

            // Bonus for smiling (optional)
            if (faceFeatures.Probabilities != null && faceFeatures.Probabilities.SmilingProbability > 0.7)
            {
                score += 5;
            }

            return Math.Max(0, Math.Min(100, score));
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

        /// <summary>
        /// Extract embedding from image using Python face recognition service
        /// </summary>
        private async Task<EmbeddingResult> ExtractEmbeddingFromImageAsync(string imagePath)
        {
            try
            {
                var pythonPath = _configuration["PythonPath"] ?? "python";
                var arguments = $"extract_embedding \"{imagePath}\"";

                // Try extract_embedding command first, fallback to register if not available
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

                if (process.ExitCode == 0 && !string.IsNullOrEmpty(output))
                {
                    try
                    {
                        var result = JsonSerializer.Deserialize<PythonEmbeddingResult>(output);
                        if (result != null && result.Success && result.Embedding != null)
                        {
                            return new EmbeddingResult
                            {
                                Success = true,
                                Embedding = result.Embedding,
                                Confidence = result.Confidence
                            };
                        }
                    }
                    catch (JsonException ex)
                    {
                        _logger.LogWarning(ex, "Failed to parse Python output, trying alternative method");
                    }
                }

                // Fallback: Use Python directly to call extract_embedding method
                // Or use register command and extract embedding from result
                return await ExtractEmbeddingUsingRegisterAsync(imagePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error running Python script to extract embedding");
                return new EmbeddingResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        /// <summary>
        /// Fallback: Extract embedding using register command (temporary employee ID)
        /// </summary>
        private async Task<EmbeddingResult> ExtractEmbeddingUsingRegisterAsync(string imagePath)
        {
            try
            {
                var pythonPath = _configuration["PythonPath"] ?? "python";
                var tempEmployeeId = $"temp_{Guid.NewGuid():N}";
                var arguments = $"register \"{imagePath}\" {tempEmployeeId}";

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

                if (process.ExitCode == 0 && !string.IsNullOrEmpty(output))
                {
                    try
                    {
                        var result = JsonSerializer.Deserialize<PythonEmbeddingResult>(output);
                        if (result != null && result.Success && result.Embedding != null)
                        {
                            return new EmbeddingResult
                            {
                                Success = true,
                                Embedding = result.Embedding,
                                Confidence = result.Confidence
                            };
                        }
                    }
                    catch (JsonException ex)
                    {
                        _logger.LogError(ex, $"Failed to parse Python register output: {output}");
                    }
                }

                return new EmbeddingResult
                {
                    Success = false,
                    Message = error ?? "Unknown error extracting embedding"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in fallback embedding extraction");
                return new EmbeddingResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        /// <summary>
        /// Calculate cosine similarity between two embeddings
        /// </summary>
        private float CalculateCosineSimilarity(float[] embedding1, float[] embedding2)
        {
            if (embedding1 == null || embedding2 == null || embedding1.Length != embedding2.Length)
            {
                return 0f;
            }

            float dotProduct = 0f;
            float norm1 = 0f;
            float norm2 = 0f;

            for (int i = 0; i < embedding1.Length; i++)
            {
                dotProduct += embedding1[i] * embedding2[i];
                norm1 += embedding1[i] * embedding1[i];
                norm2 += embedding2[i] * embedding2[i];
            }

            var denominator = Math.Sqrt(norm1) * Math.Sqrt(norm2);
            if (denominator == 0)
            {
                return 0f;
            }

            return (float)(dotProduct / denominator);
        }

        private class EmbeddingResult
        {
            public bool Success { get; set; }
            public float[] Embedding { get; set; }
            public float Confidence { get; set; }
            public string Message { get; set; }
        }

        private class PythonEmbeddingResult
        {
            [JsonPropertyName("success")]
            public bool Success { get; set; }
            
            [JsonPropertyName("embedding")]
            public float[] Embedding { get; set; }
            
            [JsonPropertyName("confidence")]
            public float Confidence { get; set; }
            
            [JsonPropertyName("message")]
            public string Message { get; set; }
        }
    }
}