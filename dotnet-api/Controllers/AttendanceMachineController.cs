using AutoMapper;
using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.Services;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceMachineController : ControllerBase
    {
        private readonly IAttendanceMachineService _attendanceMachineService;
        private readonly IMapper _mapper;

        public AttendanceMachineController(IAttendanceMachineService attendanceMachineService, IMapper mapper)
        {
            _attendanceMachineService = attendanceMachineService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var AttendanceMachine = await _attendanceMachineService.GetAllAttendanceMachinesAsync();
            return Ok(AttendanceMachine);
        }

        // GET: api/AttendanceMachine/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var machine = await _attendanceMachineService.GetAttendanceMachineByIdAsync(id);
            if (machine == null)
                return NotFound(new { message = $"AttendanceMachine with ID {id} not found." });

            return Ok(machine);
        }

        // POST: api/AttendanceMachine
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AttendanceMachineDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _attendanceMachineService.CreateAttendanceMachineAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.ID }, created);
        }

        // PUT: api/AttendanceMachine/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AttendanceMachineDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.ID)
                return BadRequest(new { message = "ID in URL does not match ID in body." });

            var updated = await _attendanceMachineService.UpdateAttendanceMachineAsync(dto);
            if (updated == null)
                return NotFound(new { message = $"AttendanceMachine with ID {id} not found." });

            return Ok(updated);
        }

        // DELETE: api/AttendanceMachine/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _attendanceMachineService.DeleteAttendanceMachineAsync(id);
            if (deleted == null)
                return NotFound(new { message = $"AttendanceMachine with ID {id} not found." });

            return Ok(deleted);
        }


    }
}
