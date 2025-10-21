using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using dotnet_api.Services;
using System.Threading.Tasks;

namespace dotnet_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TimeSheetFeedbackController : ControllerBase
    {
        private readonly ITimeSheetFeedbackService _timeSheetFeedbackService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TimeSheetFeedbackController(
            ITimeSheetFeedbackService timeSheetFeedbackService,
            UserManager<ApplicationUser> userManager)
        {
            _timeSheetFeedbackService = timeSheetFeedbackService;
            _userManager = userManager;
        }

        /// <summary>
        /// Lấy danh sách phản ánh bảng công của nhân viên hiện tại
        /// </summary>
        [HttpGet("my-feedbacks")]
        public async Task<IActionResult> GetMyTimeSheetFeedbacks()
        {
            var userId = _userManager.GetUserId(User);
            var feedbacks = await _timeSheetFeedbackService.GetTimeSheetFeedbacksByEmployeeAsync(userId);
            return Ok(feedbacks);
        }

        /// <summary>
        /// Lấy phản ánh bảng công theo ID
        /// </summary>
        [HttpGet("{timeSheetId}")]
        public async Task<IActionResult> GetTimeSheetFeedback(int timeSheetId)
        {
            var userId = _userManager.GetUserId(User);
            var feedback = await _timeSheetFeedbackService.GetTimeSheetFeedbackAsync(timeSheetId, userId);
            
            if (feedback == null)
                return NotFound("Không tìm thấy phản ánh.");

            return Ok(feedback);
        }

        /// <summary>
        /// Tạo phản ánh bảng công mới
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateTimeSheetFeedback([FromBody] CreateTimeSheetFeedbackDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var userId = _userManager.GetUserId(User);
                var feedback = await _timeSheetFeedbackService.CreateTimeSheetFeedbackAsync(createDto, userId);
                return CreatedAtAction(nameof(GetTimeSheetFeedback), new { timeSheetId = feedback.TimeSheetID }, feedback);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật phản ánh bảng công
        /// </summary>
        [HttpPut("{timeSheetId}")]
        public async Task<IActionResult> UpdateTimeSheetFeedback(int timeSheetId, [FromBody] UpdateTimeSheetFeedbackDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var userId = _userManager.GetUserId(User);
                var feedback = await _timeSheetFeedbackService.UpdateTimeSheetFeedbackAsync(updateDto, userId);
                return Ok(feedback);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Xóa phản ánh bảng công
        /// </summary>
        [HttpDelete("{timeSheetId}")]
        public async Task<IActionResult> DeleteTimeSheetFeedback(int timeSheetId)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var result = await _timeSheetFeedbackService.DeleteTimeSheetFeedbackAsync(timeSheetId, userId);
                if (!result)
                    return NotFound("Không tìm thấy phản ánh.");
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}