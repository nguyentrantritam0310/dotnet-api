using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConstructionsController : ControllerBase
    {
        private readonly IConstructionService _constructionService;

        public ConstructionsController(IConstructionService constructionService)
        {
            _constructionService = constructionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var constructions = await _constructionService.GetAllConstructionsAsync();
            return Ok(constructions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var construction = await _constructionService.GetConstructionByIdAsync(id);
            if (construction == null)
            {
                return NotFound();
            }
            return Ok(construction);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ConstructionDTO constructionDTO)
        {
            var createdConstruction = await _constructionService.CreateConstructionAsync(constructionDTO);
            return CreatedAtAction(nameof(GetById), new { id = createdConstruction.ID }, createdConstruction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ConstructionDTO constructionDTO)
        {
            if (id != constructionDTO.ID)
            {
                return BadRequest();
            }

            var updatedConstruction = await _constructionService.UpdateConstructionAsync(constructionDTO);
            if (updatedConstruction == null)
            {
                return NotFound();
            }

            return Ok(updatedConstruction);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _constructionService.DeleteConstructionAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
