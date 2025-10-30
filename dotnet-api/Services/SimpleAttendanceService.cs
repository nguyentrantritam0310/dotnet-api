using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.Data.Enums;
using dotnet_api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Services
{
    public class SimpleAttendanceService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SimpleAttendanceService> _logger;

        public SimpleAttendanceService(ApplicationDbContext context, ILogger<SimpleAttendanceService> logger)
        {
            _context = context;
            _logger = logger;
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

        public async Task<AttendanceCheckInResult> CheckInNoImageAsync(AttendanceCheckInNoImageRequest request)
        {
            try
            {
                _logger.LogInformation($"Processing no-image check-in for employee: {request.EmployeeId}");

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

                var attendance = new Attendance
                {
                    EmployeeId = request.EmployeeId,
                    CheckInDateTime = request.CheckInDateTime,
                    CheckIn = request.CheckInDateTime.TimeOfDay,
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

                // Cập nhật thông tin check-out
                attendance.CheckOutDateTime = request.CheckOutDateTime;
                attendance.CheckOut = request.CheckOutDateTime.TimeOfDay;
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



