using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
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
        public async Task<IActionResult> Create([FromBody] ConstructionPlanDTOPOST constructionPlanDTOPOST)
        {
            var createdConstructionPlan = await _constructionPlanService.CreateConstructionPlanAsync(constructionPlanDTOPOST);
            return CreatedAtAction(nameof(GetById), new { id = createdConstructionPlan.ID }, createdConstructionPlan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ConstructionPlanDTOPUT constructionPlanDTO)
        {
            if (id != constructionPlanDTO.ID)
            {
                return BadRequest("ID in URL does not match ID in request body");
            }

            var updatedConstructionPlan = await _constructionPlanService.UpdateConstructionPlanAsync(constructionPlanDTO);
            if (updatedConstructionPlan == null)
            {
                return NotFound();
            }

            return Ok(updatedConstructionPlan);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDTO statusDTO)
        {
            var updatedPlan = await _constructionPlanService.UpdateConstructionPlanStatusAsync(id, statusDTO.Status);
            if (updatedPlan == null)
            {
                return NotFound();
            }
            return Ok(updatedPlan);
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
