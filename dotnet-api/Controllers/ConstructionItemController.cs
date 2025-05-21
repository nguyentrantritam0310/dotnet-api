using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConstructionItemController : ControllerBase
    {
        private readonly IConstructionItemService _constructionItemService;

        public ConstructionItemController(IConstructionItemService constructionItemService)
        {
            _constructionItemService = constructionItemService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConstructionItemDTO>> GetById(int id)
        {
            try
            {
                var constructionItem = await _constructionItemService.GetConstructionItemByIdAsync(id);
                if (constructionItem == null)
                {
                    return NotFound();
                }
                return Ok(constructionItem);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("construction/{constructionId}")]
        public async Task<ActionResult<IEnumerable<ConstructionItemDTO>>> GetByConstructionId(int constructionId)
        {
            try
            {
                var items = await _constructionItemService.GetConstructionItemsByConstructionIdAsync(constructionId);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ConstructionItemDTO>> Create([FromBody] ConstructionItemCreateDTO createDTO)
        {
            try
            {
                var createdItem = await _constructionItemService.CreateConstructionItemAsync(createDTO);
                return CreatedAtAction(nameof(GetById), new { id = createdItem.ID }, createdItem);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ConstructionItemDTO>> Update([FromBody] ConstructionItemUpdateDTO updateDTO)
        {
            try
            {
                var updatedItem = await _constructionItemService.UpdateConstructionItemAsync(updateDTO);
                if (updatedItem == null)
                {
                    return NotFound();
                }
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult<ConstructionItemDTO>> UpdateStatus(int id, [FromBody] UpdateConstructionItemStatusDTO statusDTO)
        {
            try
            {
                var updatedItem = await _constructionItemService.UpdateConstructionItemStatusAsync(id, statusDTO.Status);
                if (updatedItem == null)
                {
                    return NotFound();
                }
                return Ok(updatedItem);
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
                await _constructionItemService.DeleteConstructionItemAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
