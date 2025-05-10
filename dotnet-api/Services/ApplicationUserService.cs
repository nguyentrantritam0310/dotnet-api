using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
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
    }
}
