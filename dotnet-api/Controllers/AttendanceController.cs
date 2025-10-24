using Microsoft.AspNetCore.Mvc;
using dotnet_api.Services;
using dotnet_api.DTOs;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly SimpleAttendanceService _attendanceService;

        public AttendanceController(SimpleAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
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
        /// Lấy thông tin chấm công hôm nay của nhân viên
        /// </summary>
        /// <param name="employeeId">ID nhân viên</param>
        /// <returns>Thông tin chấm công hôm nay</returns>
        [HttpGet("today/{employeeId}")]
        public async Task<ActionResult> GetTodayAttendance(string employeeId)
        {
            try
            {
                var attendance = await _attendanceService.GetTodayAttendanceAsync(employeeId);
                
                if (attendance == null)
                {
                    return NotFound(new { message = "Không tìm thấy bản ghi chấm công hôm nay" });
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
                    imageCheckIn = attendance.ImageCheckIn,
                    imageCheckOut = attendance.ImageCheckOut,
                    notes = attendance.Notes
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
                    imageCheckIn = a.ImageCheckIn,
                    imageCheckOut = a.ImageCheckOut,
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
        /// Test endpoint để kiểm tra kết nối API
        /// </summary>
        /// <returns>Thông tin server và thời gian hiện tại</returns>
        [HttpGet("today/test")]
        public ActionResult TestConnection()
        {
            try
            {
                return Ok(new
                {
                    message = "API Server is running",
                    timestamp = DateTime.Now,
                    server = "Attendance API",
                    version = "1.0.0",
                    status = "healthy"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Server error", error = ex.Message });
            }
        }
    }
}