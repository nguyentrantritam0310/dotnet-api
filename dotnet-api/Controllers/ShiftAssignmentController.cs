using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.DTOs.PUT;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShiftAssignmentController : ControllerBase
    {
        private readonly IShiftAssignmentService _shiftAssignmentService;

        public ShiftAssignmentController(IShiftAssignmentService shiftAssignmentService)
        {
            _shiftAssignmentService = shiftAssignmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var shiftAssignments = await _shiftAssignmentService.GetAllShiftAssignmentsAsync();
            return Ok(shiftAssignments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var shiftAssignment = await _shiftAssignmentService.GetShiftAssignmentByIdAsync(id);
            if (shiftAssignment == null)
            {
                return NotFound(new { message = "Không tìm thấy phân ca với ID đã cho" });
            }
            return Ok(shiftAssignment);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetByEmployeeId(string employeeId)
        {
            var shiftAssignments = await _shiftAssignmentService.GetShiftAssignmentsByEmployeeIdAsync(employeeId);
            return Ok(shiftAssignments);
        }

        [HttpGet("date/{date:datetime}")]
        public async Task<IActionResult> GetByDate(DateTime date)
        {
            var shiftAssignments = await _shiftAssignmentService.GetShiftAssignmentsByDateAsync(date);
            return Ok(shiftAssignments);
        }

        [HttpGet("daterange")]
        public async Task<IActionResult> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var shiftAssignments = await _shiftAssignmentService.GetShiftAssignmentsByDateRangeAsync(startDate, endDate);
            return Ok(shiftAssignments);
        }

        [HttpGet("schedule/week")]
        public async Task<IActionResult> GetScheduleByWeek([FromQuery] DateTime weekStartDate)
        {
            try
            {
                Console.WriteLine($"Received weekStartDate: {weekStartDate}");
                var shiftSchedule = await _shiftAssignmentService.GetShiftScheduleByWeekAsync(weekStartDate);
                Console.WriteLine($"Returning {shiftSchedule?.Count() ?? 0} schedule items");
                return Ok(shiftSchedule);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetScheduleByWeek: {ex.Message}");
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("task/{constructionTaskId}")]
        public async Task<IActionResult> GetByConstructionTaskId(int constructionTaskId)
        {
            var shiftAssignments = await _shiftAssignmentService.GetShiftAssignmentsByConstructionTaskIdAsync(constructionTaskId);
            return Ok(shiftAssignments);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ShiftAssignmentDTOPOST shiftAssignmentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _shiftAssignmentService.CreateShiftAssignmentAsync(shiftAssignmentDTO);
                return CreatedAtAction(nameof(GetById), new { id = result.ID }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ShiftAssignmentDTOPUT shiftAssignmentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _shiftAssignmentService.UpdateShiftAssignmentAsync(shiftAssignmentDTO);
                if (result == null)
                {
                    return NotFound(new { message = "Không tìm thấy phân ca với ID đã cho" });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _shiftAssignmentService.DeleteShiftAssignmentAsync(id);
                if (!result)
                {
                    return NotFound(new { message = "Không tìm thấy phân ca với ID đã cho" });
                }
                return Ok(new { message = "Xóa phân ca thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("assign-task-to-work-shifts")]
        public async Task<IActionResult> AssignTaskToWorkShifts([FromBody] AssignTaskToWorkShiftsRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                var results = await _shiftAssignmentService.AssignTaskToWorkShiftsAsync(
                    request.ConstructionTaskID,
                    request.EmployeeIds,
                    request.WorkShiftIds,
                    request.StartDate,
                    request.EndDate
                );
                
                return Ok(new { 
                    message = $"Đã phân công nhiệm vụ cho {results.Count()} ca làm việc",
                    assignments = results,
                    count = results.Count()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

