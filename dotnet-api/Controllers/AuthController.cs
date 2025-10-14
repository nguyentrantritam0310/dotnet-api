using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using dotnet_api.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using dotnet_api.Data;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtService _jwtService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly ApplicationDbContext _context;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            JwtService jwtService,
            RoleManager<IdentityRole> roleManager,
            IEmailService emailService,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _roleManager = roleManager;
            _emailService = emailService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Gán role mặc định là "User"
                await _userManager.AddToRoleAsync(user, "User");
                return Ok(new { Message = "Đăng ký thành công" });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized(new { Message = "Email không tồn tại" });

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (result.Succeeded)
            {
                var token = await _jwtService.GenerateJwtToken(user);
                var refreshToken = _jwtService.GenerateRefreshToken();

                // Lưu refresh token
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                await _userManager.UpdateAsync(user);

                // Kiểm tra nếu user cần đổi mật khẩu
                bool isDefaultPassword = user.RequiresPasswordChange;

                return Ok(new AuthResponseDTO
                {
                    IsSuccess = true,
                    Message = "Đăng nhập thành công",
                    Token = token,
                    RefreshToken = refreshToken,
                    Expiration = DateTime.Now.AddDays(1),
                    RequiresPasswordChange = isDefaultPassword
                });
            }

            return Unauthorized(new { Message = "Mật khẩu không đúng" });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDTO model)
        {
            var principal = _jwtService.GetPrincipalFromExpiredToken(model.Token);
            var email = principal.FindFirst(ClaimTypes.Email)?.Value;

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || user.RefreshToken != model.RefreshToken || 
                user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest(new { Message = "Invalid refresh token" });

            var newToken = await _jwtService.GenerateJwtToken(user);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(user);

            return Ok(new AuthResponseDTO
            {
                IsSuccess = true,
                Message = "Token refreshed successfully",
                Token = newToken,
                RefreshToken = newRefreshToken,
                Expiration = DateTime.Now.AddDays(1)
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
                return BadRequest(new { Message = "User not found" });

            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiryTime = null;
                await _userManager.UpdateAsync(user);
            }

            await _signInManager.SignOutAsync();
            return Ok(new { Message = "Đăng xuất thành công" });
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
                return Unauthorized(new { Message = "User not found" });

            var user = await _context.ApplicationUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
            
            if (user == null)
                return Unauthorized(new { Message = "User not found" });

            // Get role from custom Role table and map to English role names
            var roleName = GetEnglishRoleName(user.Role?.RoleName);
            
            return Ok(new
            {
                id = user.Id,
                email = user.Email,
                firstName = user.FirstName,
                lastName = user.LastName,
                fullName = $"{user.FirstName} {user.LastName}",
                role = roleName
            });
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
                return Unauthorized(new { Message = "User not found" });

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Unauthorized(new { Message = "User not found" });

            // Kiểm tra mật khẩu hiện tại
            var isCurrentPasswordValid = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);
            if (!isCurrentPasswordValid)
                return BadRequest(new { Message = "Mật khẩu hiện tại không đúng" });

            // Đổi mật khẩu
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                // Clear the password change requirement flag
                user.RequiresPasswordChange = false;
                await _userManager.UpdateAsync(user);
                
                // Send password changed notification email
                var fullName = $"{user.FirstName} {user.LastName}".Trim();
                _ = Task.Run(async () => await _emailService.SendPasswordChangedNotificationAsync(user.Email, fullName));
                
                return Ok(new { Message = "Đổi mật khẩu thành công" });
            }

            return BadRequest(new { Message = "Đổi mật khẩu thất bại", Errors = result.Errors });
        }

        [HttpPost("assign-role")]
        [Authorize(Roles = "director")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return NotFound(new { Message = "User not found" });

            // Kiểm tra role có tồn tại không
            if (!await _roleManager.RoleExistsAsync(model.Role))
                return BadRequest(new { Message = "Role does not exist" });

            // Xóa tất cả role hiện tại
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            // Gán role mới
            var result = await _userManager.AddToRoleAsync(user, model.Role);
            if (result.Succeeded)
            {
                return Ok(new { Message = "Role assigned successfully" });
            }

            return BadRequest(result.Errors);
        }

        // Helper method to map Vietnamese role names to English role names
        private string GetEnglishRoleName(string vietnameseRoleName)
        {
            return vietnameseRoleName?.ToLower() switch
            {
                "nhân viên kỹ thuật" => "technician",
                "chỉ huy công trình" => "manager", 
                "giám đốc" => "director",
                "nhân viên thợ" => "worker",
                "trưởng phòng hành chính – nhân sự" => "hr_manager",
                "nhân viên phòng hành chính - nhân sự" => "hr_employee",
                _ => "user"
            };
        }
    }
} 