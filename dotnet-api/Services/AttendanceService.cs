﻿using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.Data.Enums;
using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AttendanceService> _logger;
        private readonly IFaceRecognitionService _faceRecognitionService;

        public AttendanceService(
            ApplicationDbContext context, 
            ILogger<AttendanceService> logger,
            IFaceRecognitionService faceRecognitionService)
        {
            _context = context;
            _logger = logger;
            _faceRecognitionService = faceRecognitionService;
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
                        FaceRecognitionConfidence = existingAttendance.FaceRecognitionConfidence ?? 0,
                        Status = existingAttendance.Status ?? AttendanceStatusEnum.Present
                    };
                }

                // Nhận dạng khuôn mặt
                var faceResult = await _faceRecognitionService.RecognizeFaceAsync(
                    Convert.FromBase64String(request.ImageBase64));

                if (!faceResult.Success || faceResult.EmployeeId != request.EmployeeId)
                {
                    return new AttendanceCheckInResult
                    {
                        Success = false,
                        Message = "Không thể xác thực khuôn mặt hoặc không khớp với nhân viên",
                        EmployeeId = request.EmployeeId
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

                // Tạo hoặc cập nhật attendance record
                var attendance = existingAttendance ?? new Attendance
                {
                    EmployeeId = request.EmployeeId,
                    CreatedDate = DateTime.Now
                };

                attendance.CheckInDateTime = request.CheckInDateTime;
                attendance.CheckIn = request.CheckInDateTime.TimeOfDay;
                attendance.ImageCheckIn = await SaveAttendanceImageAsync(request.ImageBase64, "checkin", request.EmployeeId);
                attendance.CheckInLocation = request.Location ?? $"{request.Latitude},{request.Longitude}";
                attendance.FaceRecognitionConfidence = faceResult.Confidence;
                attendance.AttendanceMachineId = request.AttendanceMachineId;
                attendance.Status = AttendanceStatusEnum.Present;
                attendance.LastUpdated = DateTime.Now;

                if (existingAttendance == null)
                {
                    _context.Attendances.Add(attendance);
                }
                else
                {
                    _context.Attendances.Update(attendance);
                }

                await _context.SaveChangesAsync();

                _logger.LogInformation($"Check-in successful for employee: {request.EmployeeId}, Attendance ID: {attendance.ID}");

                return new AttendanceCheckInResult
                {
                    Success = true,
                    Message = "Chấm công thành công",
                    AttendanceId = attendance.ID ?? 0,
                    EmployeeId = request.EmployeeId,
                    EmployeeName = employee.UserName,
                    CheckInDateTime = attendance.CheckInDateTime.Value,
                    FaceRecognitionConfidence = faceResult.Confidence,
                    Status = attendance.Status.Value
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

        public async Task<bool> CheckOutAsync(string employeeId, DateTime checkOutDateTime, string? imageBase64 = null)
        {
            try
            {
                var attendance = await GetTodayAttendanceAsync(employeeId);
                if (attendance == null || !attendance.CheckInDateTime.HasValue)
                {
                    _logger.LogWarning($"No check-in record found for employee: {employeeId}");
                    return false;
                }

                attendance.CheckOutDateTime = checkOutDateTime;
                attendance.CheckOut = checkOutDateTime.TimeOfDay;
                attendance.LastUpdated = DateTime.Now;

                if (!string.IsNullOrEmpty(imageBase64))
                {
                    attendance.ImageCheckOut = await SaveAttendanceImageAsync(imageBase64, "checkout", employeeId);
                }

                _context.Attendances.Update(attendance);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Check-out successful for employee: {employeeId}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing check-out for employee: {employeeId}");
                return false;
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

        public async Task<bool> HasCheckedInTodayAsync(string employeeId)
        {
            try
            {
                var today = DateTime.Today;
                return await _context.Attendances
                    .AnyAsync(a => 
                        a.EmployeeId == employeeId && 
                        a.CheckInDateTime.HasValue && 
                        a.CheckInDateTime.Value.Date == today);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error checking if employee {employeeId} has checked in today");
                throw;
            }
        }

        public async Task<List<Attendance>> GetAttendanceByDateAsync(DateTime date)
        {
            try
            {
                return await _context.Attendances
                    .Include(a => a.Employee)
                    .Include(a => a.AttendanceMachine)
                    .Where(a => a.CheckInDateTime.HasValue && a.CheckInDateTime.Value.Date == date.Date)
                    .OrderBy(a => a.CheckInDateTime)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting attendance for date: {date:yyyy-MM-dd}");
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