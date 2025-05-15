using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.Services;
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

        //[HttpPost]
        //public async Task<IActionResult> Create([FromForm] ConstructionDTOPOST constructionDTO, [FromForm] IFormFile designBlueprint)
        //{
        //    try
        //    {
        //        var createdConstruction = await _constructionService.CreateConstructionAsync(constructionDTO, designBlueprint);
        //        return CreatedAtAction(nameof(GetById), new { id = createdConstruction.ID }, createdConstruction);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, [FromForm] ConstructionDTO constructionDTO, [FromForm] IFormFile designBlueprint)
        //{
        //    if (id != constructionDTO.ID)
        //    {
        //        return BadRequest();
        //    }

        //    var updatedConstruction = await _constructionService.UpdateConstructionAsync(constructionDTO, designBlueprint);
        //    if (updatedConstruction == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(updatedConstruction);
        //}


        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDTO statusDTO)
        {
            var updatedPlan = await _constructionService.UpdateConstructionStatusAsync(id, statusDTO.Status);
            if (updatedPlan == null)
            {
                return NotFound();
            }
            return Ok(updatedPlan);
        }

    }
}
