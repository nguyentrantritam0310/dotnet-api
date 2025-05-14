using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConstructionTaskController : ControllerBase
    {
        private readonly IConstructionTaskService _constructionTaskService;

        public ConstructionTaskController(IConstructionTaskService constructionTaskService)
        {
            _constructionTaskService = constructionTaskService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            var constructionTask = await _constructionTaskService.GetAllConstructionsTaskByPlanAsync(id);
            return Ok(constructionTask);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var constructionTask = await _constructionTaskService.GetConstructionTaskByIdAsync(id);
        //    if (constructionTask == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(constructionTask);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] ConstructionTaskDTOPOST constructionTaskDTOPOST)
        //{
        //    var createdConstructionTask = await _constructionTaskService.CreateConstructionTaskAsync(constructionTaskDTOPOST);
        //    return CreatedAtAction(nameof(GetById), new { id = createdConstructionTask.ID }, createdConstructionTask);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] ConstructionTaskDTOPUT constructionTaskDTO)
        //{
        //    if (id != constructionTaskDTO.ID)
        //    {
        //        return BadRequest("ID in URL does not match ID in request body");
        //    }

        //    var updatedConstructionTask = await _constructionTaskService.UpdateConstructionTaskAsync(constructionTaskDTO);
        //    if (updatedConstructionTask == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(updatedConstructionTask);
        //}

        //[HttpPatch("{id}/status")]
        //public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDTO statusDTO)
        //{
        //    var updatedTask = await _constructionTaskService.UpdateConstructionTaskStatusAsync(id, statusDTO.Status);
        //    if (updatedTask == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(updatedTask);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var result = await _constructionTaskService.DeleteConstructionTaskAsync(id);
        //    if (!result)
        //    {
        //        return NotFound();
        //    }

        //    return NoContent();
        //}
    }
}
