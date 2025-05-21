using dotnet_api.DTOs;

namespace dotnet_api.Services.Interfaces
{
    public interface IApplicationUserService
    {
        Task<IEnumerable<ApplicationUserDTO>> GetAllApplicationUserByRoleNameAsync(string roleName);
    }
}
