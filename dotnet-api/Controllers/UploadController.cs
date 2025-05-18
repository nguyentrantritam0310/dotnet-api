using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public UploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost("image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded");
                }

                // Kiểm tra định dạng file
                if (!file.ContentType.StartsWith("image/"))
                {
                    return BadRequest("Only image files are allowed");
                }

                // Tạo tên file duy nhất
                string uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
                
                // Tạo đường dẫn đến thư mục images
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
                
                // Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Tạo đường dẫn đầy đủ cho file
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Lưu file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Ok(new { filename = uniqueFileName });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 