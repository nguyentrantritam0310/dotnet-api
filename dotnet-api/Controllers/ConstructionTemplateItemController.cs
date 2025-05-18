using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConstructionTemplateItemController : ControllerBase
    {
        private readonly IConstructionTemplateItemService _constructionTemplateItemService;

        public ConstructionTemplateItemController(IConstructionTemplateItemService constructionTemplateItemService)
        {
            _constructionTemplateItemService = constructionTemplateItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var constructionTemplateItem = await _constructionTemplateItemService.GetAllConstructionsTemplateItemAsync();
            return Ok(constructionTemplateItem);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var constructionTemplateItem = await _constructionTemplateItemService.GetConstructionTemplateItemByConstructionTypeIdAsync(id);
            if (constructionTemplateItem == null)
            {
                return NotFound();
            }
            return Ok(constructionTemplateItem);
        }
    }
}
