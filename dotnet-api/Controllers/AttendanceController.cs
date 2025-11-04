using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using dotnet_api.Services;
using dotnet_api.DTOs;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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