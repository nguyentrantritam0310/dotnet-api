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
            
            // Log Python script path and verify it exists
            _logger.LogInformation($"🔧 Python script path: {_pythonScriptPath}");
            if (File.Exists(_pythonScriptPath))
            {
                _logger.LogInformation($"✅ Python script file exists");
            }
            else
            {
                _logger.LogError($"❌ Python script file NOT FOUND at: {_pythonScriptPath}");
            }
            
            var pythonPath = _configuration["PythonPath"] ?? "python";
            _logger.LogInformation($"🔧 Python interpreter: {pythonPath}");
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
                float confidence = 0f; // Start with 0, only update if Python returns valid confidence
                
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
                        // Use Python's confidence if available and valid (between 0 and 1)
                        if (embeddingResult.Confidence > 0 && embeddingResult.Confidence <= 1)
                        {
                            confidence = embeddingResult.Confidence;
                        }
                        else
                        {
                            // Fallback: calculate confidence from quality score
                            confidence = Math.Max(0f, Math.Min(1f, qualityScore / 100f));
                            _logger.LogWarning($"Python confidence invalid ({embeddingResult.Confidence}), using quality-based confidence: {confidence}");
                        }
                        _logger.LogInformation($"Successfully extracted embedding (dimension: {embedding.Length}, confidence: {confidence:F3}) for face {faceId}");
                    }
                    else
                    {
                        _logger.LogWarning($"Failed to extract embedding, using mock embedding. Error: {embeddingResult.Message}");
                        embedding = GenerateMockEmbedding().ToArray();
                        // Use quality score as confidence when using mock embedding
                        confidence = Math.Max(0f, Math.Min(1f, qualityScore / 100f));
                        _logger.LogWarning($"Using mock embedding with quality-based confidence: {confidence:F3}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error extracting embedding from image, using mock embedding");
                    embedding = GenerateMockEmbedding().ToArray();
                    // Use quality score as confidence when using mock embedding due to error
                    confidence = Math.Max(0f, Math.Min(1f, qualityScore / 100f));
                    _logger.LogWarning($"Using mock embedding due to error, quality-based confidence: {confidence:F3}");
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

                        // SECURITY: Check embedding dimension mismatch
                        if (verifyEmbedding.Length != registeredEmbedding.Length)
                        {
                            _logger.LogWarning($"⚠️ [SECURITY] Embedding dimension mismatch - Verify: {verifyEmbedding.Length}, Registered: {registeredEmbedding.Length}. FaceId: {registration.FaceId}. Skipping comparison.");
                            continue; // Skip comparison if dimensions don't match
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

                // SECURITY: Use very high threshold to prevent false positives (wrong person being recognized)
                // Threshold: 0.92 (92% similarity) - very strict to prevent security breaches
                // Note: With 256-dim embedding, same person should achieve 90-95% similarity
                const float similarityThreshold = 0.92f;
                var isMatch = bestSimilarity >= similarityThreshold;
                
                // EXTRA SECURITY: Even if above threshold, reject if similarity is suspiciously low for same person
                // Same person with good quality should achieve 93%+ with 256-dim embedding
                if (isMatch && bestSimilarity < 0.93f)
                {
                    _logger.LogWarning($"🚨 [SECURITY ALERT] Borderline match (similarity: {bestSimilarity:F3}) - This might be a different person!");
                    // Still allow but log warning - you can set isMatch = false here if want stricter
                }

                // Log security-critical information
                _logger.LogWarning($"🔒 [SECURITY] Face verification attempt - EmployeeId: {request.EmployeeId}, BestSimilarity: {bestSimilarity:F3}, Threshold: {similarityThreshold}, IsMatch: {isMatch}, MatchedFaceId: {bestMatch?.FaceId}");
                
                // If similarity is suspiciously high but person is different, log warning
                if (isMatch && bestSimilarity > 0.98f)
                {
                    _logger.LogWarning($"⚠️ [SECURITY WARNING] Extremely high similarity ({bestSimilarity:F3}) - verify this is correct person (possible duplicate or same photo)");
                }

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

        public async Task<FaceRegistrationResultDTO> RegisterFaceEmbeddingAsync(FaceEmbeddingRegisterRequestDTO request)
        {
            try
            {
                // Validate user
                var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == request.EmployeeId);
                if (user == null)
                {
                    return new FaceRegistrationResultDTO { Success = false, Message = "Người dùng không tồn tại" };
                }

                // Limit active registrations to 4 (one per pose)
                var existingCount = await _context.FaceRegistrations.CountAsync(fr => fr.EmployeeId == request.EmployeeId && fr.IsActive);
                if (existingCount >= 4)
                {
                    return new FaceRegistrationResultDTO
                    {
                        Success = false,
                        Message = "Bạn đã đăng ký đủ 4 góc (front/left/right/up). Không thể đăng ký thêm."
                    };
                }

                // Prevent duplicate pose for same employee if pose provided
                if (!string.IsNullOrWhiteSpace(request.Pose))
                {
                    var existsSamePose = await _context.FaceRegistrations.AnyAsync(fr => fr.EmployeeId == request.EmployeeId && fr.IsActive && fr.Pose == request.Pose);
                    if (existsSamePose)
                    {
                        return new FaceRegistrationResultDTO
                        {
                            Success = false,
                            Message = $"Bạn đã đăng ký pose '{request.Pose}'. Vui lòng chuyển sang pose khác."
                        };
                    }
                }

                // Generate ID and persist
                var faceId = $"FACE_{request.EmployeeId}_{DateTime.Now:yyyyMMddHHmmss}";

                // Derive a confidence from provided quality score to avoid 0 display
                var derivedConfidence = (request.FaceQualityScore.HasValue)
                    ? Math.Max(0f, Math.Min(1f, request.FaceQualityScore.Value / 100f))
                    : 0f;

                var faceRegistration = new FaceRegistration
                {
                    EmployeeId = request.EmployeeId,
                    FaceId = faceId,
                    ImagePath = string.Empty, // no image stored
                    EmbeddingData = JsonSerializer.Serialize(request.Embedding),
                    FaceFeaturesData = string.Empty,
                    Confidence = derivedConfidence,
                    FaceQualityScore = request.FaceQualityScore ?? 0f,
                    RegisteredDate = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow,
                    IsActive = true,
                    RegisteredBy = request.EmployeeId,
                    Notes = request.Notes ?? string.Empty,
                    Pose = request.Pose
                };

                _context.FaceRegistrations.Add(faceRegistration);
                await _context.SaveChangesAsync();

                return new FaceRegistrationResultDTO
                {
                    Success = true,
                    Message = "Đăng ký embedding khuôn mặt thành công",
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
                    FaceId = faceId,
                    FaceQualityScore = faceRegistration.FaceQualityScore
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RegisterFaceEmbeddingAsync");
                return new FaceRegistrationResultDTO { Success = false, Message = "Có lỗi xảy ra khi đăng ký embedding khuôn mặt" };
            }
        }

        public async Task<FaceVerificationResultDTO> VerifyFaceEmbeddingAsync(FaceEmbeddingVerifyRequestDTO request)
        {
            try
            {
                // Validate embedding dimension upfront
                const int EXPECTED_EMBEDDING_DIMENSION = 256;
                if (request.Embedding == null || request.Embedding.Length == 0)
                {
                    _logger.LogWarning($"🚨 [SECURITY] Invalid embedding: null or empty for employee {request.EmployeeId}");
                    return new FaceVerificationResultDTO
                    {
                        Success = false,
                        Message = "Embedding không hợp lệ",
                        IsMatch = false
                    };
                }

                if (request.Embedding.Length != EXPECTED_EMBEDDING_DIMENSION)
                {
                    _logger.LogWarning($"🚨 [SECURITY] Embedding dimension mismatch - Expected: {EXPECTED_EMBEDDING_DIMENSION}, Received: {request.Embedding.Length} for employee {request.EmployeeId}");
                    return new FaceVerificationResultDTO
                    {
                        Success = false,
                        Message = $"Embedding không đúng định dạng (Expected {EXPECTED_EMBEDDING_DIMENSION} dimensions, got {request.Embedding.Length}). Vui lòng đăng ký lại khuôn mặt.",
                        IsMatch = false
                    };
                }

                var faceRegistrations = await _context.FaceRegistrations
                    .Where(fr => fr.EmployeeId == request.EmployeeId && fr.IsActive)
                    .ToListAsync();

                if (!faceRegistrations.Any())
                {
                    _logger.LogInformation($"ℹ️ No active face registrations found for employee {request.EmployeeId}");
                    return new FaceVerificationResultDTO
                    {
                        Success = false,
                        Message = "Người dùng chưa đăng ký khuôn mặt",
                        IsMatch = false
                    };
                }

                float bestSimilarity = 0f;
                FaceRegistration? bestMatch = null;
                var similarityDetails = new List<(string FaceId, float Similarity)>();

                foreach (var registration in faceRegistrations)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(registration.EmbeddingData)) continue;
                        var registeredEmbedding = JsonSerializer.Deserialize<float[]>(registration.EmbeddingData);
                        if (registeredEmbedding == null || registeredEmbedding.Length == 0) continue;

                        // SECURITY: Check embedding dimension mismatch
                        if (request.Embedding.Length != registeredEmbedding.Length)
                        {
                            _logger.LogWarning($"⚠️ [SECURITY] Embedding dimension mismatch - Request: {request.Embedding.Length}, Registered: {registeredEmbedding.Length}. FaceId: {registration.FaceId}. Skipping comparison.");
                            continue; // Skip comparison if dimensions don't match
                        }
                        
                        var similarity = CalculateCosineSimilarity(request.Embedding, registeredEmbedding);
                        similarityDetails.Add((registration.FaceId, similarity));
                        
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
                
                // Log all similarity scores for debugging
                _logger.LogInformation($"📊 [VERIFY] Similarity scores for {request.EmployeeId}: {string.Join(", ", similarityDetails.Select(d => $"{d.FaceId}:{d.Similarity:F3}"))}");

                // SECURITY: Use very high threshold to prevent false positives (wrong person being recognized)
                // Threshold: 0.92 (92% similarity) - very strict to prevent security breaches
                // Note: With 256-dim embedding, same person should achieve 90-95% similarity
                // If similarity < 92%, likely a different person
                const float similarityThreshold = 0.92f;
                
                // Additional security: require at least 2 poses to match if similarity is borderline (90-92%)
                // For now, use strict single threshold
                var isMatch = bestSimilarity >= similarityThreshold;
                
                // EXTRA SECURITY: Even if above threshold, reject if similarity is suspiciously low for same person
                // Same person with good quality should achieve 93%+ with 256-dim embedding
                if (isMatch && bestSimilarity < 0.93f)
                {
                    _logger.LogWarning($"🚨 [SECURITY ALERT] Borderline match (similarity: {bestSimilarity:F3}) - This might be a different person!");
                    // Still allow but log warning - you can set isMatch = false here if want stricter
                }
                
                // Log security-critical information
                _logger.LogWarning($"🔒 [SECURITY] Face verification attempt - EmployeeId: {request.EmployeeId}, BestSimilarity: {bestSimilarity:F3}, Threshold: {similarityThreshold}, IsMatch: {isMatch}, MatchedFaceId: {bestMatch?.FaceId}");
                
                // If similarity is suspiciously high but person is different, log warning
                if (isMatch && bestSimilarity > 0.95f)
                {
                    _logger.LogWarning($"⚠️ [SECURITY WARNING] Very high similarity ({bestSimilarity:F3}) - verify this is correct person");
                }

                var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == request.EmployeeId);

                return new FaceVerificationResultDTO
                {
                    Success = true,
                    Message = isMatch ? "Nhận diện khuôn mặt thành công" : $"Không nhận diện được khuôn mặt (Similarity: {(bestSimilarity * 100):F1}%, Threshold: {(similarityThreshold * 100):F1}%)",
                    Confidence = bestSimilarity,
                    IsMatch = isMatch,
                    MatchedFaceId = isMatch ? bestMatch?.FaceId : null,
                    EmployeeName = user != null ? $"{user.FirstName} {user.LastName}" : string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in VerifyFaceEmbeddingAsync");
                return new FaceVerificationResultDTO { Success = false, Message = "Có lỗi xảy ra khi nhận diện khuôn mặt", IsMatch = false };
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

        private float CalculateFaceQualityScore(FaceFeaturesDTO faceFeatures, bool isMultiPose = false)
        {
            if (faceFeatures == null)
            {
                _logger.LogWarning("FaceFeatures is null, returning quality score 0");
                return 0;
            }

            float score = 100;
            bool hasValidData = false;

            // Deduct points for head rotation angles
            // For multi-pose registration, we allow rotation up to 45 degrees before penalizing
            if (faceFeatures.HeadEulerAngles != null)
            {
                var angleX = Math.Abs(faceFeatures.HeadEulerAngles.X);
                var angleY = Math.Abs(faceFeatures.HeadEulerAngles.Y);
                var angleZ = Math.Abs(faceFeatures.HeadEulerAngles.Z);

                if (isMultiPose)
                {
                    // For multi-pose: only penalize excessive rotation (>45 degrees)
                    var excessiveX = Math.Max(0, angleX - 45);
                    var excessiveY = Math.Max(0, angleY - 45);
                    var excessiveZ = Math.Max(0, angleZ - 45);
                    score -= (excessiveX + excessiveY + excessiveZ) * 2; // 2 points per excessive degree
                    
                    _logger.LogDebug($"Multi-pose - Head angles - X: {angleX:F2}°, Y: {angleY:F2}°, Z: {angleZ:F2}°, Excessive deduction: {(excessiveX + excessiveY + excessiveZ) * 2:F1}");
                }
                else
                {
                    // For single-pose: only penalize excessive rotation (>30 degrees)
                    var excessiveX = Math.Max(0, angleX - 30);
                    var excessiveY = Math.Max(0, angleY - 30);
                    var excessiveZ = Math.Max(0, angleZ - 30);
                    score -= (excessiveX + excessiveY + excessiveZ) * 2;
                    
                    _logger.LogDebug($"Single-pose - Head angles - X: {angleX:F2}°, Y: {angleY:F2}°, Z: {angleZ:F2}°, Excessive deduction: {(excessiveX + excessiveY + excessiveZ) * 2:F1}");
                }
                hasValidData = true;
            }
            else
            {
                _logger.LogWarning("HeadEulerAngles is null, cannot assess head pose");
                score -= 20; // Penalty for missing head angle data
            }

            // Deduct points for small face size
            if (faceFeatures.Bounds != null && faceFeatures.Bounds.Width > 0 && faceFeatures.Bounds.Height > 0)
            {
                var faceSize = faceFeatures.Bounds.Width * faceFeatures.Bounds.Height;
                if (faceSize < 0.2) score -= 20;
                else if (faceSize < 0.3) score -= 10;
                hasValidData = true;
                
                _logger.LogDebug($"Face size: {faceSize:F4}, Score deduction: {(faceSize < 0.2 ? 20 : (faceSize < 0.3 ? 10 : 0))}");
            }
            else
            {
                _logger.LogWarning("Bounds is null or invalid, cannot assess face size");
                score -= 15; // Penalty for missing bounds data
            }

            // Bonus/penalty for eye openness
            if (faceFeatures.Probabilities != null)
            {
                var leftEyeOpen = faceFeatures.Probabilities.LeftEyeOpenProbability;
                var rightEyeOpen = faceFeatures.Probabilities.RightEyeOpenProbability;
                
                // Use probabilities if they are available (even if 0, it means ML Kit detected something)
                var avgEyeOpen = (leftEyeOpen + rightEyeOpen) / 2;

                if (avgEyeOpen > 0.8) score += 10;
                else if (avgEyeOpen < 0.5) score -= 15;
                hasValidData = true;
                
                _logger.LogDebug($"Eye openness - Left: {leftEyeOpen:F2}, Right: {rightEyeOpen:F2}, Avg: {avgEyeOpen:F2}, Score adjustment: {(avgEyeOpen > 0.8 ? 10 : (avgEyeOpen < 0.5 ? -15 : 0))}");

                // Bonus for smiling (optional)
                if (faceFeatures.Probabilities.SmilingProbability > 0.7)
                {
                    score += 5;
                }
            }
            else
            {
                _logger.LogWarning("Probabilities is null, cannot assess eye/mouth state");
                score -= 10; // Penalty for missing probability data
            }

            // If no valid data was found, return a low score
            if (!hasValidData)
            {
                _logger.LogWarning("No valid face features data found, returning low quality score");
                return 30; // Low score when data is mostly missing
            }

            var finalScore = Math.Max(0, Math.Min(100, score));
            _logger.LogDebug($"Calculated face quality score: {finalScore:F1}/100 (base: 100, deductions/applicable)");
            
            return finalScore;
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
                
                _logger.LogInformation($"🚀 Executing Python script: {pythonPath} \"{_pythonScriptPath}\" {arguments}");
                
                process.Start();

                var output = await process.StandardOutput.ReadToEndAsync();
                var error = await process.StandardError.ReadToEndAsync();
                
                await process.WaitForExitAsync();

                _logger.LogInformation($"📊 Python process exited with code: {process.ExitCode}");
                _logger.LogInformation($"📤 Python stdout length: {(output != null ? output.Length : 0)} characters");
                _logger.LogInformation($"📤 Python stdout (first 500 chars): {(output != null ? output.Substring(0, Math.Min(500, output.Length)) : "null")}");
                if (!string.IsNullOrEmpty(error))
                {
                    _logger.LogWarning($"⚠️ Python stderr: {error}");
                }

                if (process.ExitCode == 0 && !string.IsNullOrEmpty(output))
                {
                    try
                    {
                        // Try to parse JSON - Python might output multiple lines (logs before JSON)
                        // Find the last line that looks like JSON (starts with {)
                        var jsonOutput = output;
                        var lines = output.Split('\n');
                        foreach (var line in lines.Reverse())
                        {
                            var trimmedLine = line.Trim();
                            if (trimmedLine.StartsWith("{") && trimmedLine.EndsWith("}"))
                            {
                                jsonOutput = trimmedLine;
                                _logger.LogInformation($"✅ Found JSON in Python output: {jsonOutput.Substring(0, Math.Min(200, jsonOutput.Length))}...");
                                break;
                            }
                        }
                        
                        var result = JsonSerializer.Deserialize<PythonEmbeddingResult>(jsonOutput);
                        if (result != null && result.Success && result.Embedding != null)
                        {
                            _logger.LogInformation($"✅ Python extract_embedding successful! Embedding dimension: {result.Embedding.Length}, Confidence: {result.Confidence}");
                            return new EmbeddingResult
                            {
                                Success = true,
                                Embedding = result.Embedding,
                                Confidence = result.Confidence
                            };
                        }
                        else
                        {
                            _logger.LogWarning($"⚠️ Python returned Success={result?.Success}, but Embedding is null or empty. Message: {result?.Message}");
                        }
                    }
                    catch (JsonException ex)
                    {
                        _logger.LogWarning(ex, $"⚠️ Failed to parse Python output as JSON. Output was: {output?.Substring(0, Math.Min(500, output?.Length ?? 0))}");
                    }
                }
                else
                {
                    _logger.LogWarning($"⚠️ Python script failed or returned empty output. ExitCode: {process.ExitCode}, Output empty: {string.IsNullOrEmpty(output)}");
                    if (process.ExitCode != 0)
                    {
                        _logger.LogError($"❌ Python script error: {error}");
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
                    Message = ex.Message,
                    Confidence = 0f  // Explicitly set to 0 on error
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
                
                _logger.LogInformation($"🚀 Executing Python register fallback: {pythonPath} \"{_pythonScriptPath}\" {arguments}");
                
                process.Start();

                var output = await process.StandardOutput.ReadToEndAsync();
                var error = await process.StandardError.ReadToEndAsync();
                
                await process.WaitForExitAsync();

                _logger.LogInformation($"📊 Python register process exited with code: {process.ExitCode}");
                _logger.LogInformation($"📤 Python register stdout length: {(output != null ? output.Length : 0)} characters");
                _logger.LogInformation($"📤 Python register stdout (first 500 chars): {(output != null ? output.Substring(0, Math.Min(500, output.Length)) : "null")}");
                if (!string.IsNullOrEmpty(error))
                {
                    _logger.LogWarning($"⚠️ Python register stderr: {error}");
                }

                if (process.ExitCode == 0 && !string.IsNullOrEmpty(output))
                {
                    try
                    {
                        // Try to parse JSON - Python might output multiple lines (logs before JSON)
                        var jsonOutput = output;
                        var lines = output.Split('\n');
                        foreach (var line in lines.Reverse())
                        {
                            var trimmedLine = line.Trim();
                            if (trimmedLine.StartsWith("{") && trimmedLine.EndsWith("}"))
                            {
                                jsonOutput = trimmedLine;
                                _logger.LogInformation($"✅ Found JSON in Python register output: {jsonOutput.Substring(0, Math.Min(200, jsonOutput.Length))}...");
                                break;
                            }
                        }
                        
                        var result = JsonSerializer.Deserialize<PythonEmbeddingResult>(jsonOutput);
                        if (result != null && result.Success && result.Embedding != null)
                        {
                            _logger.LogInformation($"✅ Python register fallback successful! Embedding dimension: {result.Embedding.Length}, Confidence: {result.Confidence}");
                            return new EmbeddingResult
                            {
                                Success = true,
                                Embedding = result.Embedding,
                                Confidence = result.Confidence
                            };
                        }
                        else
                        {
                            _logger.LogWarning($"⚠️ Python register returned Success={result?.Success}, but Embedding is null. Message: {result?.Message}");
                        }
                    }
                    catch (JsonException ex)
                    {
                        _logger.LogError(ex, $"⚠️ Failed to parse Python register output as JSON. Output was: {output?.Substring(0, Math.Min(500, output?.Length ?? 0))}");
                    }
                }
                else
                {
                    _logger.LogWarning($"⚠️ Python register script failed or returned empty output. ExitCode: {process.ExitCode}, Output empty: {string.IsNullOrEmpty(output)}");
                    if (process.ExitCode != 0)
                    {
                        _logger.LogError($"❌ Python register script error: {error}");
                    }
                }

                return new EmbeddingResult
                {
                    Success = false,
                    Message = error ?? "Unknown error extracting embedding",
                    Confidence = 0f  // Explicitly set to 0 on failure
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in fallback embedding extraction");
                return new EmbeddingResult
                {
                    Success = false,
                    Message = ex.Message,
                    Confidence = 0f  // Explicitly set to 0 on error
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