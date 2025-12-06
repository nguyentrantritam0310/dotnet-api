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

        private string GetCurrentUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        private ActionResult ValidateUserAccess(string requestedEmployeeId, string operation = "access")
        {
            var currentUserId = GetCurrentUserId();
            if (string.IsNullOrEmpty(currentUserId))
            {
                return Unauthorized(new { message = "Không thể xác định người dùng từ token" });
            }

            if (requestedEmployeeId != currentUserId)
            {
                _logger.LogWarning("Unauthorized access attempt - UserId: {CurrentUserId}, RequestedEmployeeId: {RequestedEmployeeId}, Operation: {Operation}", 
                    currentUserId, requestedEmployeeId, operation);
                return Forbid("Bạn chỉ có thể chấm công cho chính mình");
            }

            return null;
        }

        private ActionResult HandleServiceResult<T>(T result) where T : class
        {
            if (result is AttendanceCheckInResult attendanceResult)
            {
                return attendanceResult.Success ? Ok(result) : BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("checkin-noimage")]
        public async Task<ActionResult<AttendanceCheckInResult>> CheckInNoImage([FromBody] AttendanceCheckInNoImageRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var validationResult = ValidateUserAccess(request.EmployeeId, "check-in");
            if (validationResult != null)
            {
                return validationResult;
            }

            try
            {
                var currentUserId = GetCurrentUserId();
                var result = await _attendanceService.CheckInNoImageAsync(request, currentUserId);
                return HandleServiceResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CheckInNoImage - EmployeeId: {EmployeeId}", request?.EmployeeId);
                return StatusCode(500, new { message = "Lỗi hệ thống", error = ex.Message });
            }
        }

        [HttpPost("checkout-noimage")]
        public async Task<ActionResult<AttendanceCheckInResult>> CheckOutNoImage([FromBody] AttendanceCheckOutNoImageRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var validationResult = ValidateUserAccess(request.EmployeeId, "check-out");
            if (validationResult != null)
            {
                return validationResult;
            }

            try
            {
                var currentUserId = GetCurrentUserId();
                var result = await _attendanceService.CheckOutNoImageAsync(request, currentUserId);
                return HandleServiceResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CheckOutNoImage - EmployeeId: {EmployeeId}", request?.EmployeeId);
                return StatusCode(500, new { message = "Lỗi hệ thống", error = ex.Message });
            }
        }

        [HttpGet("today/{employeeId}")]
        public async Task<ActionResult> GetTodayAttendance(string employeeId)
        {
            var validationResult = ValidateUserAccess(employeeId, "view attendance");
            if (validationResult != null)
            {
                return validationResult;
            }

            try
            {
                var today = DateTime.Today;
                
                // Ưu tiên tìm record chưa checkout (CheckOutDateTime == null) để checkout
                // Nếu không có, mới lấy record mới nhất
                var attendance = await _context.Attendances
                    .Include(a => a.Employee)
                    .Include(a => a.AttendanceMachine)
                    .Include(a => a.ShiftAssignment)
                        .ThenInclude(sa => sa.WorkShift)
                    .Where(a => 
                        a.EmployeeId == employeeId && 
                        a.CheckInDateTime.HasValue && 
                        a.CheckInDateTime.Value.Date == today &&
                        !a.CheckOutDateTime.HasValue) // Ưu tiên record chưa checkout
                    .OrderByDescending(a => a.CheckInDateTime)
                    .FirstOrDefaultAsync();
                
                // Nếu không tìm thấy record chưa checkout, lấy record mới nhất (có thể đã checkout)
                if (attendance == null)
                {
                    attendance = await _context.Attendances
                        .Include(a => a.Employee)
                        .Include(a => a.AttendanceMachine)
                        .Include(a => a.ShiftAssignment)
                            .ThenInclude(sa => sa.WorkShift)
                        .Where(a => 
                            a.EmployeeId == employeeId && 
                            a.CheckInDateTime.HasValue && 
                            a.CheckInDateTime.Value.Date == today)
                        .OrderByDescending(a => a.CheckInDateTime)
                        .FirstOrDefaultAsync();
                }
                
                if (attendance == null)
                {
                    var attendances = await _attendanceService.GetEmployeeAttendanceAsync(employeeId, today, today);
                    if (attendances == null || attendances.Count == 0)
                    {
                        return NotFound(new { message = "Không tìm thấy bản ghi chấm công hôm nay" });
                    }

                    // Ưu tiên tìm record chưa checkout
                    attendance = attendances.FirstOrDefault(a => 
                        a.CheckInDateTime.HasValue && 
                        a.CheckInDateTime.Value.Date == today &&
                        !a.CheckOutDateTime.HasValue) 
                        ?? attendances.OrderByDescending(a => a.CheckInDateTime).FirstOrDefault();
                    
                    if (attendance == null)
                    {
                        return NotFound(new { message = "Không tìm thấy bản ghi chấm công hôm nay" });
                    }

                    // Đảm bảo ShiftAssignment được load nếu có ShiftAssignmentID
                    if (attendance.ShiftAssignment == null && attendance.ShiftAssignmentID.HasValue)
                    {
                        attendance = await _context.Attendances
                            .Include(a => a.Employee)
                            .Include(a => a.AttendanceMachine)
                            .Include(a => a.ShiftAssignment)
                                .ThenInclude(sa => sa.WorkShift)
                            .FirstOrDefaultAsync(a => a.ID == attendance.ID);
                    }
                }
                
                // Đảm bảo ShiftAssignment được load nếu có ShiftAssignmentID (fallback)
                if (attendance != null && attendance.ShiftAssignment == null && attendance.ShiftAssignmentID.HasValue)
                {
                    attendance = await _context.Attendances
                        .Include(a => a.Employee)
                        .Include(a => a.AttendanceMachine)
                        .Include(a => a.ShiftAssignment)
                            .ThenInclude(sa => sa.WorkShift)
                        .FirstOrDefaultAsync(a => a.ID == attendance.ID);
                }
                
                if (attendance == null)
                {
                    return NotFound(new { message = "Không tìm thấy bản ghi chấm công hôm nay" });
                }

                var workShiftID = await GetWorkShiftIdAsync(attendance);

                var responseData = new
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
                    workShiftID = workShiftID,
                    shiftAssignmentID = attendance.ShiftAssignmentID // THÊM FIELD NÀY ĐỂ FRONTEND CÓ THỂ LẤY WorkShiftID
                };

                return Ok(responseData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetTodayAttendance - EmployeeId: {EmployeeId}", employeeId);
                return StatusCode(500, new { message = "Lỗi hệ thống", error = ex.Message });
            }
        }

        private async Task<int?> GetWorkShiftIdAsync(Data.Entities.Attendance attendance)
        {
            if (attendance.ShiftAssignment?.WorkShiftID != null)
            {
                return attendance.ShiftAssignment.WorkShiftID;
            }

            if (attendance.ShiftAssignmentID.HasValue)
            {
                var shiftAssignment = await _context.ShiftAssignments
                    .FirstOrDefaultAsync(sa => sa.ID == attendance.ShiftAssignmentID.Value);
                
                if (shiftAssignment != null)
                {
                    return shiftAssignment.WorkShiftID;
                }
            }

            return null;
        }

        [HttpGet("today-shifts/{employeeId}")]
        public async Task<ActionResult> GetTodayCheckedShifts(string employeeId)
        {
            var validationResult = ValidateUserAccess(employeeId, "view checked shifts");
            if (validationResult != null)
            {
                return validationResult;
            }

            try
            {
                var checkedShifts = await _attendanceService.GetTodayCheckedShiftsAsync(employeeId);
                
                return Ok(new
                {
                    checkedShiftIds = checkedShifts
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetTodayCheckedShifts - EmployeeId: {EmployeeId}", employeeId);
                return StatusCode(500, new { message = "Lỗi hệ thống", error = ex.Message });
            }
        }

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
                _logger.LogError(ex, "Error in GetAttendanceHistory - EmployeeId: {EmployeeId}, StartDate: {StartDate}, EndDate: {EndDate}", 
                    employeeId, startDate, endDate);
                return StatusCode(500, new { message = "Lỗi hệ thống", error = ex.Message });
            }
        }

        [HttpPut("{attendanceId}/shift")]
        public async Task<ActionResult> UpdateAttendanceShift(int attendanceId, [FromBody] UpdateAttendanceShiftRequest request)
        {
            try
            {
                if (!request.ShiftAssignmentID.HasValue)
                {
                    return BadRequest(new { message = "ShiftAssignmentID là bắt buộc" });
                }

                var attendance = await _context.Attendances
                    .Include(a => a.ShiftAssignment)
                    .FirstOrDefaultAsync(a => a.ID == attendanceId);

                if (attendance == null)
                {
                    return NotFound(new { message = "Không tìm thấy bản ghi chấm công" });
                }

                // Kiểm tra ShiftAssignment có tồn tại không
                var shiftAssignment = await _context.ShiftAssignments
                    .FirstOrDefaultAsync(sa => sa.ID == request.ShiftAssignmentID.Value);

                if (shiftAssignment == null)
                {
                    return BadRequest(new { message = "Không tìm thấy phân ca với ID đã cho" });
                }

                // Cập nhật ShiftAssignmentID
                attendance.ShiftAssignmentID = request.ShiftAssignmentID.Value;
                attendance.LastUpdated = DateTime.Now;

                _context.Attendances.Update(attendance);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Updated attendance shift - AttendanceId: {AttendanceId}, ShiftAssignmentID: {ShiftAssignmentID}",
                    attendanceId, request.ShiftAssignmentID.Value);

                return Ok(new { 
                    message = "Cập nhật ca làm việc thành công",
                    attendanceId = attendance.ID,
                    shiftAssignmentID = attendance.ShiftAssignmentID
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAttendanceShift - AttendanceId: {AttendanceId}", attendanceId);
                return StatusCode(500, new { message = "Lỗi hệ thống", error = ex.Message });
            }
        }

    }

    public class UpdateAttendanceShiftRequest
    {
        public int? ShiftAssignmentID { get; set; }
    }
}