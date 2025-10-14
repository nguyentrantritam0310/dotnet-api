using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.DTOs.PUT;
using dotnet_api.Data.Enums;

namespace dotnet_api.Services.Interfaces
{
    public interface IApplicationUserService
    {
        Task<IEnumerable<ApplicationUserDTO>> GetAllApplicationUserByRoleNameAsync(string roleName);
        Task<IEnumerable<ApplicationUserDTO>> GetAllApplicationUserAsync();
        
        // Employee CRUD operations
        Task<EmployeeDTO> CreateEmployeeAsync(EmployeeDTOPOST employeeDTO);
        Task<EmployeeDTO> GetEmployeeByIdAsync(string id);
        Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync();
        Task<EmployeeDTO> UpdateEmployeeAsync(EmployeeDTOPUT employeeDTO);
        Task<bool> DeleteEmployeeAsync(string id);
        Task<IEnumerable<RoleDTO>> GetAllRolesAsync();
        Task<EmployeeDTO> UpdateEmployeeStatusAsync(string employeeId, EmployeeStatusEnum status);
    }
}
