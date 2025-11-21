using AutoMapper;
using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.DTOs.PUT;
using dotnet_api.Services;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using dotnet_api.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PayrollAdjustmentController : ControllerBase
    {
        private readonly IPayrollAdjustmentService _PayrollAdjustmentService;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public PayrollAdjustmentController(IPayrollAdjustmentService PayrollAdjustmentService, IMapper mapper, ApplicationDbContext context)
        {
            _PayrollAdjustmentService = PayrollAdjustmentService;
            _mapper = mapper;
            _context = context;
        }

        private string GetCurrentUserId()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email)) return null;
            var user = _context.ApplicationUsers.FirstOrDefault(u => u.Email == email);
            return user?.Id;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var PayrollAdjustment = await _PayrollAdjustmentService.GetAllPayrollAdjustmentsAsync();
            return Ok(PayrollAdjustment);
        }

        // GET: api/PayrollAdjustment/{id}
        [HttpGet("{voucherNo}")]
        public async Task<IActionResult> GetById(string voucherNo)
        {
            var machine = await _PayrollAdjustmentService.GetPayrollAdjustmentByIdAsync(voucherNo);
            if (machine == null)
                return NotFound(new { message = $"PayrollAdjustment with ID {voucherNo} not found." });

            return Ok(machine);
        }

        // POST: api/PayrollAdjustment
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PayrollAdjustmentDTOPOST dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var mappedDto = _mapper.Map<PayrollAdjustmentDTO>(dto);
            var created = await _PayrollAdjustmentService.CreatePayrollAdjustmentAsync(mappedDto);
            return CreatedAtAction(nameof(GetById), new { voucherNo = created.voucherNo }, created);
        }

        // PUT: api/PayrollAdjustment/{id}
        [HttpPut("{voucherNo}")]
        public async Task<IActionResult> Update(string voucherNo, [FromBody] PayrollAdjustmentDTOPUT dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (voucherNo != dto.voucherNo)
                return BadRequest(new { message = "ID in URL does not match ID in body." });

            var mappedDto = _mapper.Map<PayrollAdjustmentDTO>(dto);
            var updated = await _PayrollAdjustmentService.UpdatePayrollAdjustmentAsync(mappedDto);
            if (updated == null)
                return NotFound(new { message = $"PayrollAdjustment with ID {voucherNo} not found." });

            return Ok(updated);
        }

        // DELETE: api/PayrollAdjustment/{id}
        [HttpDelete("{voucherNo}")]
        public async Task<IActionResult> Delete(string voucherNo)
        {
            var deleted = await _PayrollAdjustmentService.DeletePayrollAdjustmentAsync(voucherNo);
            if (deleted == null)
                return NotFound(new { message = $"PayrollAdjustment with ID {voucherNo} not found." });

            return Ok(deleted);
        }

        // Approval workflow endpoints
        [HttpPut("{voucherNo}/submit")]
        public async Task<IActionResult> SubmitPayrollAdjustmentForApproval(string voucherNo, [FromBody] ApprovalActionDTO dto)
        {
            try
            {
                var submitterId = GetCurrentUserId();
                if (string.IsNullOrEmpty(submitterId))
                    return Unauthorized(new { message = "Không thể xác định người dùng" });

                var result = await _PayrollAdjustmentService.SubmitPayrollAdjustmentForApprovalAsync(voucherNo, submitterId, dto?.Notes);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{voucherNo}/approve")]
        public async Task<IActionResult> ApprovePayrollAdjustment(string voucherNo, [FromBody] ApprovalActionDTO dto)
        {
            try
            {
                var approverId = GetCurrentUserId();
                if (string.IsNullOrEmpty(approverId))
                    return Unauthorized(new { message = "Không thể xác định người dùng" });

                var result = await _PayrollAdjustmentService.ApprovePayrollAdjustmentAsync(voucherNo, approverId, dto.Notes);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{voucherNo}/reject")]
        public async Task<IActionResult> RejectPayrollAdjustment(string voucherNo, [FromBody] ApprovalActionDTO dto)
        {
            try
            {
                var approverId = GetCurrentUserId();
                if (string.IsNullOrEmpty(approverId))
                    return Unauthorized(new { message = "Không thể xác định người dùng" });

                var result = await _PayrollAdjustmentService.RejectPayrollAdjustmentAsync(voucherNo, approverId, dto.Notes);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{voucherNo}/return")]
        public async Task<IActionResult> ReturnPayrollAdjustment(string voucherNo, [FromBody] ApprovalActionDTO dto)
        {
            try
            {
                var approverId = GetCurrentUserId();
                if (string.IsNullOrEmpty(approverId))
                    return Unauthorized(new { message = "Không thể xác định người dùng" });

                var result = await _PayrollAdjustmentService.ReturnPayrollAdjustmentAsync(voucherNo, approverId, dto.Notes);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
