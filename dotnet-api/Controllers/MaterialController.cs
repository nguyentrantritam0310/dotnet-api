using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _MaterialService;

        public MaterialController(IMaterialService MaterialService)
        {
            _MaterialService = MaterialService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Materials = await _MaterialService.GetAllMaterialAsync();
            return Ok(Materials);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Material = await _MaterialService.GetMaterialByIdAsync(id);
            if (Material == null)
            {
                return NotFound();
            }
            return Ok(Material);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MaterialDTOPOST MaterialDTO)
        {
            var createdMaterial = await _MaterialService.CreateMaterialAsync(MaterialDTO);
            return CreatedAtAction(nameof(GetById), new { id = createdMaterial.ID }, createdMaterial);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MaterialDTOPOST materialDTO)
        {
            if (id != materialDTO.ID)
            {
                return BadRequest();
            }

            var updatedMaterial = await _MaterialService.UpdateMaterialAsync(materialDTO);
            if (updatedMaterial == null)
            {
                return NotFound();
            }

            return Ok(updatedMaterial);
        }

        [HttpPut("update-stock/{id}")]
        public async Task<IActionResult> UpdateStockQuantity(int id, [FromBody] MaterialUpdateStockQuantityDTO MaterialDTO)
        {
            if (id != MaterialDTO.ID)
            {
                return BadRequest();
            }

            var updatedMaterial = await _MaterialService.UpdateStockQuantityMaterialAsync(MaterialDTO);
            if (updatedMaterial == null)
            {
                return NotFound();
            }

            return Ok(updatedMaterial);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _MaterialService.DeleteMaterialAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
