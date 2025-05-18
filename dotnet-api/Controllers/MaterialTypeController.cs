using dotnet_api.DTOs.POST;
using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using dotnet_api.Data.Entities;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialTypeController : ControllerBase
    {
        private readonly IMaterialTypeService _materialTypeService;

        public MaterialTypeController(IMaterialTypeService MaterialTypeService)
        {
            _materialTypeService =  MaterialTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var MaterialType = await _materialTypeService.GetAllMaterialTypeAsync();
            return Ok(MaterialType);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var MaterialType = await _materialTypeService.GetMaterialTypeByIdAsync(id);
            if (MaterialType == null)
            {
                return NotFound();
            }
            return Ok(MaterialType);
        }

       
    }
}

