using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.Data.Enums;
using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.DTOs.PUT;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;

        public ApplicationUserService(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager, IEmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<ConstructionItemDTO> GetConstructionItemByIdAsync(int id)
        {
            var constructionItem = await _context.ConstructionItems
                .Include(c => c.Construction)
                .Include(c => c.ConstructionPlans)

                .Include(c => c.ConstructionStatus)
                .FirstOrDefaultAsync(c => c.ID == id);

            return constructionItem == null ? null : _mapper.Map<ConstructionItemDTO>(constructionItem);
        }


        public async Task<IEnumerable<ApplicationUserDTO>> GetAllApplicationUserByRoleNameAsync(string roleName)
        {
            var applicationUsers = await _userManager.GetUsersInRoleAsync(roleName);

            var userDTOs = _mapper.Map<IEnumerable<ApplicationUserDTO>>(applicationUsers);
            foreach (var userDTO in userDTOs)
            {
                userDTO.RoleName = roleName;
            }
            return userDTOs;
        }
        public async Task<IEnumerable<ApplicationUserDTO>> GetAllApplicationUserAsync()
        {
            var userDTOs = await _context.ApplicationUsers
                .Include(u => u.Role)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ApplicationUserDTO>>(userDTOs);
        }

        // Employee CRUD operations
        public async Task<EmployeeDTO> CreateEmployeeAsync(EmployeeDTOPOST employeeDTO)
        {
            // Check if email already exists
            var existingUser = await _userManager.FindByEmailAsync(employeeDTO.Email);
            if (existingUser != null)
            {
                throw new Exception("Email đã được sử dụng bởi tài khoản khác");
            }

            // Check if ID already exists
            var existingId = await _userManager.FindByIdAsync(employeeDTO.Id);
            if (existingId != null)
            {
                throw new Exception("ID nhân viên đã được sử dụng bởi tài khoản khác");
            }

            // Check if role exists
            var role = await _context.Roles.FindAsync(employeeDTO.RoleID);
            if (role == null)
            {
                throw new Exception("Chức danh không tồn tại");
            }

            // Generate random secure password
            var temporaryPassword = PasswordGeneratorService.GenerateSecurePassword(12);
            
            var user = _mapper.Map<ApplicationUser>(employeeDTO);
            user.RequiresPasswordChange = true; // Set flag for new users
            var result = await _userManager.CreateAsync(user, temporaryPassword);
            
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Không thể tạo tài khoản: {errors}");
            }

            // Send login credentials via email
            var fullName = $"{user.FirstName} {user.LastName}".Trim();
            var emailSent = await _emailService.SendLoginCredentialsAsync(user.Email, fullName, temporaryPassword);
            
            if (!emailSent)
            {
                // Log warning but don't fail the creation
                Console.WriteLine($"Warning: Could not send email to {user.Email}");
            }

            // Also add user to Identity role for authentication
            var identityRoleName = GetIdentityRoleName(role.RoleName);
            if (!string.IsNullOrEmpty(identityRoleName))
            {
                var addToRoleResult = await _userManager.AddToRoleAsync(user, identityRoleName);
                if (!addToRoleResult.Succeeded)
                {
                    var errors = string.Join(", ", addToRoleResult.Errors.Select(e => e.Description));
                    Console.WriteLine($"Warning: Could not add user to Identity role {identityRoleName}: {errors}");
                }
            }

            return await GetEmployeeByIdAsync(user.Id);
        }

        public async Task<EmployeeDTO> GetEmployeeByIdAsync(string id)
        {
            var user = await _context.ApplicationUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return null;

            return _mapper.Map<EmployeeDTO>(user);
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync()
        {
            var users = await _context.ApplicationUsers
                .Include(u => u.Role)
                .OrderBy(u => u.RoleID) // Sắp xếp theo chức vụ (RoleID)
                .ToListAsync();

            return _mapper.Map<IEnumerable<EmployeeDTO>>(users);
        }

        public async Task<EmployeeDTO> UpdateEmployeeAsync(EmployeeDTOPUT employeeDTO)
        {
            var user = await _context.ApplicationUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == employeeDTO.Id);

            if (user == null) 
                throw new Exception("Không tìm thấy nhân viên với ID đã cho");

            // Check if email is being changed and if new email already exists
            if (user.Email != employeeDTO.Email)
            {
                var existingUser = await _userManager.FindByEmailAsync(employeeDTO.Email);
                if (existingUser != null && existingUser.Id != user.Id)
                {
                    throw new Exception("Email đã được sử dụng bởi tài khoản khác");
                }
            }

            // Check if role exists
            var role = await _context.Roles.FindAsync(employeeDTO.RoleID);
            if (role == null)
            {
                throw new Exception("Chức danh không tồn tại");
            }

            // Update properties
            user.FirstName = employeeDTO.FirstName;
            user.LastName = employeeDTO.LastName;
            user.birthday = employeeDTO.Birthday;
            user.joinDate = employeeDTO.JoinDate;
            user.Phone = employeeDTO.Phone;
            user.Email = employeeDTO.Email;
            user.Gender = employeeDTO.Gender;
            user.RoleID = employeeDTO.RoleID;
            user.Status = employeeDTO.Status;

            // Update username and normalized fields
            user.UserName = employeeDTO.Email;
            user.NormalizedUserName = employeeDTO.Email.ToUpper();
            user.NormalizedEmail = employeeDTO.Email.ToUpper();

            await _context.SaveChangesAsync();

            // Also update Identity role for authentication
            var identityRoleName = GetIdentityRoleName(role.RoleName);
            if (!string.IsNullOrEmpty(identityRoleName))
            {
                // Remove user from all current roles
                var currentRoles = await _userManager.GetRolesAsync(user);
                if (currentRoles.Any())
                {
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);
                }
                
                // Add user to new role
                var addToRoleResult = await _userManager.AddToRoleAsync(user, identityRoleName);
                if (!addToRoleResult.Succeeded)
                {
                    var errors = string.Join(", ", addToRoleResult.Errors.Select(e => e.Description));
                    Console.WriteLine($"Warning: Could not update user Identity role to {identityRoleName}: {errors}");
                }
            }

            return await GetEmployeeByIdAsync(user.Id);
        }

        public async Task<bool> DeleteEmployeeAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) 
                throw new Exception("Không tìm thấy nhân viên với ID đã cho");

            // Check if user has related data that prevents deletion
            var hasRelatedData = await _context.ConstructionPlans.AnyAsync(cp => cp.EmployeeID == id) ||
                                await _context.ExportOrders.AnyAsync(eo => eo.EmployeeID == id) ||
                                await _context.Reports.AnyAsync(r => r.EmployeeID == id) ||
                                await _context.ImportOrderEmployees.AnyAsync(ioe => ioe.EmployeeID == id) ||
                                await _context.EmployeeRequests.AnyAsync(er => er.EmployeeID == id) ||
                                await _context.ApplicationUser_PayrollAdjustments.AnyAsync(apa => apa.EmployeeID == id) ||
                                await _context.Contracts.AnyAsync(c => c.EmployeeID == id);

            if (hasRelatedData)
            {
                throw new Exception("Không thể xóa nhân viên vì có dữ liệu liên quan. Vui lòng xóa các dữ liệu liên quan trước.");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Không thể xóa nhân viên: {errors}");
            }

            return true;
        }

        public async Task<IEnumerable<RoleDTO>> GetAllRolesAsync()
        {
            var roles = await _context.Roles.ToListAsync();
            return _mapper.Map<IEnumerable<RoleDTO>>(roles);
        }

        public async Task<EmployeeDTO> UpdateEmployeeStatusAsync(string employeeId, EmployeeStatusEnum status)
        {
            var employee = await _context.ApplicationUsers.FindAsync(employeeId);
            if (employee == null)
            {
                throw new Exception("Không tìm thấy nhân viên với ID đã cho");
            }

            employee.Status = status;
            await _context.SaveChangesAsync();

            return await GetEmployeeByIdAsync(employeeId);
        }

        // Helper method to map custom role names to Identity role names
        private string GetIdentityRoleName(string customRoleName)
        {
            return customRoleName?.ToLower() switch
            {
                "nhân viên kỹ thuật" => "technician",
                "chỉ huy công trình" => "manager", 
                "giám đốc" => "director",
                "nhân viên thợ" => "worker",
                "trưởng phòng hành chính – nhân sự" => "hr_manager",
                "nhân viên phòng hành chính - nhân sự" => "hr_employee",
                _ => null
            };
        }
    }
}
