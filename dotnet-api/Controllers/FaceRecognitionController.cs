using Microsoft.AspNetCore.Mvc;
using dotnet_api.Services.Interfaces;
using dotnet_api.DTOs;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FaceRecognitionController : ControllerBase
    {
        private readonly IFaceRecognitionService _faceRecognitionService;

        public FaceRecognitionController(IFaceRecognitionService faceRecognitionService)
        {
            _faceRecognitionService = faceRecognitionService;
        }

        /// <summary>
        /// Đăng ký khuôn mặt cho nhân viên
        /// </summary>
        /// <param name="employeeId">ID nhân viên</param>
        /// <param name="image">Ảnh khuôn mặt</param>
        /// <returns></returns>
        [HttpPost("register/{employeeId}")]
        public async Task<IActionResult> RegisterFace(string employeeId, IFormFile image)
        {
            if (image == null || image.Length == 0)
                return BadRequest(new { message = "Không có ảnh được tải lên" });

            if (!IsValidImageFile(image))
                return BadRequest(new { message = "Định dạng ảnh không hợp lệ. Chỉ chấp nhận JPG, PNG" });

            try
            {
                using var memoryStream = new MemoryStream();
                await image.CopyToAsync(memoryStream);
                var imageBytes = memoryStream.ToArray();

                var result = await _faceRecognitionService.RegisterFaceAsync(employeeId, imageBytes);
                
                if (result.Success)
                {
                    return Ok(new { 
                        message = "Đăng ký khuôn mặt thành công",
                        faceId = result.FaceId,
                        confidence = result.Confidence
                    });
                }
                else
                {
                    return BadRequest(new { message = result.Message });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Lỗi server: {ex.Message}" });
            }
        }

        /// <summary>
        /// Nhận dạng khuôn mặt từ ảnh check-in
        /// </summary>
        /// <param name="image">Ảnh khuôn mặt để nhận dạng</param>
        /// <returns></returns>
        [HttpPost("recognize")]
        public async Task<IActionResult> RecognizeFace(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return BadRequest(new { message = "Không có ảnh được tải lên" });

            if (!IsValidImageFile(image))
                return BadRequest(new { message = "Định dạng ảnh không hợp lệ. Chỉ chấp nhận JPG, PNG" });

            try
            {
                using var memoryStream = new MemoryStream();
                await image.CopyToAsync(memoryStream);
                var imageBytes = memoryStream.ToArray();

                var result = await _faceRecognitionService.RecognizeFaceAsync(imageBytes);
                
                if (result.Success)
                {
                    return Ok(new { 
                        success = true,
                        employeeId = result.EmployeeId,
                        employeeName = result.EmployeeName,
                        confidence = result.Confidence,
                        message = "Nhận dạng thành công"
                    });
                }
                else
                {
                    return Ok(new { 
                        success = false,
                        message = result.Message,
                        confidence = result.Confidence
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Lỗi server: {ex.Message}" });
            }
        }

        /// <summary>
        /// Nhận dạng khuôn mặt từ base64 string (cho mobile app)
        /// </summary>
        /// <param name="request">Request chứa base64 image và userId</param>
        /// <returns></returns>
        [HttpPost("recognize-base64")]
        public async Task<IActionResult> RecognizeFaceBase64([FromBody] FaceRecognitionRequest request)
        {
            if (string.IsNullOrEmpty(request.ImageBase64))
                return BadRequest(new { message = "Không có ảnh base64" });

            try
            {
                // Decode base64 to bytes
                var imageBytes = Convert.FromBase64String(request.ImageBase64);
                
                var result = await _faceRecognitionService.RecognizeFaceAsync(imageBytes);
                
                if (result.Success)
                {
                    return Ok(new { 
                        success = true,
                        employeeId = result.EmployeeId,
                        employeeName = result.EmployeeName,
                        confidence = result.Confidence,
                        message = "Nhận dạng thành công"
                    });
                }
                else
                {
                    return Ok(new { 
                        success = false,
                        message = result.Message,
                        confidence = result.Confidence
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Lỗi server: {ex.Message}" });
            }
        }

        /// <summary>
        /// Xóa khuôn mặt đã đăng ký
        /// </summary>
        /// <param name="employeeId">ID nhân viên</param>
        /// <returns></returns>
        [HttpDelete("unregister/{employeeId}")]
        public async Task<IActionResult> UnregisterFace(string employeeId)
        {
            try
            {
                var result = await _faceRecognitionService.UnregisterFaceAsync(employeeId);
                
                if (result)
                {
                    return Ok(new { message = "Xóa khuôn mặt thành công" });
                }
                else
                {
                    return NotFound(new { message = "Không tìm thấy khuôn mặt để xóa" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Lỗi server: {ex.Message}" });
            }
        }

        /// <summary>
        /// Lấy danh sách nhân viên đã đăng ký khuôn mặt
        /// </summary>
        /// <returns></returns>
        [HttpGet("registered-employees")]
        public async Task<IActionResult> GetRegisteredEmployees()
        {
            try
            {
                var employees = await _faceRecognitionService.GetRegisteredEmployeesAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Lỗi server: {ex.Message}" });
            }
        }

        private bool IsValidImageFile(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return allowedExtensions.Contains(fileExtension);
        }
    }

    public class FaceRecognitionRequest
    {
        public string ImageBase64 { get; set; }
        public string UserId { get; set; }
    }
}
