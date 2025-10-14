using Microsoft.AspNetCore.Mvc;
using dotnet_api.Services;
using dotnet_api.DTOs;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceDataController : ControllerBase
    {
        private readonly AttendanceDataService _attendanceDataService;

        public AttendanceDataController(AttendanceDataService attendanceDataService)
        {
            _attendanceDataService = attendanceDataService;
        }

        /// <summary>
        /// Lấy tất cả dữ liệu chấm công
        /// </summary>
        /// <returns>Danh sách dữ liệu chấm công</returns>
        [HttpGet]
        public async Task<ActionResult<List<AttendanceDataDto>>> GetAllAttendanceData()
        {
            try
            {
                var attendanceData = await _attendanceDataService.GetAllAttendanceDataAsync();
                return Ok(attendanceData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi lấy dữ liệu chấm công", error = ex.Message });
            }
        }

        /// <summary>
        /// Lấy dữ liệu chấm công theo mã nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>Danh sách dữ liệu chấm công của nhân viên</returns>
        [HttpGet("employee/{employeeCode}")]
        public async Task<ActionResult<List<AttendanceDataDto>>> GetAttendanceDataByEmployee(string employeeCode)
        {
            try
            {
                var attendanceData = await _attendanceDataService.GetAttendanceDataByEmployeeAsync(employeeCode);
                return Ok(attendanceData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi lấy dữ liệu chấm công của nhân viên", error = ex.Message });
            }
        }

        /// <summary>
        /// Lấy dữ liệu chấm công theo khoảng thời gian
        /// </summary>
        /// <param name="startDate">Ngày bắt đầu (yyyy-MM-dd)</param>
        /// <param name="endDate">Ngày kết thúc (yyyy-MM-dd)</param>
        /// <returns>Danh sách dữ liệu chấm công trong khoảng thời gian</returns>
        [HttpGet("daterange")]
        public async Task<ActionResult<List<AttendanceDataDto>>> GetAttendanceDataByDateRange(
            [FromQuery] string startDate, 
            [FromQuery] string endDate)
        {
            try
            {
                if (!DateTime.TryParse(startDate, out DateTime start) || !DateTime.TryParse(endDate, out DateTime end))
                {
                    return BadRequest(new { message = "Định dạng ngày không hợp lệ. Sử dụng định dạng yyyy-MM-dd" });
                }

                var attendanceData = await _attendanceDataService.GetAttendanceDataByDateRangeAsync(start, end);
                return Ok(attendanceData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi lấy dữ liệu chấm công theo khoảng thời gian", error = ex.Message });
            }
        }

        /// <summary>
        /// Lấy dữ liệu chấm công theo tháng
        /// </summary>
        /// <param name="year">Năm</param>
        /// <param name="month">Tháng</param>
        /// <returns>Danh sách dữ liệu chấm công trong tháng</returns>
        [HttpGet("month")]
        public async Task<ActionResult<List<AttendanceDataDto>>> GetAttendanceDataByMonth(
            [FromQuery] int year, 
            [FromQuery] int month)
        {
            try
            {
                if (year < 1900 || year > 2100 || month < 1 || month > 12)
                {
                    return BadRequest(new { message = "Năm hoặc tháng không hợp lệ" });
                }

                var startDate = new DateTime(year, month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                var attendanceData = await _attendanceDataService.GetAttendanceDataByDateRangeAsync(startDate, endDate);
                return Ok(attendanceData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi lấy dữ liệu chấm công theo tháng", error = ex.Message });
            }
        }
    }
}
