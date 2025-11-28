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
        
        private static readonly ConcurrentDictionary<string, DateTime> _verificationTokenCache = new();

        public SimpleAttendanceService(ApplicationDbContext context, ILogger<SimpleAttendanceService> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        private DateTime ParseAsVietnamTime(DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                return dateTime;
            }
            
            if (dateTime.Kind == DateTimeKind.Local)
            {
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
            }
            
            if (dateTime.Kind == DateTimeKind.Utc)
            {
                return DateTime.SpecifyKind(dateTime.AddHours(7), DateTimeKind.Unspecified);
            }
            
            return dateTime;
        }
        
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

        public async Task<AttendanceCheckInResult> CheckInNoImageAsync(AttendanceCheckInNoImageRequest request, string authenticatedUserId)
        {
            try
            {
                var validationResult = ValidateFaceVerificationRequest(request.EmployeeId, request.MatchedFaceId, 
                    request.MatchConfidence, request.VerificationTimestamp, request.VerificationToken);
                
                if (validationResult != null)
                {
                    return validationResult;
                }

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

                var faceRegistration = await _context.FaceRegistrations
                    .FirstOrDefaultAsync(fr => 
                        fr.FaceId == request.MatchedFaceId && 
                        fr.EmployeeId == request.EmployeeId && 
                        fr.IsActive);
                
                if (faceRegistration == null)
                {
                    _logger.LogWarning("Invalid or inactive FaceId - FaceId: {FaceId}, EmployeeId: {EmployeeId}",
                        request.MatchedFaceId, request.EmployeeId);
                    return new AttendanceCheckInResult
                    {
                        Success = false,
                        Message = "Khuôn mặt đăng ký không hợp lệ hoặc đã bị vô hiệu hóa. Vui lòng đăng ký lại khuôn mặt.",
                        EmployeeId = request.EmployeeId
                    };
                }

                _verificationTokenCache.TryAdd(request.VerificationToken, DateTime.UtcNow.AddSeconds(60));
                CleanupExpiredTokens();

                if (request.WorkShiftID.HasValue && request.WorkShiftID.Value > 0)
                {
                    var today = DateTime.Today;
                    var existingShiftAttendance = await _context.Attendances
                        .Include(a => a.ShiftAssignment)
                        .FirstOrDefaultAsync(a => 
                            a.EmployeeId == request.EmployeeId && 
                            a.CheckInDateTime.HasValue && 
                            a.CheckInDateTime.Value.Date == today &&
                            a.ShiftAssignment != null &&
                            a.ShiftAssignment.WorkShiftID == request.WorkShiftID.Value);
                    
                    if (existingShiftAttendance != null)
                    {
                        return new AttendanceCheckInResult
                        {
                            Success = false,
                            Message = $"Bạn đã chấm công ca này hôm nay. Vui lòng chọn ca khác.",
                            EmployeeId = request.EmployeeId,
                            CheckInDateTime = existingShiftAttendance.CheckInDateTime.Value,
                            Status = existingShiftAttendance.Status ?? AttendanceStatusEnum.Present
                        };
                    }
                }
                
                var activeAttendance = await GetTodayAttendanceAsync(request.EmployeeId);
                if (activeAttendance != null && activeAttendance.CheckInDateTime.HasValue && !activeAttendance.CheckOutDateTime.HasValue)
                {
                    return new AttendanceCheckInResult
                    {
                        Success = false,
                        Message = "Bạn đã chấm công vào nhưng chưa chấm công ra. Vui lòng chấm công ra trước khi chấm công vào ca mới.",
                        EmployeeId = request.EmployeeId,
                        CheckInDateTime = activeAttendance.CheckInDateTime.Value,
                        Status = activeAttendance.Status ?? AttendanceStatusEnum.Present
                    };
                }

                var checkInDateTime = ParseAsVietnamTime(request.CheckInDateTime);
                int? shiftAssignmentId = await GetOrCreateShiftAssignmentAsync(request.EmployeeId, request.WorkShiftID, checkInDateTime.Date);
                
                var attendance = new Attendance
                {
                    EmployeeId = request.EmployeeId,
                    ShiftAssignmentID = shiftAssignmentId,
                    CheckInDateTime = checkInDateTime,
                    CheckIn = checkInDateTime.TimeOfDay,
                    CheckInLocation = request.Location ?? $"{request.Latitude},{request.Longitude}",
                    AttendanceMachineId = request.AttendanceMachineId,
                    Status = AttendanceStatusEnum.Present,
                    Notes = request.Notes,
                    CreatedDate = DateTime.Now,
                    LastUpdated = DateTime.Now
                };

                _context.Attendances.Add(attendance);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Check-in successful - EmployeeId: {EmployeeId}, AttendanceId: {AttendanceId}, FaceId: {FaceId}, Confidence: {Confidence}",
                    request.EmployeeId, attendance.ID, faceRegistration.FaceId, request.MatchConfidence);

                return new AttendanceCheckInResult
                {
                    Success = true,
                    Message = "Chấm công thành công",
                    AttendanceId = attendance.ID ?? 0,
                    EmployeeId = request.EmployeeId,
                    EmployeeName = employee.UserName ?? employee.Email ?? "Unknown",
                    CheckInDateTime = attendance.CheckInDateTime.Value,
                    Status = attendance.Status.Value,
                    Location = attendance.CheckInLocation
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing no-image check-in - EmployeeId: {EmployeeId}", request.EmployeeId);
                return new AttendanceCheckInResult
                {
                    Success = false,
                    Message = $"Lỗi hệ thống: {ex.Message}",
                    EmployeeId = request.EmployeeId
                };
            }
        }

        public async Task<AttendanceCheckInResult> CheckOutNoImageAsync(AttendanceCheckOutNoImageRequest request, string authenticatedUserId)
        {
            try
            {
                var validationResult = ValidateFaceVerificationRequest(request.EmployeeId, request.MatchedFaceId, 
                    request.MatchConfidence, request.VerificationTimestamp, request.VerificationToken);
                
                if (validationResult != null)
                {
                    return validationResult;
                }

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

                var faceRegistration = await _context.FaceRegistrations
                    .FirstOrDefaultAsync(fr => 
                        fr.FaceId == request.MatchedFaceId && 
                        fr.EmployeeId == request.EmployeeId && 
                        fr.IsActive);
                
                if (faceRegistration == null)
                {
                    _logger.LogWarning("Invalid or inactive FaceId - FaceId: {FaceId}, EmployeeId: {EmployeeId}",
                        request.MatchedFaceId, request.EmployeeId);
                    return new AttendanceCheckInResult
                    {
                        Success = false,
                        Message = "Khuôn mặt đăng ký không hợp lệ hoặc đã bị vô hiệu hóa. Vui lòng đăng ký lại khuôn mặt.",
                        EmployeeId = request.EmployeeId
                    };
                }

                _verificationTokenCache.TryAdd(request.VerificationToken, DateTime.UtcNow.AddSeconds(60));
                CleanupExpiredTokens();

                // Tìm attendance theo ca cụ thể (nếu có WorkShiftID) hoặc lấy bất kỳ attendance nào chưa checkout
                var attendance = await GetTodayAttendanceByShiftAsync(request.EmployeeId, request.WorkShiftID);

                if (attendance == null || !attendance.CheckInDateTime.HasValue)
                {
                    return new AttendanceCheckInResult
                    {
                        Success = false,
                        Message = request.WorkShiftID.HasValue && request.WorkShiftID.Value > 0
                            ? $"Không tìm thấy bản ghi chấm công vào cho ca này hôm nay"
                            : "Không tìm thấy bản ghi chấm công vào hôm nay",
                        EmployeeId = request.EmployeeId
                    };
                }

                if (attendance.CheckOutDateTime.HasValue)
                {
                    return new AttendanceCheckInResult
                    {
                        Success = false,
                        Message = request.WorkShiftID.HasValue && request.WorkShiftID.Value > 0
                            ? "Bạn đã chấm công ra cho ca này hôm nay"
                            : "Bạn đã chấm công ra hôm nay",
                        EmployeeId = request.EmployeeId,
                        CheckInDateTime = attendance.CheckInDateTime.Value
                    };
                }

                var checkOutDateTime = ParseAsVietnamTime(request.CheckOutDateTime);
                
                attendance.CheckOutDateTime = checkOutDateTime;
                attendance.CheckOut = checkOutDateTime.TimeOfDay;
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

                _logger.LogInformation("Check-out successful - EmployeeId: {EmployeeId}, AttendanceId: {AttendanceId}, FaceId: {FaceId}, Confidence: {Confidence}",
                    request.EmployeeId, attendance.ID, faceRegistration.FaceId, request.MatchConfidence);

                return new AttendanceCheckInResult
                {
                    Success = true,
                    Message = "Chấm công ra thành công",
                    AttendanceId = attendance.ID ?? 0,
                    EmployeeId = request.EmployeeId,
                    EmployeeName = employee.UserName ?? employee.Email ?? "Unknown",
                    CheckInDateTime = attendance.CheckInDateTime.Value,
                    Status = attendance.Status ?? AttendanceStatusEnum.Present
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing no-image check-out - EmployeeId: {EmployeeId}", request.EmployeeId);
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
                    .Include(a => a.ShiftAssignment)
                        .ThenInclude(sa => sa.WorkShift)
                    .FirstOrDefaultAsync(a => 
                        a.EmployeeId == employeeId && 
                        a.CheckInDateTime.HasValue && 
                        a.CheckInDateTime.Value.Date == today);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting today's attendance - EmployeeId: {EmployeeId}", employeeId);
                throw;
            }
        }

        public async Task<Attendance?> GetTodayAttendanceByShiftAsync(string employeeId, int? workShiftID)
        {
            try
            {
                var today = DateTime.Today;
                var query = _context.Attendances
                    .Include(a => a.Employee)
                    .Include(a => a.AttendanceMachine)
                    .Include(a => a.ShiftAssignment)
                        .ThenInclude(sa => sa.WorkShift)
                    .Where(a => 
                        a.EmployeeId == employeeId && 
                        a.CheckInDateTime.HasValue && 
                        a.CheckInDateTime.Value.Date == today);

                // Nếu có WorkShiftID, tìm attendance theo ca cụ thể
                if (workShiftID.HasValue && workShiftID.Value > 0)
                {
                    query = query.Where(a => 
                        a.ShiftAssignment != null && 
                        a.ShiftAssignment.WorkShiftID == workShiftID.Value);
                }

                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting today's attendance by shift - EmployeeId: {EmployeeId}, WorkShiftID: {WorkShiftID}", 
                    employeeId, workShiftID);
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
                    .Include(a => a.ShiftAssignment)
                        .ThenInclude(sa => sa.WorkShift)
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
                _logger.LogError(ex, "Error getting attendance - EmployeeId: {EmployeeId}", employeeId);
                throw;
            }
        }

        public async Task<List<int>> GetTodayCheckedShiftsAsync(string employeeId)
        {
            try
            {
                var today = DateTime.Today;
                return await _context.Attendances
                    .Include(a => a.ShiftAssignment)
                    .Where(a => 
                        a.EmployeeId == employeeId && 
                        a.CheckInDateTime.HasValue &&
                        a.CheckInDateTime.Value.Date == today &&
                        a.ShiftAssignment != null &&
                        a.ShiftAssignment.WorkShiftID > 0)
                    .Select(a => a.ShiftAssignment.WorkShiftID)
                    .Distinct()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting today's checked shifts - EmployeeId: {EmployeeId}", employeeId);
                throw;
            }
        }

        private AttendanceCheckInResult? ValidateFaceVerificationRequest(string employeeId, string? matchedFaceId, 
            float? matchConfidence, DateTime? verificationTimestamp, string? verificationToken)
        {
            if (string.IsNullOrWhiteSpace(matchedFaceId))
            {
                return new AttendanceCheckInResult
                {
                    Success = false,
                    Message = "Không có thông tin xác thực khuôn mặt. Vui lòng quét lại khuôn mặt.",
                    EmployeeId = employeeId
                };
            }

            const float REQUIRED_CONFIDENCE_THRESHOLD = 0.80f;
            if (!matchConfidence.HasValue || matchConfidence.Value < REQUIRED_CONFIDENCE_THRESHOLD)
            {
                _logger.LogWarning("Insufficient confidence - EmployeeId: {EmployeeId}, Confidence: {Confidence}, Required: {Required}",
                    employeeId, matchConfidence, REQUIRED_CONFIDENCE_THRESHOLD);
                return new AttendanceCheckInResult
                {
                    Success = false,
                    Message = $"Độ tin cậy nhận diện không đạt yêu cầu ({(matchConfidence * 100):F1}% < {REQUIRED_CONFIDENCE_THRESHOLD * 100:F0}%). Vui lòng quét lại khuôn mặt.",
                    EmployeeId = employeeId
                };
            }

            if (!verificationTimestamp.HasValue)
            {
                return new AttendanceCheckInResult
                {
                    Success = false,
                    Message = "Thông tin xác thực không hợp lệ. Vui lòng quét lại khuôn mặt.",
                    EmployeeId = employeeId
                };
            }

            var verificationTime = verificationTimestamp.Value;
            if (verificationTime.Kind == DateTimeKind.Unspecified)
            {
                verificationTime = DateTime.SpecifyKind(verificationTime, DateTimeKind.Utc);
            }
            else if (verificationTime.Kind == DateTimeKind.Local)
            {
                verificationTime = verificationTime.ToUniversalTime();
            }

            var verificationAge = DateTime.UtcNow - verificationTime;
            const int MAX_VERIFICATION_AGE_SECONDS = 60;
            if (verificationAge.TotalSeconds > MAX_VERIFICATION_AGE_SECONDS)
            {
                _logger.LogWarning("Verification timestamp expired - EmployeeId: {EmployeeId}, Age: {Age:F1}s, Max: {Max}s",
                    employeeId, verificationAge.TotalSeconds, MAX_VERIFICATION_AGE_SECONDS);
                return new AttendanceCheckInResult
                {
                    Success = false,
                    Message = "Phiên xác thực đã hết hạn. Vui lòng quét lại khuôn mặt.",
                    EmployeeId = employeeId
                };
            }
            
            if (verificationAge.TotalSeconds < -5)
            {
                _logger.LogWarning("Verification timestamp too far in future - EmployeeId: {EmployeeId}, Age: {Age:F1}s",
                    employeeId, verificationAge.TotalSeconds);
                return new AttendanceCheckInResult
                {
                    Success = false,
                    Message = "Thời gian xác thực không hợp lệ. Vui lòng kiểm tra đồng hồ thiết bị.",
                    EmployeeId = employeeId
                };
            }

            if (string.IsNullOrWhiteSpace(verificationToken))
            {
                return new AttendanceCheckInResult
                {
                    Success = false,
                    Message = "Thông tin xác thực không hợp lệ. Vui lòng quét lại khuôn mặt.",
                    EmployeeId = employeeId
                };
            }

            if (_verificationTokenCache.ContainsKey(verificationToken))
            {
                _logger.LogWarning("Replay attack detected - EmployeeId: {EmployeeId}, Token: {Token}",
                    employeeId, verificationToken.Substring(0, Math.Min(8, verificationToken.Length)));
                return new AttendanceCheckInResult
                {
                    Success = false,
                    Message = "Phiên xác thực đã được sử dụng. Vui lòng quét lại khuôn mặt.",
                    EmployeeId = employeeId
                };
            }

            return null;
        }

        private async Task<int?> GetOrCreateShiftAssignmentAsync(string employeeId, int? workShiftID, DateTime workDate)
        {
            if (!workShiftID.HasValue || workShiftID.Value <= 0)
            {
                return null;
            }

            var existingShiftAssignment = await _context.ShiftAssignments
                .FirstOrDefaultAsync(sa => sa.EmployeeID == employeeId 
                                         && sa.WorkShiftID == workShiftID.Value 
                                         && sa.WorkDate.Date == workDate.Date);
            
            if (existingShiftAssignment != null)
            {
                return existingShiftAssignment.ID;
            }

            var newShiftAssignment = new ShiftAssignment
            {
                EmployeeID = employeeId,
                WorkShiftID = workShiftID.Value,
                WorkDate = workDate
            };
            
            _context.ShiftAssignments.Add(newShiftAssignment);
            await _context.SaveChangesAsync();
            return newShiftAssignment.ID;
        }
    }
}