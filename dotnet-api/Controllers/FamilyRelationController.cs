using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.DTOs.PUT;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FamilyRelationController : ControllerBase
    {
        private readonly IFamilyRelationService _familyRelationService;

        public FamilyRelationController(IFamilyRelationService familyRelationService)
        {
            _familyRelationService = familyRelationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var familyRelations = await _familyRelationService.GetAllFamilyRelationsAsync();
            return Ok(familyRelations);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetByEmployeeId(string employeeId)
        {
            var familyRelations = await _familyRelationService.GetFamilyRelationsByEmployeeIdAsync(employeeId);
            return Ok(familyRelations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var familyRelation = await _familyRelationService.GetFamilyRelationByIdAsync(id);
            if (familyRelation == null)
            {
                return NotFound();
            }
            return Ok(familyRelation);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FamilyRelationDTOPOST familyRelationDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _familyRelationService.CreateFamilyRelationAsync(familyRelationDTO);
                return CreatedAtAction(nameof(GetById), new { id = result.ID }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] FamilyRelationDTOPUT familyRelationDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _familyRelationService.UpdateFamilyRelationAsync(familyRelationDTO);
                if (result == null)
                {
                    return NotFound(new { message = "Không tìm thấy quan hệ gia đình với ID đã cho" });
                }
                return Ok(result);
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
                var result = await _familyRelationService.DeleteFamilyRelationAsync(id);
                if (!result)
                {
                    return NotFound(new { message = "Không tìm thấy quan hệ gia đình với ID đã cho" });
                }
                return Ok(new { message = "Xóa quan hệ gia đình thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
