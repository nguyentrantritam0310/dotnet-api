using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using dotnet_api.Services;
using dotnet_api.DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClosingController : ControllerBase
    {
        private readonly IClosingService _closingService;

        public ClosingController(IClosingService closingService)
        {
            _closingService = closingService;
        }

        [HttpGet("status/{employeeId}/{year}/{month}")]
        public async Task<ActionResult<ClosingStatusDTO>> GetClosingStatus(string employeeId, int year, int month)
        {
            try
            {
                var timeSheetClosed = await _closingService.IsTimeSheetClosedAsync(employeeId, year, month);
                var payrollClosed = await _closingService.IsPayrollClosedAsync(employeeId, year, month);
                var overtimeSheetClosed = await _closingService.IsOvertimeSheetClosedAsync(employeeId, year, month);

                // Lấy thông tin chi tiết về thời gian chốt
                var timeSheets = await _closingService.GetClosedTimeSheetsAsync(year, month);
                var payrolls = await _closingService.GetClosedPayrollsAsync(year, month);
                var overtimeSheets = await _closingService.GetClosedOvertimeSheetsAsync(year, month);

                var timeSheet = timeSheets.FirstOrDefault(ts => ts.EmployeeID == employeeId);
                var payroll = payrolls.FirstOrDefault(p => p.EmployeeID == employeeId);
                var overtimeSheet = overtimeSheets.FirstOrDefault(os => os.EmployeeID == employeeId);

                var status = new ClosingStatusDTO
                {
                    IsTimeSheetClosed = timeSheetClosed,
                    IsPayrollClosed = payrollClosed,
                    IsOvertimeSheetClosed = overtimeSheetClosed,
                    TimeSheetClosedAt = timeSheet?.ClosedAt,
                    PayrollClosedAt = payroll?.ClosedAt,
                    OvertimeSheetClosedAt = overtimeSheet?.ClosedAt,
                    TimeSheetClosedBy = timeSheet?.ClosedBy,
                    PayrollClosedBy = payroll?.ClosedBy,
                    OvertimeSheetClosedBy = overtimeSheet?.ClosedBy
                };

                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Lỗi khi lấy trạng thái chốt: {ex.Message}" });
            }
        }

        [HttpPost("timesheet/{employeeId}/{year}/{month}")]
        public async Task<ActionResult<ClosingResultDTO>> CloseTimeSheet(string employeeId, int year, int month)
        {
            try
            {
                var closedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Unknown";
                var result = await _closingService.CloseTimeSheetAsync(employeeId, year, month, closedBy);
                
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
                return BadRequest(new ClosingResultDTO
                {
                    Success = false,
                    Message = $"Lỗi khi chốt bảng công: {ex.Message}"
                });
            }
        }

        [HttpPost("payroll/{employeeId}/{year}/{month}")]
        public async Task<ActionResult<ClosingResultDTO>> ClosePayroll(string employeeId, int year, int month)
        {
            try
            {
                var closedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Unknown";
                var result = await _closingService.ClosePayrollAsync(employeeId, year, month, closedBy);
                
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
                return BadRequest(new ClosingResultDTO
                {
                    Success = false,
                    Message = $"Lỗi khi chốt bảng lương: {ex.Message}"
                });
            }
        }

        [HttpPost("test")]
        public async Task<ActionResult<object>> TestEndpoint()
        {
            try
            {
                var closedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Unknown";
                
                return Ok(new
                {
                    Message = "Test endpoint working",
                    ClosedBy = closedBy,
                    Timestamp = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        [HttpPost("payroll/all/{year}/{month}")]
        public async Task<ActionResult<ClosingResultDTO>> CloseAllPayrolls(int year, int month)
        {
            try
            {
                Console.WriteLine($"CloseAllPayrolls called with year: {year}, month: {month}");
                
                var closedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Unknown";
                Console.WriteLine($"Closed by: {closedBy}");
                
                var result = await _closingService.CloseAllPayrollsAsync(year, month, closedBy);
                Console.WriteLine($"CloseAllPayrollsAsync result: Success={result.Success}, Message={result.Message}");
                
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
                Console.WriteLine($"Exception in CloseAllPayrolls: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return BadRequest(new ClosingResultDTO
                {
                    Success = false,
                    Message = $"Lỗi khi chốt lương hàng loạt: {ex.Message}"
                });
            }
        }

        [HttpPost("overtime/{employeeId}/{year}/{month}")]
        public async Task<ActionResult<ClosingResultDTO>> CloseOvertimeSheet(string employeeId, int year, int month)
        {
            try
            {
                var closedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Unknown";
                var result = await _closingService.CloseOvertimeSheetAsync(employeeId, year, month, closedBy);
                
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
                return BadRequest(new ClosingResultDTO
                {
                    Success = false,
                    Message = $"Lỗi khi chốt bảng tăng ca: {ex.Message}"
                });
            }
        }

        [HttpPost("all/{employeeId}/{year}/{month}")]
        public async Task<ActionResult<ClosingResultDTO>> CloseAllSheets(string employeeId, int year, int month)
        {
            try
            {
                var closedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Unknown";
                var result = await _closingService.CloseAllSheetsAsync(employeeId, year, month, closedBy);
                
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
                return BadRequest(new ClosingResultDTO
                {
                    Success = false,
                    Message = $"Lỗi khi chốt tất cả bảng: {ex.Message}"
                });
            }
        }

        [HttpGet("closed/timesheets/{year}/{month}")]
        public async Task<ActionResult<List<TimeSheetDTO>>> GetClosedTimeSheets(int year, int month)
        {
            try
            {
                var timeSheets = await _closingService.GetClosedTimeSheetsAsync(year, month);
                return Ok(timeSheets);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Lỗi khi lấy danh sách bảng công đã chốt: {ex.Message}" });
            }
        }

        [HttpGet("closed/payrolls/{year}/{month}")]
        public async Task<ActionResult<List<PayrollDTO>>> GetClosedPayrolls(int year, int month)
        {
            try
            {
                var payrolls = await _closingService.GetClosedPayrollsAsync(year, month);
                return Ok(payrolls);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Lỗi khi lấy danh sách bảng lương đã chốt: {ex.Message}" });
            }
        }

        [HttpGet("closed/overtimesheets/{year}/{month}")]
        public async Task<ActionResult<List<OvertimeSheetDTO>>> GetClosedOvertimeSheets(int year, int month)
        {
            try
            {
                var overtimeSheets = await _closingService.GetClosedOvertimeSheetsAsync(year, month);
                return Ok(overtimeSheets);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Lỗi khi lấy danh sách bảng tăng ca đã chốt: {ex.Message}" });
            }
        }

        [HttpGet("history")]
        public async Task<ActionResult<object>> GetClosingHistory([FromQuery] int? year = null, [FromQuery] int? month = null)
        {
            try
            {
                // Test endpoint first
                var testResponse = new
                {
                    Message = "Closing history endpoint is working",
                    Year = year ?? DateTime.Now.Year,
                    Month = month ?? DateTime.Now.Month,
                    TimeSheets = new List<object>(),
                    Payrolls = new List<object>(),
                    OvertimeSheets = new List<object>(),
                    TotalClosedTimeSheets = 0,
                    TotalClosedPayrolls = 0,
                    TotalClosedOvertimeSheets = 0
                };

                return Ok(testResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Lỗi khi lấy lịch sử chốt công: {ex.Message}" });
            }
        }
    }
}
