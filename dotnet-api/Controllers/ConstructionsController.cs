using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConstructionsController : ControllerBase
    {
        private readonly IConstructionService _constructionService;
        private readonly IMapper _mapper;

        public ConstructionsController(IConstructionService constructionService, IMapper mapper)
        {
            _constructionService = constructionService;
            _mapper = mapper;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ConstructionCreateDTO constructionDTO)
        {
            try
            {
                // Log incoming request data
                Console.WriteLine($"Received construction data: {System.Text.Json.JsonSerializer.Serialize(constructionDTO)}");

                // Validate input
                if (constructionDTO == null)
                {
                    return BadRequest(new { message = "Dữ liệu công trình không hợp lệ" });
                }

                // Validate dates
                if (constructionDTO.StartDate >= constructionDTO.ExpectedCompletionDate)
                {
                    return BadRequest(new { message = "Ngày kết thúc phải sau ngày bắt đầu" });
                }

                // Validate required fields
                if (string.IsNullOrWhiteSpace(constructionDTO.ConstructionName))
                {
                    return BadRequest(new { message = "Tên công trình không được để trống" });
                }

                if (string.IsNullOrWhiteSpace(constructionDTO.Location))
                {
                    return BadRequest(new { message = "Địa điểm không được để trống" });
                }

                if (constructionDTO.TotalArea <= 0)
                {
                    return BadRequest(new { message = "Diện tích phải lớn hơn 0" });
                }

                // Map and create construction
                try
                {
                    var createdConstruction = await _constructionService.CreateConstructionAsync(constructionDTO);
                    Console.WriteLine($"Created construction: {System.Text.Json.JsonSerializer.Serialize(createdConstruction)}");
                    
                    return CreatedAtAction(nameof(GetById), new { id = createdConstruction.ID }, createdConstruction);
                }
                catch (AutoMapper.AutoMapperMappingException ex)
                {
                    Console.WriteLine($"Mapping error: {ex.Message}");
                    return BadRequest(new { message = "Lỗi mapping dữ liệu. Vui lòng kiểm tra lại định dạng dữ liệu." });
                }
            }
            catch (Exception ex)
            {
                // Log the full exception details
                Console.WriteLine($"Error creating construction: {ex}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException}");
                    Console.WriteLine($"Inner exception message: {ex.InnerException.Message}");
                    Console.WriteLine($"Inner exception stack trace: {ex.InnerException.StackTrace}");
                }
                return BadRequest(new { message = $"Có lỗi xảy ra khi tạo công trình: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ConstructionUpdateDTO constructionDTO)
        {
            if (id != constructionDTO.ID)
            {
                return BadRequest(new { message = "ID không khớp" });
            }

            try
            {
                // Validate input
                if (constructionDTO == null)
                {
                    return BadRequest(new { message = "Dữ liệu công trình không hợp lệ" });
                }

                // Validate dates
                if (constructionDTO.StartDate >= constructionDTO.ExpectedCompletionDate)
                {
                    return BadRequest(new { message = "Ngày kết thúc phải sau ngày bắt đầu" });
                }

                // Validate required fields
                if (string.IsNullOrWhiteSpace(constructionDTO.ConstructionName))
                {
                    return BadRequest(new { message = "Tên công trình không được để trống" });
                }

                if (string.IsNullOrWhiteSpace(constructionDTO.Location))
                {
                    return BadRequest(new { message = "Địa điểm không được để trống" });
                }

                if (constructionDTO.TotalArea <= 0)
                {
                    return BadRequest(new { message = "Diện tích phải lớn hơn 0" });
                }

                var updatedConstruction = await _constructionService.UpdateConstructionAsync(constructionDTO);
                if (updatedConstruction == null)
                {
                    return NotFound(new { message = "Không tìm thấy công trình" });
                }
                return Ok(updatedConstruction);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDTO statusDTO)
        {
            var updatedConstruction = await _constructionService.UpdateConstructionStatusAsync(id, statusDTO.Status);
            if (updatedConstruction == null)
            {
                return NotFound();
            }
            return Ok(updatedConstruction);
        }
    }
}
