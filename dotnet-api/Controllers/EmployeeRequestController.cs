using AutoMapper;
using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.DTOs.PUT;
using dotnet_api.Services;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeRequestController : ControllerBase
    {
        private readonly IEmployeeRequestService _EmployeeRequestService;
        private readonly IMapper _mapper;

        public EmployeeRequestController(IEmployeeRequestService EmployeeRequestService, IMapper mapper)
        {
            _EmployeeRequestService = EmployeeRequestService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var EmployeeRequest = await _EmployeeRequestService.GetAllEmployeeRequestsAsync();
            return Ok(EmployeeRequest);
        }

        // GET: api/EmployeeRequest/{id}
        [HttpGet("{voucherNo}")]
        public async Task<IActionResult> GetById(string voucherNo)
        {
            var machine = await _EmployeeRequestService.GetEmployeeRequestByIdAsync(voucherNo);
            if (machine == null)
                return NotFound(new { message = $"EmployeeRequest with ID {voucherNo} not found." });

            return Ok(machine);
        }

        // POST: api/EmployeeRequest
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _EmployeeRequestService.CreateEmployeeRequestAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.VoucherCode }, created);
        }

        // PUT: api/EmployeeRequest/{id}
        [HttpPut("{voucherNo}")]
        public async Task<IActionResult> Update(string voucherNo, [FromBody] EmployeeRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (voucherNo != dto.VoucherCode)
                return BadRequest(new { message = "ID in URL does not match ID in body." });

            var updated = await _EmployeeRequestService.UpdateEmployeeRequestAsync(dto);
            if (updated == null)
                return NotFound(new { message = $"EmployeeRequest with ID {voucherNo} not found." });

            return Ok(updated);
        }

        // DELETE: api/EmployeeRequest/{id}
        [HttpDelete("{voucherNo}")]
        public async Task<IActionResult> Delete(string voucherNo)
        {
            var deleted = await _EmployeeRequestService.DeleteEmployeeRequestAsync(voucherNo);
            if (deleted == null)
                return NotFound(new { message = $"EmployeeRequest with ID {voucherNo} not found." });

            return Ok(deleted);
        }

        // Leave Request specific endpoints
        [HttpGet("leave")]
        public async Task<IActionResult> GetAllLeaveRequests()
        {
            var leaveRequests = await _EmployeeRequestService.GetAllLeaveRequestsAsync();
            return Ok(leaveRequests);
        }

        [HttpGet("leave/{voucherCode}")]
        public async Task<IActionResult> GetLeaveRequestById(string voucherCode)
        {
            var leaveRequest = await _EmployeeRequestService.GetLeaveRequestByIdAsync(voucherCode);
            if (leaveRequest == null)
                return NotFound(new { message = $"Leave request with voucher code {voucherCode} not found." });

            return Ok(leaveRequest);
        }

        [HttpPost("leave")]
        public async Task<IActionResult> CreateLeaveRequest([FromBody] LeaveRequestDTOPOST dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _EmployeeRequestService.CreateLeaveRequestAsync(dto);
            return CreatedAtAction(nameof(GetLeaveRequestById), new { voucherCode = created.VoucherCode }, created);
        }

        [HttpPut("leave/{voucherCode}")]
        public async Task<IActionResult> UpdateLeaveRequest(string voucherCode, [FromBody] LeaveRequestDTOPUT dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (voucherCode != dto.VoucherCode)
                return BadRequest(new { message = "Voucher code in URL does not match voucher code in body." });

            var updated = await _EmployeeRequestService.UpdateLeaveRequestAsync(dto);
            if (updated == null)
                return NotFound(new { message = $"Leave request with voucher code {voucherCode} not found." });

            return Ok(updated);
        }

        [HttpDelete("leave/{voucherCode}")]
        public async Task<IActionResult> DeleteLeaveRequest(string voucherCode)
        {
            var deleted = await _EmployeeRequestService.DeleteLeaveRequestAsync(voucherCode);
            if (deleted == null)
                return NotFound(new { message = $"Leave request with voucher code {voucherCode} not found." });

            return Ok(deleted);
        }

        // Overtime Request specific endpoints
        [HttpGet("overtime")]
        public async Task<IActionResult> GetAllOvertimeRequests()
        {
            var overtimeRequests = await _EmployeeRequestService.GetAllOvertimeRequestsAsync();
            return Ok(overtimeRequests);
        }

        [HttpGet("overtime/{voucherCode}")]
        public async Task<IActionResult> GetOvertimeRequestById(string voucherCode)
        {
            var overtimeRequest = await _EmployeeRequestService.GetOvertimeRequestByIdAsync(voucherCode);
            if (overtimeRequest == null)
                return NotFound(new { message = $"Overtime request with voucher code {voucherCode} not found." });

            return Ok(overtimeRequest);
        }

        [HttpPost("overtime")]
        public async Task<IActionResult> CreateOvertimeRequest([FromBody] OvertimeRequestDTOPOST dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _EmployeeRequestService.CreateOvertimeRequestAsync(dto);
            return CreatedAtAction(nameof(GetOvertimeRequestById), new { voucherCode = created.VoucherCode }, created);
        }

        [HttpPut("overtime/{voucherCode}")]
        public async Task<IActionResult> UpdateOvertimeRequest(string voucherCode, [FromBody] OvertimeRequestDTOPUT dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (voucherCode != dto.VoucherCode)
                return BadRequest(new { message = "Voucher code in URL does not match voucher code in body." });

            var updated = await _EmployeeRequestService.UpdateOvertimeRequestAsync(dto);
            if (updated == null)
                return NotFound(new { message = $"Overtime request with voucher code {voucherCode} not found." });

            return Ok(updated);
        }

        [HttpDelete("overtime/{voucherCode}")]
        public async Task<IActionResult> DeleteOvertimeRequest(string voucherCode)
        {
            var deleted = await _EmployeeRequestService.DeleteOvertimeRequestAsync(voucherCode);
            if (deleted == null)
                return NotFound(new { message = $"Overtime request with voucher code {voucherCode} not found." });

            return Ok(deleted);
        }
    }
}
