using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialNormController : ControllerBase
    {
        private readonly IMaterialNormService _MaterialNormService;

        public MaterialNormController(IMaterialNormService MaterialNormService)
        {
            _MaterialNormService = MaterialNormService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            var MaterialNorm = await _MaterialNormService.GetAllMaterialNormByConstructionAsync(id);
            return Ok(MaterialNorm);
        }

        [HttpGet("item/{id}")]
        public async Task<IActionResult> GetAllByItem(int id)
        {
            var MaterialNorm = await _MaterialNormService.GetAllMaterialNormByConstructionItemAsync(id);
            return Ok(MaterialNorm);
        }
    }
}
