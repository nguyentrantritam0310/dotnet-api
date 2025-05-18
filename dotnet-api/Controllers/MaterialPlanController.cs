using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialPlanController : ControllerBase
    {
        private readonly IMaterialPlanService _MaterialPlanService;

        public MaterialPlanController(IMaterialPlanService MaterialPlanService)
        {
            _MaterialPlanService = MaterialPlanService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var MaterialPlans = await _MaterialPlanService.GetAllMaterialPlanAsync();
        //    return Ok(MaterialPlans);
        //}

        [HttpGet("{importOrderId}")]
        public async Task<IActionResult> GetById(int importOrderId)
        {
            var MaterialPlan = await _MaterialPlanService.GetMaterialPlanByIdAsync(importOrderId);
            if (MaterialPlan == null)
            {
                return NotFound();
            }
            return Ok(MaterialPlan);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MaterialPlanDTOPOST MaterialPlanDTO)
        {
            var createdMaterialPlan = await _MaterialPlanService.CreateMaterialPlanAsync(MaterialPlanDTO);
            return CreatedAtAction(nameof(GetById), new {
                importOrderId = createdMaterialPlan.ImportOrderID
            }, createdMaterialPlan);
        }
        [HttpPost("update-quantity-note")]
        public async Task<IActionResult> UpdateQuantityAndNote([FromBody] MaterialPlanDTOPOST dto)
        {
            var updated = await _MaterialPlanService.UpdateMaterialPlanQuantityAndNoteAsync(dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] MaterialPlanDTO MaterialPlanDTO)
        //{
        //    if (id != MaterialPlanDTO.ID)
        //    {
        //        return BadRequest();
        //    }

        //    var updatedMaterialPlan = await _MaterialPlanService.UpdateMaterialPlanAsync(MaterialPlanDTO);
        //    if (updatedMaterialPlan == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(updatedMaterialPlan);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var result = await _MaterialPlanService.DeleteMaterialPlanAsync(id);
        //    if (!result)
        //    {
        //        return NotFound();
        //    }

        //    return NoContent();
        //}
    }
}
