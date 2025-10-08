using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
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

        public ApplicationUserService(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
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

            // Check if employee code already exists
            var existingEmployeeCode = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.EmployeeCode == employeeDTO.EmployeeCode);
            if (existingEmployeeCode != null)
            {
                throw new Exception("Mã nhân viên đã được sử dụng bởi tài khoản khác");
            }

            // Check if role exists
            var role = await _context.Roles.FindAsync(employeeDTO.RoleID);
            if (role == null)
            {
                throw new Exception("Chức danh không tồn tại");
            }

            var user = _mapper.Map<ApplicationUser>(employeeDTO);
            var result = await _userManager.CreateAsync(user, employeeDTO.Password);
            
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Không thể tạo tài khoản: {errors}");
            }

            // Note: Role is already set via RoleID in the user entity
            // No need to use UserManager.AddToRoleAsync since we're using custom Roles table
            // The role is already assigned through user.RoleID

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

            // Check if employee code is being changed and if new code already exists
            if (user.EmployeeCode != employeeDTO.EmployeeCode)
            {
                var existingEmployeeCode = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.EmployeeCode == employeeDTO.EmployeeCode);
                if (existingEmployeeCode != null && existingEmployeeCode.Id != user.Id)
                {
                    throw new Exception("Mã nhân viên đã được sử dụng bởi tài khoản khác");
                }
            }

            // Check if role exists
            var role = await _context.Roles.FindAsync(employeeDTO.RoleID);
            if (role == null)
            {
                throw new Exception("Chức danh không tồn tại");
            }

            // Update properties
            user.EmployeeCode = employeeDTO.EmployeeCode;
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

            // Note: Role is already updated via user.RoleID
            // No need to use UserManager role management since we're using custom Roles table

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
    }
}
