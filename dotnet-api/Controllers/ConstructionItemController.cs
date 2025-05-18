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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var constructionItem = await _constructionItemService.GetAllConstructionsItemAsync();
            return Ok(constructionItem);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var constructionItem = await _constructionItemService.GetConstructionItemByIdAsync(id);
            if (constructionItem == null)
            {
                return NotFound();
            }
            return Ok(constructionItem);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ConstructionItemDTO constructionItemDTO)
        {
            var createdConstructionItem = await _constructionItemService.CreateConstructionItemAsync(constructionItemDTO);
            return CreatedAtAction(nameof(GetById), new { id = createdConstructionItem.ID }, createdConstructionItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ConstructionItemDTO constructionItemDTO)
        {
            if (id != constructionItemDTO.ID)
            {
                return BadRequest();
            }

            var updatedConstructionItem = await _constructionItemService.UpdateConstructionItemAsync(constructionItemDTO);
            if (updatedConstructionItem == null)
            {
                return NotFound();
            }

            return Ok(updatedConstructionItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _constructionItemService.DeleteConstructionItemAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
