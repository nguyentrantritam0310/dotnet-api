using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ApprovalHistoryController : ControllerBase
    {
        private readonly IApprovalHistoryService _approvalHistoryService;

        public ApprovalHistoryController(IApprovalHistoryService approvalHistoryService)
        {
            _approvalHistoryService = approvalHistoryService;
        }

        [HttpGet("{requestType}/{requestId}")]
        public async Task<IActionResult> GetHistory(string requestType, string requestId)
        {
            try
            {
                var history = await _approvalHistoryService.GetHistoryAsync(requestType, requestId);
                return Ok(history);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi lấy lịch sử duyệt", error = ex.Message });
            }
        }

        [HttpDelete("{requestType}/{requestId}/undo")]
        public async Task<IActionResult> UndoLatestApproval(string requestType, string requestId)
        {
            try
            {
                var success = await _approvalHistoryService.DeleteLatestHistoryAsync(requestType, requestId);
                if (success)
                    return Ok(new { message = "Đã hoàn tác thành công" });
                else
                    return NotFound(new { message = "Không tìm thấy lịch sử duyệt" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

