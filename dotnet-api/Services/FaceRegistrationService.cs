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

        public FaceRegistrationService(
            ApplicationDbContext context, 
            ILogger<FaceRegistrationService> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<FaceRegistrationResultDTO> RegisterFaceEmbeddingAsync(FaceEmbeddingRegisterRequestDTO request)
        {
            try
            {
                var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == request.EmployeeId);
                if (user == null)
                {
                    return new FaceRegistrationResultDTO { Success = false, Message = "Người dùng không tồn tại" };
                }

                var existingCount = await _context.FaceRegistrations.CountAsync(fr => fr.EmployeeId == request.EmployeeId && fr.IsActive);
                if (existingCount >= 4)
                {
                    return new FaceRegistrationResultDTO
                    {
                        Success = false,
                        Message = "Bạn đã đăng ký đủ 4 góc (front/left/right/up). Không thể đăng ký thêm."
                    };
                }

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

                var faceId = $"FACE_{request.EmployeeId}_{DateTime.Now:yyyyMMddHHmmss}";
                var derivedConfidence = request.FaceQualityScore.HasValue
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
                    FaceRegistration = MapToFaceRegistrationDTO(faceRegistration, user),
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
                const int EXPECTED_EMBEDDING_DIMENSION = 512;
                
                var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == request.EmployeeId);
                
                if (request.Embedding == null || request.Embedding.Length == 0)
                {
                    return new FaceVerificationResultDTO
                    {
                        Success = false,
                        Message = "Embedding không hợp lệ",
                        IsMatch = false
                    };
                }

                if (request.Embedding.Length != EXPECTED_EMBEDDING_DIMENSION)
                {
                    _logger.LogWarning("Embedding dimension mismatch - Expected: {Expected}, Received: {Received}, EmployeeId: {EmployeeId}",
                        EXPECTED_EMBEDDING_DIMENSION, request.Embedding.Length, request.EmployeeId);
                    return new FaceVerificationResultDTO
                    {
                        Success = false,
                        Message = $"Embedding không đúng định dạng (Expected {EXPECTED_EMBEDDING_DIMENSION} dimensions, got {request.Embedding.Length})",
                        IsMatch = false
                    };
                }

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

                float bestSimilarity = 0f;
                FaceRegistration? bestMatch = null;
                var similarityDetails = new List<(string FaceId, string? Pose, float Similarity)>();

                foreach (var registration in faceRegistrations)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(registration.EmbeddingData)) continue;
                        var registeredEmbedding = JsonSerializer.Deserialize<float[]>(registration.EmbeddingData);
                        if (registeredEmbedding == null || registeredEmbedding.Length == 0) continue;

                        if (request.Embedding.Length != registeredEmbedding.Length)
                        {
                            _logger.LogWarning("Embedding dimension mismatch - Request: {RequestDim}, Registered: {RegisteredDim}, FaceId: {FaceId}",
                                request.Embedding.Length, registeredEmbedding.Length, registration.FaceId);
                            continue;
                        }
                        
                        var similarity = CalculateCosineSimilarity(request.Embedding, registeredEmbedding);
                        similarityDetails.Add((registration.FaceId, registration.Pose, similarity));
                        
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
                
                const float similarityThreshold = 0.75f;
                const float minSimilarityForRejection = 0.70f;
                
                if (bestSimilarity < minSimilarityForRejection)
                {
                    return new FaceVerificationResultDTO
                    {
                        Success = true,
                        Message = $"Sai người! Khuôn mặt này không khớp với khuôn mặt đã đăng ký. (Độ tương đồng: {(bestSimilarity * 100):F1}%)",
                        Confidence = bestSimilarity,
                        IsMatch = false,
                        EmployeeName = user != null ? $"{user.FirstName} {user.LastName}" : string.Empty
                    };
                }
                
                var hasMultiplePoses = faceRegistrations.Count >= 3;
                var matchingPosesCount = similarityDetails.Count(d => d.Similarity >= similarityThreshold);
                
                bool isMatch;
                if (hasMultiplePoses && matchingPosesCount >= 2)
                {
                    isMatch = true;
                }
                else if (hasMultiplePoses && matchingPosesCount == 0)
                {
                    isMatch = false;
                }
                else
                {
                    isMatch = bestSimilarity >= similarityThreshold;
                }
                
                _logger.LogInformation("Face verification - EmployeeId: {EmployeeId}, Similarity: {Similarity:F3}, Threshold: {Threshold}, IsMatch: {IsMatch}, MatchedFaceId: {FaceId}",
                    request.EmployeeId, bestSimilarity, similarityThreshold, isMatch, bestMatch?.FaceId);

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

                return MapToFaceRegistrationDTO(registration, registration.Employee);
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


        private float CalculateFaceQualityScore(FaceFeaturesDTO faceFeatures, bool isMultiPose = false)
        {
            if (faceFeatures == null) return 0;

            float score = 100;
            bool hasValidData = false;

            if (faceFeatures.HeadEulerAngles != null)
            {
                var angleX = Math.Abs(faceFeatures.HeadEulerAngles.X);
                var angleY = Math.Abs(faceFeatures.HeadEulerAngles.Y);
                var angleZ = Math.Abs(faceFeatures.HeadEulerAngles.Z);

                var maxAngle = isMultiPose ? 45f : 30f;
                var excessiveX = Math.Max(0, angleX - maxAngle);
                var excessiveY = Math.Max(0, angleY - maxAngle);
                var excessiveZ = Math.Max(0, angleZ - maxAngle);
                    score -= (excessiveX + excessiveY + excessiveZ) * 2;
                hasValidData = true;
            }
            else
            {
                score -= 20;
            }

            if (faceFeatures.Bounds != null && faceFeatures.Bounds.Width > 0 && faceFeatures.Bounds.Height > 0)
            {
                var faceSize = faceFeatures.Bounds.Width * faceFeatures.Bounds.Height;
                if (faceSize < 0.2) score -= 20;
                else if (faceSize < 0.3) score -= 10;
                hasValidData = true;
            }
            else
            {
                score -= 15;
            }

            if (faceFeatures.Probabilities != null)
            {
                var avgEyeOpen = (faceFeatures.Probabilities.LeftEyeOpenProbability + 
                                 faceFeatures.Probabilities.RightEyeOpenProbability) / 2;
                if (avgEyeOpen > 0.8) score += 10;
                else if (avgEyeOpen < 0.5) score -= 15;
                
                if (faceFeatures.Probabilities.SmilingProbability > 0.7)
                {
                    score += 5;
                }
                hasValidData = true;
            }
            else
            {
                score -= 10;
            }

            if (!hasValidData) return 30;

            return Math.Max(0, Math.Min(100, score));
        }

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

        private FaceRegistrationDTO MapToFaceRegistrationDTO(FaceRegistration registration, ApplicationUser user)
        {
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
                EmployeeName = $"{user.FirstName} {user.LastName}",
                EmployeeEmail = user.Email
            };
        }

    }
}