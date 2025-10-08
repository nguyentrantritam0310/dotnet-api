using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OvertimeTypeController : ControllerBase
    {
        private readonly IOvertimeTypeService _overtimeTypeService;

        public OvertimeTypeController(IOvertimeTypeService overtimeTypeService)
        {
            _overtimeTypeService = overtimeTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var overtimeTypes = await _overtimeTypeService.GetAllOvertimeTypesAsync();
            return Ok(overtimeTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var overtimeType = await _overtimeTypeService.GetOvertimeTypeByIdAsync(id);
            if (overtimeType == null)
            {
                return NotFound(new { message = $"OvertimeType with ID {id} not found." });
            }
            return Ok(overtimeType);
        }
    }
}




