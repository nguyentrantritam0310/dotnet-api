using dotnet_api.DTOs;
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
    }
}
