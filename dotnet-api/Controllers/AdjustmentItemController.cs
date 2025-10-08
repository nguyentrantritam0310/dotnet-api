using dotnet_api.DTOs;
using dotnet_api.Services;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdjustmentItemController : ControllerBase
    {
        private readonly IAdjustmentItemService _adjustmentItemService;

        public AdjustmentItemController(IAdjustmentItemService adjustmentItemService)
        {
            _adjustmentItemService = adjustmentItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var adjustmentItems = await _adjustmentItemService.GetAllAdjustmentItemsAsync();
            return Ok(adjustmentItems);
        }

        [HttpGet("by-type/{adjustmentTypeId}")]
        public async Task<IActionResult> GetByTypeId(int adjustmentTypeId)
        {
            var adjustmentItems = await _adjustmentItemService.GetAdjustmentItemsByTypeIdAsync(adjustmentTypeId);
            return Ok(adjustmentItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var adjustmentItem = await _adjustmentItemService.GetAdjustmentItemByIdAsync(id);
            if (adjustmentItem == null)
                return NotFound(new { message = $"AdjustmentItem with ID {id} not found." });

            return Ok(adjustmentItem);
        }
    }
}






