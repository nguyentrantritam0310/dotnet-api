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
    public class PayrollAdjustmentController : ControllerBase
    {
        private readonly IPayrollAdjustmentService _PayrollAdjustmentService;
        private readonly IMapper _mapper;

        public PayrollAdjustmentController(IPayrollAdjustmentService PayrollAdjustmentService, IMapper mapper)
        {
            _PayrollAdjustmentService = PayrollAdjustmentService;
            _mapper = mapper;
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


    }
}
