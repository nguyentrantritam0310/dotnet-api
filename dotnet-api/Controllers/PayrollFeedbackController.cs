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
    public class PayrollFeedbackController : ControllerBase
    {
        private readonly IPayrollFeedbackService _payrollFeedbackService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PayrollFeedbackController(
            IPayrollFeedbackService payrollFeedbackService,
            UserManager<ApplicationUser> userManager)
        {
            _payrollFeedbackService = payrollFeedbackService;
            _userManager = userManager;
        }

        /// <summary>
        /// Lấy danh sách phản ánh bảng lương của nhân viên hiện tại
        /// </summary>
        [HttpGet("my-feedbacks")]
        public async Task<IActionResult> GetMyPayrollFeedbacks()
        {
            var userId = _userManager.GetUserId(User);
            var feedbacks = await _payrollFeedbackService.GetPayrollFeedbacksByEmployeeAsync(userId);
            return Ok(feedbacks);
        }

        /// <summary>
        /// Lấy phản ánh bảng lương theo ID
        /// </summary>
        [HttpGet("{payrollId}")]
        public async Task<IActionResult> GetPayrollFeedback(int payrollId)
        {
            var userId = _userManager.GetUserId(User);
            var feedback = await _payrollFeedbackService.GetPayrollFeedbackAsync(payrollId, userId);
            
            if (feedback == null)
                return NotFound("Không tìm thấy phản ánh.");

            return Ok(feedback);
        }

        /// <summary>
        /// Tạo phản ánh bảng lương mới
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreatePayrollFeedback([FromBody] CreatePayrollFeedbackDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var userId = _userManager.GetUserId(User);
                var feedback = await _payrollFeedbackService.CreatePayrollFeedbackAsync(createDto, userId);
                return CreatedAtAction(nameof(GetPayrollFeedback), new { payrollId = feedback.PayrollID }, feedback);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật phản ánh bảng lương
        /// </summary>
        [HttpPut("{payrollId}")]
        public async Task<IActionResult> UpdatePayrollFeedback(int payrollId, [FromBody] UpdatePayrollFeedbackDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var userId = _userManager.GetUserId(User);
                var feedback = await _payrollFeedbackService.UpdatePayrollFeedbackAsync(updateDto, userId);
                return Ok(feedback);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Xóa phản ánh bảng lương
        /// </summary>
        [HttpDelete("{payrollId}")]
        public async Task<IActionResult> DeletePayrollFeedback(int payrollId)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var result = await _payrollFeedbackService.DeletePayrollFeedbackAsync(payrollId, userId);
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