using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.DTOs.PUT;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractController(IContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contracts = await _contractService.GetAllContractsAsync();
            return Ok(contracts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contract = await _contractService.GetContractByIdAsync(id);
            if (contract == null)
            {
                return NotFound(new { message = "Không tìm thấy hợp đồng với ID đã cho" });
            }
            return Ok(contract);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContractDTOPOST contractDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _contractService.CreateContractAsync(contractDTO);
                return CreatedAtAction(nameof(GetById), new { id = result.ID }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ContractDTOPUT contractDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _contractService.UpdateContractAsync(contractDTO);
                if (result == null)
                {
                    return NotFound(new { message = "Không tìm thấy hợp đồng với ID đã cho" });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _contractService.DeleteContractAsync(id);
                if (!result)
                {
                    return NotFound(new { message = "Không tìm thấy hợp đồng với ID đã cho" });
                }
                return Ok(new { message = "Xóa hợp đồng thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("contract-types")]
        public async Task<IActionResult> GetContractTypes()
        {
            var contractTypes = await _contractService.GetContractTypesAsync();
            return Ok(contractTypes);
        }

        [HttpGet("contract-forms")]
        public async Task<IActionResult> GetContractForms()
        {
            var contractForms = await _contractService.GetContractFormsAsync();
            return Ok(contractForms);
        }

        [HttpGet("allowances")]
        public async Task<IActionResult> GetAllowances()
        {
            var allowances = await _contractService.GetAllowancesAsync();
            return Ok(allowances);
        }
    }
}