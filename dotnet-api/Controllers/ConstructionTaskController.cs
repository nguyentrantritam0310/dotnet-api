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


        [HttpGet("item/{id}")]
        public async Task<IActionResult> GetAllbyItem(int id)
        {
            var constructionTask = await _constructionTaskService.GetAllConstructionsTaskByItemAsync(id);
            return Ok(constructionTask);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var constructionTask = await _constructionTaskService.GetConstructionTaskByIdAsync(id);
            if (constructionTask == null)
            {
                return NotFound();
            }
            return Ok(constructionTask);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ConstructionTaskDTOPOST constructionTaskDTOPOST)
        {
            var createdConstructionTask = await _constructionTaskService.CreateConstructionTaskAsync(constructionTaskDTOPOST);
            return CreatedAtAction(nameof(GetById), new { id = createdConstructionTask.ID }, createdConstructionTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ConstructionTaskDTOPOST constructionTaskDTO)
        {
            if (id != constructionTaskDTO.ID)
            {
                return BadRequest("ID in URL does not match ID in request body");
            }

            var updatedConstructionTask = await _constructionTaskService.UpdateConstructionTaskAsync(constructionTaskDTO);
            if (updatedConstructionTask == null)
            {
                return NotFound();
            }

            return Ok(updatedConstructionTask);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDTO statusDTO)
        {
            var updatedTask = await _constructionTaskService.UpdateConstructionTaskStatusAsync(id, statusDTO.Status);
            if (updatedTask == null)
            {
                return NotFound();
            }
            return Ok(updatedTask);
        }

        [HttpPatch("{id}/actual")]
        public async Task<IActionResult> UpdateActual(int id, [FromBody] ConstructionTaskDTOPUT taskDTO)
        {
            var updatedTask = await _constructionTaskService.UpdateConstructionTaskActualAsync(id, taskDTO.ActualWorkload);
            if (updatedTask == null)
            {
                return NotFound();
            }
            return Ok(updatedTask);
        }

    }
}
