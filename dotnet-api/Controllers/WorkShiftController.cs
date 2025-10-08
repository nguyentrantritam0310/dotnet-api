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
    public class WorkShiftController : ControllerBase
    {
        private readonly IWorkShiftService _workShiftService;
        private readonly IMapper _mapper;

        public WorkShiftController(IWorkShiftService workShiftService, IMapper mapper)
        {
            _workShiftService = workShiftService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var workShift = await _workShiftService.GetAllWorkShiftsAsync();
            return Ok(workShift);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var workShift = await _workShiftService.GetWorkShiftByIdAsync(id);
            if (workShift == null)
            {
                return NotFound();
            }
            return Ok(workShift);
        }
        // POST: api/AttendanceMachine
        [HttpPost]
        public async Task<ActionResult<WorkShiftDTO>> Create([FromBody] WorkShiftDTOPOST dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _workShiftService.CreateWorkShiftAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.ID }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] WorkShiftDTOPUT dto)
        {
            if (id != dto.ID) return BadRequest();
            var updated = await _workShiftService.UpdateWorkShiftAsync(dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _workShiftService.DeleteWorkShiftAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

    }
}
