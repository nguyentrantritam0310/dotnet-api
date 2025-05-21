using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkSubTypeVariantController : ControllerBase
    {
        private readonly IWorkSubTypeVariantService _workSubTypeVariantService;

        public WorkSubTypeVariantController(IWorkSubTypeVariantService workSubTypeVariantService)
        {
            _workSubTypeVariantService = workSubTypeVariantService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkSubTypeVariantDTO>>> GetAll()
        {
            var variants = await _workSubTypeVariantService.GetAllWorkSubTypeVariantsAsync();
            return Ok(variants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkSubTypeVariantDTO>> GetById(int id)
        {
            var variant = await _workSubTypeVariantService.GetWorkSubTypeVariantByIdAsync(id);
            if (variant == null)
            {
                return NotFound();
            }
            return Ok(variant);
        }
    }
} 