using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using dotnet_api.DTOs;
using dotnet_api.Services;
using dotnet_api.Services.Interfaces;
using System.Security.Claims;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FaceRegistrationController : ControllerBase
    {
        private readonly IFaceRegistrationService _faceRegistrationService;
        private readonly ILogger<FaceRegistrationController> _logger;

        public FaceRegistrationController(
            IFaceRegistrationService faceRegistrationService,
            ILogger<FaceRegistrationController> logger)
        {
            _faceRegistrationService = faceRegistrationService;
            _logger = logger;
        }

        [HttpPost("register-embedding")]
        public async Task<ActionResult<FaceRegistrationResultDTO>> RegisterFaceEmbedding([FromBody] FaceEmbeddingRegisterRequestDTO request)
        {
            try
            {
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(currentUserId)) return Unauthorized("Không thể xác định người dùng");
                if (request.EmployeeId != currentUserId) return Forbid("Bạn chỉ có thể đăng ký khuôn mặt của chính mình");

                if (request.Embedding == null || request.Embedding.Length == 0)
                {
                    return BadRequest(new { message = "Thiếu embedding" });
                }

                var result = await _faceRegistrationService.RegisterFaceEmbeddingAsync(request);
                if (result.Success) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RegisterFaceEmbedding endpoint");
                return StatusCode(500, new { message = "Có lỗi xảy ra khi đăng ký embedding" });
            }
        }

        [HttpPost("verify-embedding")]
        public async Task<ActionResult<FaceVerificationResultDTO>> VerifyFaceEmbedding([FromBody] FaceEmbeddingVerifyRequestDTO request)
        {
            try
            {
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(currentUserId)) return Unauthorized("Không thể xác định người dùng");
                if (request.EmployeeId != currentUserId) return Forbid("Bạn chỉ có thể xác minh khuôn mặt của chính mình");

                if (request.Embedding == null || request.Embedding.Length == 0)
                {
                    return BadRequest(new { message = "Thiếu embedding" });
                }

                var result = await _faceRegistrationService.VerifyFaceEmbeddingAsync(request);
                if (result.Success) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in VerifyFaceEmbedding endpoint");
                return StatusCode(500, new { message = "Có lỗi xảy ra khi xác minh embedding" });
            }
        }

        [HttpGet("my-faces")]
        public async Task<ActionResult<List<FaceRegistrationListDTO>>> GetMyFaceRegistrations()
        {
            try
            {
                // Get current user ID from JWT token
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                if (string.IsNullOrEmpty(currentUserId))
                {
                    return Unauthorized("Không thể xác định người dùng");
                }

                var registrations = await _faceRegistrationService.GetUserFaceRegistrationsAsync(currentUserId);
                return Ok(registrations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetMyFaceRegistrations endpoint");
                return StatusCode(500, new { message = "Có lỗi xảy ra khi lấy danh sách khuôn mặt" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFaceRegistration(int id)
        {
            try
            {
                // Get current user ID from JWT token
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                if (string.IsNullOrEmpty(currentUserId))
                {
                    return Unauthorized("Không thể xác định người dùng");
                }

                var success = await _faceRegistrationService.DeleteFaceRegistrationAsync(id, currentUserId);
                
                if (success)
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
                _logger.LogError(ex, "Error in DeleteFaceRegistration endpoint");
                return StatusCode(500, new { message = "Có lỗi xảy ra khi xóa khuôn mặt" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FaceRegistrationDTO>> GetFaceRegistration(int id)
        {
            try
            {
                // Get current user ID from JWT token
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                if (string.IsNullOrEmpty(currentUserId))
                {
                    return Unauthorized("Không thể xác định người dùng");
                }

                var registration = await _faceRegistrationService.GetFaceRegistrationByIdAsync(id);
                
                if (registration == null)
                {
                    return NotFound(new { message = "Không tìm thấy khuôn mặt" });
                }

                // Only allow users to view their own face registrations
                if (registration.EmployeeId != currentUserId)
                {
                    return Forbid("Bạn chỉ có thể xem khuôn mặt của chính mình");
                }

                return Ok(registration);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetFaceRegistration endpoint");
                return StatusCode(500, new { message = "Có lỗi xảy ra khi lấy thông tin khuôn mặt" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFaceRegistration(int id, [FromBody] UpdateFaceRegistrationRequest request)
        {
            try
            {
                // Get current user ID from JWT token
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                if (string.IsNullOrEmpty(currentUserId))
                {
                    return Unauthorized("Không thể xác định người dùng");
                }

                var success = await _faceRegistrationService.UpdateFaceRegistrationAsync(id, request.Notes, currentUserId);
                
                if (success)
                {
                    return Ok(new { message = "Cập nhật khuôn mặt thành công" });
                }
                else
                {
                    return NotFound(new { message = "Không tìm thấy khuôn mặt để cập nhật" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateFaceRegistration endpoint");
                return StatusCode(500, new { message = "Có lỗi xảy ra khi cập nhật khuôn mặt" });
            }
        }
    }

    public class UpdateFaceRegistrationRequest
    {
        public string Notes { get; set; }
    }
}
