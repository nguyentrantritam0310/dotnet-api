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
    public class ApplicationUserController : ControllerBase
    {
        private readonly IApplicationUserService _applicationUserService;

        public ApplicationUserController(IApplicationUserService ApplicationUserService)
        {
            _applicationUserService = ApplicationUserService;
        }

        [HttpGet("{roleName}")]
        public async Task<IActionResult> GetByRoleName(string roleName)
        {
            var applicationUser = await _applicationUserService.GetAllApplicationUserByRoleNameAsync(roleName);
            if (applicationUser == null)
            {
                return NotFound();
            }
            return Ok(applicationUser);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var applicationUser = await _applicationUserService.GetAllApplicationUserAsync();
            return Ok(applicationUser);
        }

        // Employee CRUD endpoints
        [HttpPost("employee")]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDTOPOST employeeDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _applicationUserService.CreateEmployeeAsync(employeeDTO);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("employee/{id}")]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            var employee = await _applicationUserService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpGet("employees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _applicationUserService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpPut("employee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDTOPUT employeeDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _applicationUserService.UpdateEmployeeAsync(employeeDTO);
                if (result == null)
                {
                    return NotFound(new { message = "Không tìm thấy nhân viên với ID đã cho" });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("employee/{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(new { message = "ID nhân viên không được để trống" });

            try
            {
                var result = await _applicationUserService.DeleteEmployeeAsync(id);
                if (!result)
                {
                    return NotFound(new { message = "Không tìm thấy nhân viên với ID đã cho" });
                }
                return Ok(new { message = "Xóa nhân viên thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _applicationUserService.GetAllRolesAsync();
            return Ok(roles);
        }
    }
}
