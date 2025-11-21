using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using dotnet_api.Services;
using dotnet_api.DTOs;
using dotnet_api.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AttendanceController : ControllerBase
    {
        private readonly SimpleAttendanceService _attendanceService;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AttendanceController> _logger;

        public AttendanceController(SimpleAttendanceService attendanceService, ApplicationDbContext context, ILogger<AttendanceController> logger)
        {
            _attendanceService = attendanceService;
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Chấm công vào (Check-in)
        /// </summary>
        /// <param name="request">Thông tin chấm công vào</param>
        /// <returns>Kết quả chấm công</returns>
        [HttpPost("checkin")]
        public async Task<ActionResult<AttendanceCheckInResult>> CheckIn([FromBody] AttendanceCheckInRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _attendanceService.CheckInAsync(request);
                
                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", error = ex.Message });
            }
        }

        /// <summary>
        /// Chấm công vào (không ảnh) - Requires face verification
        /// </summary>
        [HttpPost("checkin-noimage")]
        public async Task<ActionResult<AttendanceCheckInResult>> CheckInNoImage([FromBody] AttendanceCheckInNoImageRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // SECURITY: Validate that user can only check-in for themselves
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(currentUserId))
                {
                    return Unauthorized(new { message = "Không thể xác định người dùng từ token" });
                }

                if (request.EmployeeId != currentUserId)
                {
                    return Forbid("Bạn chỉ có thể chấm công cho chính mình");
                }

                var result = await _attendanceService.CheckInNoImageAsync(request, currentUserId);
                if (result.Success) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", error = ex.Message });
            }
        }

        /// <summary>
        /// Chấm công ra (Check-out)
        /// </summary>
        /// <param name="request">Thông tin chấm công ra</param>
        /// <returns>Kết quả chấm công</returns>
        [HttpPost("checkout")]
        public async Task<ActionResult<AttendanceCheckInResult>> CheckOut([FromBody] AttendanceCheckOutRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _attendanceService.CheckOutAsync(request);
                
                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", error = ex.Message });
            }
        }

        /// <summary>
        /// Chấm công ra (không ảnh)
        /// </summary>
        [HttpPost("checkout-noimage")]
        public async Task<ActionResult<AttendanceCheckInResult>> CheckOutNoImage([FromBody] AttendanceCheckOutNoImageRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // SECURITY: Validate that user can only check-out for themselves
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(currentUserId))
                {
                    return Unauthorized(new { message = "Không thể xác định người dùng từ token" });
                }

                if (request.EmployeeId != currentUserId)
                {
                    return Forbid("Bạn chỉ có thể chấm công cho chính mình");
                }

                var result = await _attendanceService.CheckOutNoImageAsync(request, currentUserId);
                if (result.Success) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", error = ex.Message });
            }
        }

        /// <summary>
        /// Lấy thông tin chấm công hôm nay của nhân viên
        /// </summary>
        /// <param name="employeeId">ID nhân viên</param>
        /// <returns>Thông tin chấm công hôm nay</returns>
        [HttpGet("today/{employeeId}")]
        public async Task<ActionResult> GetTodayAttendance(string employeeId)
        {
            try
            {
                // SECURITY: Validate that user can only view their own attendance
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(currentUserId))
                {
                    return Unauthorized(new { message = "Không thể xác định người dùng từ token" });
                }

                if (employeeId != currentUserId)
                {
                    _logger.LogWarning($"🚨 [SECURITY] User {currentUserId} attempted to access attendance for {employeeId}");
                    return Forbid("Bạn chỉ có thể xem thông tin chấm công của chính mình");
                }

                // Ưu tiên lấy attendance chưa checkout (để checkout)
                // Nếu không có thì lấy attendance mới nhất (đã checkout)
                var today = DateTime.Today;
                var attendances = await _attendanceService.GetEmployeeAttendanceAsync(employeeId, today, today);
                
                if (attendances == null || attendances.Count == 0)
                {
                    return NotFound(new { message = "Không tìm thấy bản ghi chấm công hôm nay" });
                }

                // Ưu tiên lấy attendance chưa checkout
                var attendance = attendances.FirstOrDefault(a => 
                    a.CheckInDateTime.HasValue && 
                    a.CheckInDateTime.Value.Date == today &&
                    !a.CheckOutDateTime.HasValue) 
                    ?? attendances.OrderByDescending(a => a.CheckInDateTime).FirstOrDefault();
                
                if (attendance == null)
                {
                    return NotFound(new { message = "Không tìm thấy bản ghi chấm công hôm nay" });
                }

                // Reload attendance với include đầy đủ nếu ShiftAssignment null
                if (attendance.ShiftAssignment == null && attendance.ShiftAssignmentID.HasValue)
                {
                    _logger.LogWarning($"⚠️ [GET_TODAY] Reloading attendance {attendance.ID} with full includes...");
                    attendance = await _context.Attendances
                        .Include(a => a.Employee)
                        .Include(a => a.AttendanceMachine)
                        .Include(a => a.ShiftAssignment)
                            .ThenInclude(sa => sa.WorkShift)
                        .FirstOrDefaultAsync(a => a.ID == attendance.ID);
                }

                // Log để debug
                _logger.LogInformation($"📊 [GET_TODAY] Found attendance ID: {attendance.ID}, EmployeeId: {attendance.EmployeeId}, CheckIn: {attendance.CheckInDateTime}, CheckOut: {attendance.CheckOutDateTime}, ShiftAssignmentID: {attendance.ShiftAssignmentID}, WorkShiftID: {attendance.ShiftAssignment?.WorkShiftID}");

                // Fallback: Nếu ShiftAssignment null nhưng có ShiftAssignmentID, load trực tiếp từ database
                int? workShiftID = attendance.ShiftAssignment?.WorkShiftID;
                if (!workShiftID.HasValue && attendance.ShiftAssignmentID.HasValue)
                {
                    _logger.LogWarning($"⚠️ [GET_TODAY] ShiftAssignment is null but ShiftAssignmentID exists: {attendance.ShiftAssignmentID}. Loading directly from database...");
                    var shiftAssignment = await _context.ShiftAssignments
                        .FirstOrDefaultAsync(sa => sa.ID == attendance.ShiftAssignmentID.Value);
                    if (shiftAssignment != null)
                    {
                        workShiftID = shiftAssignment.WorkShiftID;
                        _logger.LogInformation($"✅ [GET_TODAY] Loaded ShiftAssignment directly. WorkShiftID: {workShiftID}");
                    }
                    else
                    {
                        _logger.LogError($"❌ [GET_TODAY] ShiftAssignment with ID {attendance.ShiftAssignmentID} not found in database!");
                    }
                }

                return Ok(new
                {
                    id = attendance.ID,
                    employeeId = attendance.EmployeeId,
                    employeeName = attendance.Employee?.UserName ?? attendance.Employee?.Email,
                    checkInDateTime = attendance.CheckInDateTime,
                    checkOutDateTime = attendance.CheckOutDateTime,
                    checkInLocation = attendance.CheckInLocation,
                    checkOutLocation = attendance.CheckOutLocation,
                    status = attendance.Status,
                    notes = attendance.Notes,
                    workShiftID = workShiftID
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", error = ex.Message });
            }
        }

        /// <summary>
        /// Lấy danh sách các ca đã chấm công hôm nay (để ẩn khỏi dropdown khi check-in)
        /// </summary>
        /// <param name="employeeId">ID nhân viên</param>
        /// <returns>Danh sách WorkShiftID đã chấm công hôm nay</returns>
        [HttpGet("today-shifts/{employeeId}")]
        public async Task<ActionResult> GetTodayCheckedShifts(string employeeId)
        {
            try
            {
                // SECURITY: Validate that user can only view their own checked shifts
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(currentUserId))
                {
                    return Unauthorized(new { message = "Không thể xác định người dùng từ token" });
                }

                if (employeeId != currentUserId)
                {
                    _logger.LogWarning($"🚨 [SECURITY] User {currentUserId} attempted to access checked shifts for {employeeId}");
                    return Forbid("Bạn chỉ có thể xem thông tin chấm công của chính mình");
                }

                var today = DateTime.Today;
                var checkedShifts = await _attendanceService.GetTodayCheckedShiftsAsync(employeeId);
                
                return Ok(new
                {
                    checkedShiftIds = checkedShifts
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", error = ex.Message });
            }
        }

        /// <summary>
        /// Lấy lịch sử chấm công của nhân viên
        /// </summary>
        /// <param name="employeeId">ID nhân viên</param>
        /// <param name="startDate">Ngày bắt đầu (yyyy-MM-dd)</param>
        /// <param name="endDate">Ngày kết thúc (yyyy-MM-dd)</param>
        /// <returns>Danh sách chấm công</returns>
        [HttpGet("history/{employeeId}")]
        public async Task<ActionResult> GetAttendanceHistory(string employeeId, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            try
            {
                var attendances = await _attendanceService.GetEmployeeAttendanceAsync(employeeId, startDate, endDate);
                
                var result = attendances.Select(a => new
                {
                    id = a.ID,
                    employeeId = a.EmployeeId,
                    employeeName = a.Employee?.UserName ?? a.Employee?.Email,
                    checkInDateTime = a.CheckInDateTime,
                    checkOutDateTime = a.CheckOutDateTime,
                    checkInLocation = a.CheckInLocation,
                    checkOutLocation = a.CheckOutLocation,
                    status = a.Status,
                    notes = a.Notes,
                    createdDate = a.CreatedDate,
                    lastUpdated = a.LastUpdated
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", error = ex.Message });
            }
        }

        /// <summary>
        /// Serve attendance images
        /// </summary>
        /// <param name="filename">Image filename</param>
        /// <returns>Image file</returns>
        [HttpGet("image/{filename}")]
        public IActionResult GetAttendanceImage(string filename)
        {
            try
            {
                var uploadsPath = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production" 
                    ? "/var/www/backend/uploads" 
                    : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "uploads");
                
                var imagePath = Path.Combine(uploadsPath, "attendance", filename);
                
                // Debug logging
                Console.WriteLine($"Looking for image: {imagePath}");
                Console.WriteLine($"File exists: {System.IO.File.Exists(imagePath)}");
                Console.WriteLine($"Directory exists: {Directory.Exists(Path.GetDirectoryName(imagePath))}");
                
                if (Directory.Exists(Path.GetDirectoryName(imagePath)))
                {
                    var files = Directory.GetFiles(Path.GetDirectoryName(imagePath));
                    Console.WriteLine($"Files in directory: {string.Join(", ", files)}");
                }
                
                if (!System.IO.File.Exists(imagePath))
                {
                    return NotFound(new { 
                        message = "Image not found", 
                        searchedPath = imagePath,
                        uploadsPath = uploadsPath,
                        filename = filename
                    });
                }
                
                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                var contentType = GetContentType(filename);
                
                return File(imageBytes, contentType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error serving image", error = ex.Message });
            }
        }
        
        private string GetContentType(string filename)
        {
            var extension = Path.GetExtension(filename).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                _ => "application/octet-stream"
            };
        }
    }
}