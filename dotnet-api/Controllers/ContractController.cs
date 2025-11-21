using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.DTOs.PUT;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using dotnet_api.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _contractService;
        private readonly ApplicationDbContext _context;

        public ContractController(IContractService contractService, ApplicationDbContext context)
        {
            _contractService = contractService;
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
            // Debug logging
            Console.WriteLine($"Received contractDTO: {contractDTO?.ContractNumber}");
            Console.WriteLine($"ApproveStatus: {contractDTO?.ApproveStatus}");
            
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is invalid:");
                foreach (var error in ModelState)
                {
                    Console.WriteLine($"Key: {error.Key}, Errors: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
                }
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _contractService.CreateContractAsync(contractDTO);
                return CreatedAtAction(nameof(GetById), new { id = result.ID }, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in Create: {ex.Message}");
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ContractDTOPUT contractDTO)
        {
            // Debug logging
            Console.WriteLine($"=== UPDATE CONTRACT DEBUG ===");
            Console.WriteLine($"Received contractDTO: {contractDTO?.ContractNumber}");
            Console.WriteLine($"Contract ID: {contractDTO?.ID}");
            Console.WriteLine($"ApproveStatus: {contractDTO?.ApproveStatus}");
            Console.WriteLine($"StartDate: {contractDTO?.StartDate}");
            Console.WriteLine($"EndDate: {contractDTO?.EndDate}");
            Console.WriteLine($"ContractSalary: {contractDTO?.ContractSalary}");
            Console.WriteLine($"InsuranceSalary: {contractDTO?.InsuranceSalary}");
            Console.WriteLine($"Allowances count: {contractDTO?.Allowances?.Count ?? 0}");
            if (contractDTO?.Allowances != null && contractDTO.Allowances.Count > 0)
            {
                foreach (var allowance in contractDTO.Allowances)
                {
                    Console.WriteLine($"Allowance: ContractID={allowance.ContractID}, AllowanceID={allowance.AllowanceID}, Value={allowance.Value}");
                }
            }
            
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is invalid:");
                foreach (var error in ModelState)
                {
                    Console.WriteLine($"Key: {error.Key}, Errors: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
                }
                return BadRequest(ModelState);
            }

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
                Console.WriteLine($"Exception in Update: {ex.Message}");
                Console.WriteLine($"Exception stack trace: {ex.StackTrace}");
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


        [HttpGet("allowances")]
        public async Task<IActionResult> GetAllowances()
        {
            var allowances = await _contractService.GetAllowancesAsync();
            return Ok(allowances);
        }

        // Approval workflow endpoints
        [HttpPut("{id}/submit")]
        public async Task<IActionResult> SubmitContractForApproval(int id, [FromBody] ApprovalActionDTO dto)
        {
            try
            {
                var submitterId = GetCurrentUserId();
                if (string.IsNullOrEmpty(submitterId))
                    return Unauthorized(new { message = "Không thể xác định người dùng" });

                var result = await _contractService.SubmitContractForApprovalAsync(id, submitterId, dto?.Notes);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}/approve")]
        public async Task<IActionResult> ApproveContract(int id, [FromBody] ApprovalActionDTO dto)
        {
            try
            {
                var approverId = GetCurrentUserId();
                if (string.IsNullOrEmpty(approverId))
                    return Unauthorized(new { message = "Không thể xác định người dùng" });

                var result = await _contractService.ApproveContractAsync(id, approverId, dto.Notes);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}/reject")]
        public async Task<IActionResult> RejectContract(int id, [FromBody] ApprovalActionDTO dto)
        {
            try
            {
                var approverId = GetCurrentUserId();
                if (string.IsNullOrEmpty(approverId))
                    return Unauthorized(new { message = "Không thể xác định người dùng" });

                var result = await _contractService.RejectContractAsync(id, approverId, dto.Notes);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}/return")]
        public async Task<IActionResult> ReturnContract(int id, [FromBody] ApprovalActionDTO dto)
        {
            try
            {
                var approverId = GetCurrentUserId();
                if (string.IsNullOrEmpty(approverId))
                    return Unauthorized(new { message = "Không thể xác định người dùng" });

                var result = await _contractService.ReturnContractAsync(id, approverId, dto.Notes);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}