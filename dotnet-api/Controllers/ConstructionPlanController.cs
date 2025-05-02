using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConstructionPlanController : ControllerBase
    {
        private readonly IConstructionPlanService _constructionPlanService;

        public ConstructionPlanController(IConstructionPlanService constructionPlanService)
        {
            _constructionPlanService = constructionPlanService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var constructionPlan = await _constructionPlanService.GetAllConstructionsPlanAsync();
            return Ok(constructionPlan);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var constructionPlan = await _constructionPlanService.GetConstructionPlanByIdAsync(id);
            if (constructionPlan == null)
            {
                return NotFound();
            }
            return Ok(constructionPlan);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ConstructionPlanDTO constructionPlanDTO)
        {
            var createdConstructionPlan = await _constructionPlanService.CreateConstructionPlanAsync(constructionPlanDTO);
            return CreatedAtAction(nameof(GetById), new { id = createdConstructionPlan.ID }, createdConstructionPlan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ConstructionPlanDTO constructionPlanDTO)
        {
            if (id != constructionPlanDTO.ID)
            {
                return BadRequest();
            }

            var updatedConstructionPlan = await _constructionPlanService.UpdateConstructionPlanAsync(constructionPlanDTO);
            if (updatedConstructionPlan == null)
            {
                return NotFound();
            }

            return Ok(updatedConstructionPlan);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _constructionPlanService.DeleteConstructionPlanAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
