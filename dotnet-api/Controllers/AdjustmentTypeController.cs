using dotnet_api.DTOs;
using dotnet_api.Services;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdjustmentTypeController : ControllerBase
    {
        private readonly IAdjustmentTypeService _adjustmentTypeService;

        public AdjustmentTypeController(IAdjustmentTypeService adjustmentTypeService)
        {
            _adjustmentTypeService = adjustmentTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var adjustmentTypes = await _adjustmentTypeService.GetAllAdjustmentTypesAsync();
            return Ok(adjustmentTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var adjustmentType = await _adjustmentTypeService.GetAdjustmentTypeByIdAsync(id);
            if (adjustmentType == null)
                return NotFound(new { message = $"AdjustmentType with ID {id} not found." });

            return Ok(adjustmentType);
        }
    }
}

