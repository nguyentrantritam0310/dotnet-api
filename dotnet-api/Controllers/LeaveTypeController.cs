using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveTypeController : ControllerBase
    {
        private readonly ILeaveTypeService _leaveTypeService;

        public LeaveTypeController(ILeaveTypeService leaveTypeService)
        {
            _leaveTypeService = leaveTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var leaveTypes = await _leaveTypeService.GetAllLeaveTypesAsync();
            return Ok(leaveTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var leaveType = await _leaveTypeService.GetLeaveTypeByIdAsync(id);
            if (leaveType == null)
                return NotFound(new { message = $"Leave type with ID {id} not found." });

            return Ok(leaveType);
        }
    }
}
