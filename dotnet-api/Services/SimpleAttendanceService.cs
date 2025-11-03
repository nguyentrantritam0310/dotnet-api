using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.Data.Enums;
using dotnet_api.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace dotnet_api.Services
{
    public class SimpleAttendanceService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SimpleAttendanceService> _logger;
        
        // In-memory cache for verification tokens to prevent replay attacks
        // Key: VerificationToken, Value: Expiry timestamp
        private static readonly ConcurrentDictionary<string, DateTime> _verificationTokenCache = new();

        public SimpleAttendanceService(ApplicationDbContext context, ILogger<SimpleAttendanceService> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        // Helper method to parse datetime string as Vietnam local time (GMT+7)
        // Client sends local datetime string without timezone info (e.g., "2025-11-03T22:28:00")
        // This represents Vietnam local time. We need to store it in database.
        // If database stores UTC, we convert: 22:28 Vietnam → 15:28 UTC
        // If database stores local time, we store: 22:28 directly
        private DateTime ParseAsVietnamTime(DateTime dateTime)
        {
            // If datetime is unspecified (from string without timezone), treat as Vietnam local time
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                // Database appears to store times in UTC (based on the 7-hour difference observed)
                // Client sends 22:28 Vietnam time → Convert to 15:28 UTC for storage
                // When querying, UTC will be converted back to local time by client/display layer
                return DateTime.SpecifyKind(dateTime.AddHours(-7), DateTimeKind.Utc);
            }
            
            // If already UTC, keep as UTC
            if (dateTime.Kind == DateTimeKind.Utc)
            {
                return dateTime;
            }
            
            // If already local, convert to UTC (Vietnam is UTC+7)
            if (dateTime.Kind == DateTimeKind.Local)
            {
                return dateTime.ToUniversalTime();
            }
            
            return dateTime;
        }
        
        // Clean up expired tokens periodically
        private void CleanupExpiredTokens()
        {
            var now = DateTime.UtcNow;
            var expiredKeys = _verificationTokenCache
                .Where(kvp => kvp.Value < now)
                .Select(kvp => kvp.Key)
                .ToList();
            
            foreach (var key in expiredKeys)
            {
                _verificationTokenCache.TryRemove(key, out _);
            }
        }

        public async Task<AttendanceCheckInResult> CheckInAsync(AttendanceCheckInRequest request)
        {
            try
            {
                _logger.LogInformation($"Processing check-in for employee: {request.EmployeeId}");

                // Kiểm tra đã chấm công vào chưa
                var existingAttendance = await GetTodayAttendanceAsync(request.EmployeeId);
                if (existingAttendance != null && existingAttendance.CheckInDateTime.HasValue)
                {
                    return new AttendanceCheckInResult
                    {
                        Success = false,
                        Message = "Bạn đã chấm công vào hôm nay",
                        EmployeeId = request.EmployeeId,
                        CheckInDateTime = existingAttendance.CheckInDateTime.Value,
                        Status = existingAttendance.Status ?? AttendanceStatusEnum.Present
                    };
                }

                // Lấy thông tin nhân viên
                var employee = await _context.Users.FindAsync(request.EmployeeId);
                if (employee == null)
                {
                    return new AttendanceCheckInResult
                    {
                        Success = false,
                        Message = "Không tìm thấy thông tin nhân viên",
                        EmployeeId = request.EmployeeId
                    };
                }

                // Tạo attendance record mới
                var attendance = new Attendance
                {
                    EmployeeId = request.EmployeeId,
                    CheckInDateTime = request.CheckInDateTime,
                    CheckIn = request.CheckInDateTime.TimeOfDay,
                    ImageCheckIn = await SaveAttendanceImageAsync(request.ImageBase64, "checkin", request.EmployeeId),
                    CheckInLocation = request.Location ?? $"{request.Latitude},{request.Longitude}",
                    AttendanceMachineId = request.AttendanceMachineId,
                    Status = AttendanceStatusEnum.Present,
                    Notes = request.Notes,
                    CreatedDate = DateTime.Now,
                    LastUpdated = DateTime.Now
                };

                _context.Attendances.Add(attendance);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Check-in successful for employee: {request.EmployeeId}, Attendance ID: {attendance.ID}");

                return new AttendanceCheckInResult
                {
                    Success = true,
                    Message = "Chấm công thành công",
                    AttendanceId = attendance.ID ?? 0,
                    EmployeeId = request.EmployeeId,
                    EmployeeName = employee.UserName ?? employee.Email ?? "Unknown",
                    CheckInDateTime = attendance.CheckInDateTime.Value,
                    Status = attendance.Status.Value,
                    ImagePath = attendance.ImageCheckIn,
                    Location = attendance.CheckInLocation
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing check-in for employee: {request.EmployeeId}");
                return new AttendanceCheckInResult
                {
                    Success = false,
                    Message = $"Lỗi hệ thống: {ex.Message}",
                    EmployeeId = request.EmployeeId
                };
            }
        }

        public async Task<AttendanceCheckInResult> CheckInNoImageAsync(AttendanceCheckInNoImageRequest request, string authenticatedUserId)
        {
            try
            {
                _logger.LogInformation($"🔒 [SECURITY] Processing no-image check-in for employee: {request.EmployeeId}, Authenticated user: {authenticatedUserId}");

                // SECURITY VALIDATION 1: Verify employee exists
                var employee = await _context.Users.FindAsync(request.EmployeeId);
                if (employee == null)
                {
                    _logger.LogWarning($"🚨 [SECURITY] Employee not found: {request.EmployeeId}");
                    return new AttendanceCheckInResult
                    {
                        Success = false,
                        Message = "Không tìm thấy thông tin nhân viên",
                        EmployeeId = request.EmployeeId
                    };
                }

                // SECURITY VALIDATION 2: Validate MatchedFaceId is provided
                if (string.IsNullOrWhiteSpace(request.MatchedFaceId))
                {
                    _logger.LogWarning($"🚨 [SECURITY] Missing MatchedFaceId for employee: {request.EmployeeId}");
                    return new AttendanceCheckInResult
                    {
                        Success = false,
                        Message = "Không có thông tin xác thực khuôn mặt. Vui lòng quét lại khuôn mặt.",
                        EmployeeId = request.EmployeeId
                    };
                }

                // SECURITY VALIDATION 3: Validate MatchConfidence meets threshold
                const float REQUIRED_CONFIDENCE_THRESHOLD = 0.92f;
                if (!request.MatchConfidence.HasValue || request.MatchConfidence.Value < REQUIRED_CONFIDENCE_THRESHOLD)
                {
                    _logger.LogWarning($"🚨 [SECURITY] Insufficient confidence for employee: {request.EmployeeId}, Confidence: {request.MatchConfidence}, Required: {REQUIRED_CONFIDENCE_THRESHOLD}");
                    return new AttendanceCheckInResult
                    {
                        Success = false,
                        Message = $"Độ tin cậy nhận diện không đạt yêu cầu ({(request.MatchConfidence * 100):F1}% < {REQUIRED_CONFIDENCE_THRESHOLD * 100:F0}%). Vui lòng quét lại khuôn mặt.",
                        EmployeeId = request.EmployeeId
                    };
                }

                // SECURITY VALIDATION 4: Verify MatchedFaceId exists and belongs to this employee
                var faceRegistration = await _context.FaceRegistrations
                    .FirstOrDefaultAsync(fr => 
                        fr.FaceId == request.MatchedFaceId && 
                        fr.EmployeeId == request.EmployeeId && 
                        fr.IsActive);
                
                if (faceRegistration == null)
                {
                    _logger.LogWarning($"🚨 [SECURITY] Invalid or inactive FaceId: {request.MatchedFaceId} for employee: {request.EmployeeId}");
                    return new AttendanceCheckInResult
                    {
                        Success = false,
                        Message = "Khuôn mặt đăng ký không hợp lệ hoặc đã bị vô hiệu hóa. Vui lòng đăng ký lại khuôn mặt.",
                        EmployeeId = request.EmployeeId
                    };
                }

                // SECURITY VALIDATION 5: Validate VerificationTimestamp (must be within 30 seconds)
                if (!request.VerificationTimestamp.HasValue)
                {
                    _logger.LogWarning($"🚨 [SECURITY] Missing VerificationTimestamp for employee: {request.EmployeeId}");
                    return new AttendanceCheckInResult
                    {
                        Success = false,
                        Message = "Thông tin xác thực không hợp lệ. Vui lòng quét lại khuôn mặt.",
                        EmployeeId = request.EmployeeId
                    };
                }

                // Parse timestamp and handle timezone issues
                var verificationTime = request.VerificationTimestamp.Value;
                // Ensure timestamp is in UTC
                if (verificationTime.Kind == DateTimeKind.Unspecified)
                {
                    verificationTime = DateTime.SpecifyKind(verificationTime, DateTimeKind.Utc);
                }
                else if (verificationTime.Kind == DateTimeKind.Local)
                {
                    verificationTime = verificationTime.ToUniversalTime();
                }

                var verificationAge = DateTime.UtcNow - verificationTime;
                const int MAX_VERIFICATION_AGE_SECONDS = 60; // Increased to 60 seconds to allow for network/processing delays
                if (verificationAge.TotalSeconds > MAX_VERIFICATION_AGE_SECONDS)
                {
                    _logger.LogWarning($"🚨 [SECURITY] Verification timestamp expired for employee: {request.EmployeeId}, Age: {verificationAge.TotalSeconds:F1}s, Max: {MAX_VERIFICATION_AGE_SECONDS}s");
                    return new AttendanceCheckInResult
                    {
                        Success = false,
                        Message = "Phiên xác thực đã hết hạn. Vui lòng quét lại khuôn mặt.",
                        EmployeeId = request.EmployeeId
                    };
                }
                
                // Allow small negative time (clock skew between devices, max 5 seconds)
                if (verificationAge.TotalSeconds < -5)
                {
                    _logger.LogWarning($"🚨 [SECURITY] Verification timestamp is too far in the future for employee: {request.EmployeeId}, Age: {verificationAge.TotalSeconds:F1}s");
                    return new AttendanceCheckInResult
                    {
                        Success = false,
                        Message = "Thời gian xác thực không hợp lệ. Vui lòng kiểm tra đồng hồ thiết bị.",
                        EmployeeId = request.EmployeeId
                    };
                }
                
                _logger.LogDebug($"✅ [SECURITY] Verification timestamp valid - Age: {verificationAge.TotalSeconds:F1}s");

                // SECURITY VALIDATION 6: Validate VerificationToken and prevent replay attacks
                if (string.IsNullOrWhiteSpace(request.VerificationToken))
                {
                    _logger.LogWarning($"🚨 [SECURITY] Missing VerificationToken for employee: {request.EmployeeId}");
                    return new AttendanceCheckInResult
                    {
                        Success = false,
                        Message = "Thông tin xác thực không hợp lệ. Vui lòng quét lại khuôn mặt.",
                        EmployeeId = request.EmployeeId
                    };
                }

                // Check if token already used (replay attack detection)
                if (_verificationTokenCache.ContainsKey(request.VerificationToken))
                {
                    _logger.LogWarning($"🚨 [SECURITY ALERT] Replay attack detected! VerificationToken reused: {request.VerificationToken.Substring(0, Math.Min(8, request.VerificationToken.Length))}... for employee: {request.EmployeeId}");
                    return new AttendanceCheckInResult
                    {
                        Success = false,
                        Message = "Phiên xác thực đã được sử dụng. Vui lòng quét lại khuôn mặt.",
                        EmployeeId = request.EmployeeId
                    };
                }

                // Add token to cache (expire in 60 seconds)
                _verificationTokenCache.TryAdd(request.VerificationToken, DateTime.UtcNow.AddSeconds(60));
                CleanupExpiredTokens();

                // Check if already checked in today
                var existingAttendance = await GetTodayAttendanceAsync(request.EmployeeId);
                if (existingAttendance != null && existingAttendance.CheckInDateTime.HasValue)
                {
                    _logger.LogInformation($"ℹ️ Employee {request.EmployeeId} already checked in today at {existingAttendance.CheckInDateTime}");
                    return new AttendanceCheckInResult
                    {
                        Success = false,
                        Message = "Bạn đã chấm công vào hôm nay",
                        EmployeeId = request.EmployeeId,
                        CheckInDateTime = existingAttendance.CheckInDateTime.Value,
                        Status = existingAttendance.Status ?? AttendanceStatusEnum.Present
                    };
                }

                // All validations passed - create attendance record
                _logger.LogInformation($"✅ [SECURITY] All validations passed for employee: {request.EmployeeId}, FaceId: {faceRegistration.FaceId}, Confidence: {request.MatchConfidence:F3}");

                // Parse CheckInDateTime as Vietnam local time (GMT+7)
                var checkInDateTime = ParseAsVietnamTime(request.CheckInDateTime);
                
                var attendance = new Attendance
                {
                    EmployeeId = request.EmployeeId,
                    CheckInDateTime = checkInDateTime,
                    CheckIn = checkInDateTime.TimeOfDay,
                    ImageCheckIn = string.Empty,
                    CheckInLocation = request.Location ?? $"{request.Latitude},{request.Longitude}",
                    AttendanceMachineId = request.AttendanceMachineId,
                    Status = AttendanceStatusEnum.Present,
                    Notes = request.Notes,
                    CreatedDate = DateTime.Now,
                    LastUpdated = DateTime.Now
                };

                _context.Attendances.Add(attendance);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"✅ Check-in successful for employee: {request.EmployeeId}, Attendance ID: {attendance.ID}, FaceId: {faceRegistration.FaceId}, Confidence: {request.MatchConfidence:F3}");

                return new AttendanceCheckInResult
                {
                    Success = true,
                    Message = "Chấm công thành công",
                    AttendanceId = attendance.ID ?? 0,
                    EmployeeId = request.EmployeeId,
                    EmployeeName = employee.UserName ?? employee.Email ?? "Unknown",
                    CheckInDateTime = attendance.CheckInDateTime.Value,
                    Status = attendance.Status.Value,
                    ImagePath = attendance.ImageCheckIn,
                    Location = attendance.CheckInLocation
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing no-image check-in for employee: {request.EmployeeId}");
                return new AttendanceCheckInResult
                {
                    Success = false,
                    Message = $"Lỗi hệ thống: {ex.Message}",
                    EmployeeId = request.EmployeeId
                };
            }
        }

        public async Task<AttendanceCheckInResult> CheckOutAsync(AttendanceCheckOutRequest request)
        {
            try
            {
                _logger.LogInformation($"Processing check-out for employee: {request.EmployeeId}");

                var attendance = await GetTodayAttendanceAsync(request.EmployeeId);
                if (attendance == null || !attendance.CheckInDateTime.HasValue)
                {
                    return new AttendanceCheckInResult
                    {
                        Success = false,
                        Message = "Không tìm thấy bản ghi chấm công vào hôm nay",
                        EmployeeId = request.EmployeeId
                    };
                }

                if (attendance.CheckOutDateTime.HasValue)
                {
                    return new AttendanceCheckInResult
                    {
                        Success = false,
                        Message = "Bạn đã chấm công ra hôm nay",
                        EmployeeId = request.EmployeeId,
                        CheckInDateTime = attendance.CheckInDateTime.Value
                    };
                }

                // Parse CheckOutDateTime as Vietnam local time (GMT+7)
                var checkOutDateTime = ParseAsVietnamTime(request.CheckOutDateTime);

                // Cập nhật thông tin check-out
                attendance.CheckOutDateTime = checkOutDateTime;
                attendance.CheckOut = checkOutDateTime.TimeOfDay;
                attendance.CheckOutLocation = request.Location ?? $"{request.Latitude},{request.Longitude}";
                attendance.LastUpdated = DateTime.Now;

                if (!string.IsNullOrEmpty(request.ImageBase64))
                {
                    attendance.ImageCheckOut = await SaveAttendanceImageAsync(request.ImageBase64, "checkout", request.EmployeeId);
                }

                if (!string.IsNullOrEmpty(request.Notes))
                {
                    attendance.Notes = string.IsNullOrEmpty(attendance.Notes) 
                        ? request.Notes 
                        : $"{attendance.Notes}\n{request.Notes}";
                }

                _context.Attendances.Update(attendance);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Check-out successful for employee: {request.EmployeeId}");

                return new AttendanceCheckInResult
                {
                    Success = true,
                    Message = "Chấm công ra thành công",
                    AttendanceId = attendance.ID ?? 0,
                    EmployeeId = request.EmployeeId,
                    EmployeeName = attendance.Employee?.UserName ?? attendance.Employee?.Email ?? "Unknown",
                    CheckInDateTime = attendance.CheckInDateTime.Value,
                    Status = attendance.Status ?? AttendanceStatusEnum.Present
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing check-out for employee: {request.EmployeeId}");
                return new AttendanceCheckInResult
                {
                    Success = false,
                    Message = $"Lỗi hệ thống: {ex.Message}",
                    EmployeeId = request.EmployeeId
                };
            }
        }

        public async Task<AttendanceCheckInResult> CheckOutNoImageAsync(AttendanceCheckOutNoImageRequest request)
        {
            try
            {
                _logger.LogInformation($"Processing no-image check-out for employee: {request.EmployeeId}");

                var attendance = await GetTodayAttendanceAsync(request.EmployeeId);
                if (attendance == null || !attendance.CheckInDateTime.HasValue)
                {
                    return new AttendanceCheckInResult
                    {
                        Success = false,
                        Message = "Không tìm thấy bản ghi chấm công vào hôm nay",
                        EmployeeId = request.EmployeeId
                    };
                }

                if (attendance.CheckOutDateTime.HasValue)
                {
                    return new AttendanceCheckInResult
                    {
                        Success = false,
                        Message = "Bạn đã chấm công ra hôm nay",
                        EmployeeId = request.EmployeeId,
                        CheckInDateTime = attendance.CheckInDateTime.Value
                    };
                }

                attendance.CheckOutDateTime = request.CheckOutDateTime;
                attendance.CheckOut = request.CheckOutDateTime.TimeOfDay;
                attendance.CheckOutLocation = request.Location ?? $"{request.Latitude},{request.Longitude}";
                attendance.LastUpdated = DateTime.Now;

                if (!string.IsNullOrEmpty(request.Notes))
                {
                    attendance.Notes = string.IsNullOrEmpty(attendance.Notes)
                        ? request.Notes
                        : $"{attendance.Notes}\n{request.Notes}";
                }

                _context.Attendances.Update(attendance);
                await _context.SaveChangesAsync();

                return new AttendanceCheckInResult
                {
                    Success = true,
                    Message = "Chấm công ra thành công",
                    AttendanceId = attendance.ID ?? 0,
                    EmployeeId = request.EmployeeId,
                    EmployeeName = attendance.Employee?.UserName ?? attendance.Employee?.Email ?? "Unknown",
                    CheckInDateTime = attendance.CheckInDateTime.Value,
                    Status = attendance.Status ?? AttendanceStatusEnum.Present
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing no-image check-out for employee: {request.EmployeeId}");
                return new AttendanceCheckInResult
                {
                    Success = false,
                    Message = $"Lỗi hệ thống: {ex.Message}",
                    EmployeeId = request.EmployeeId
                };
            }
        }

        public async Task<Attendance?> GetTodayAttendanceAsync(string employeeId)
        {
            try
            {
                var today = DateTime.Today;
                return await _context.Attendances
                    .Include(a => a.Employee)
                    .Include(a => a.AttendanceMachine)
                    .FirstOrDefaultAsync(a => 
                        a.EmployeeId == employeeId && 
                        a.CheckInDateTime.HasValue && 
                        a.CheckInDateTime.Value.Date == today);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting today's attendance for employee: {employeeId}");
                throw;
            }
        }

        public async Task<List<Attendance>> GetEmployeeAttendanceAsync(string employeeId, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                var query = _context.Attendances
                    .Include(a => a.Employee)
                    .Include(a => a.AttendanceMachine)
                    .Where(a => a.EmployeeId == employeeId);

                if (startDate.HasValue)
                    query = query.Where(a => a.CheckInDateTime >= startDate.Value);

                if (endDate.HasValue)
                    query = query.Where(a => a.CheckInDateTime <= endDate.Value);

                return await query
                    .OrderByDescending(a => a.CheckInDateTime)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting attendance for employee: {employeeId}");
                throw;
            }
        }

        private async Task<string> SaveAttendanceImageAsync(string imageBase64, string type, string employeeId)
        {
            try
            {
                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "attendance");
                Directory.CreateDirectory(uploadsDir);

                var fileName = $"{employeeId}_{type}_{DateTime.Now:yyyyMMddHHmmss}.jpg";
                var filePath = Path.Combine(uploadsDir, fileName);

                var imageBytes = Convert.FromBase64String(imageBase64);
                await File.WriteAllBytesAsync(filePath, imageBytes);

                return $"/uploads/attendance/{fileName}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error saving attendance image for employee: {employeeId}");
                return string.Empty;
            }
        }
    }
}



