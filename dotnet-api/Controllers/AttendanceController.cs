//using dotnet_api.DTOs;
//using dotnet_api.Services.Interfaces;
//using Microsoft.AspNetCore.Mvc;

//namespace dotnet_api.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class AttendanceController : ControllerBase
//    {
//        private readonly IAttendanceService _AttendanceService;

//        public AttendanceController(IAttendanceService AttendanceService)
//        {
//            _AttendanceService = AttendanceService;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var Attendances = await _AttendanceService.GetAllAttendanceAsync();
//            return Ok(Attendances);
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var Attendance = await _AttendanceService.GetAttendanceByIdAsync(id);
//            if (Attendance == null)
//            {
//                return NotFound();
//            }
//            return Ok(Attendance);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create([FromBody] AttendanceDTO AttendanceDTO)
//        {
//            var createdAttendance = await _AttendanceService.CreateAttendanceAsync(AttendanceDTO);
//            return CreatedAtAction(nameof(GetById), new { id = createdAttendance.ConstructionTaskID }, createdAttendance);
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var result = await _AttendanceService.DeleteAttendanceAsync(id);
//            if (!result)
//            {
//                return NotFound();
//            }

//            return NoContent();
//        }

//        [HttpDelete("employee/{employeeId}/task/{taskId}")]
//        public async Task<IActionResult> DeleteByEmployeeAndTask(string employeeId, int taskId)
//        {
//            var result = await _AttendanceService.DeleteAttendanceByEmployeeAndTaskAsync(employeeId, taskId);
//            if (!result)
//            {
//                return NotFound(new { message = "Không tìm thấy bản ghi chấm công cho nhân viên và nhiệm vụ này" });
//            }

//            return NoContent();
//        }

//        [HttpPut("employee/status")]
//        public async Task<IActionResult> UpdateStatusByEmployee([FromBody] UpdateAttendanceStatusDTO dto)
//        {
//            var result = await _AttendanceService.UpdateAttendanceStatusByEmployeeAsync(dto);
//            if (!result)
//            {
//                return NotFound(new { message = "Không tìm thấy bản ghi chấm công cho nhân viên này trong ngày đã chọn" });
//            }

//            return NoContent();
//        }
//    }
//}
