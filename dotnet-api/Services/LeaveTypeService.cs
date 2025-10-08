using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Services
{
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LeaveTypeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LeaveTypeDTO>> GetAllLeaveTypesAsync()
        {
            var leaveTypes = await _context.LeaveTypes
                .OrderBy(lt => lt.LeaveTypeName)
                .ToListAsync();

            return _mapper.Map<IEnumerable<LeaveTypeDTO>>(leaveTypes);
        }

        public async Task<LeaveTypeDTO> GetLeaveTypeByIdAsync(int id)
        {
            var leaveType = await _context.LeaveTypes
                .FirstOrDefaultAsync(lt => lt.ID == id);

            if (leaveType == null) return null;
            return _mapper.Map<LeaveTypeDTO>(leaveType);
        }
    }
}
