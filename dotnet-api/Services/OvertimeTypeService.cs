using AutoMapper;
using dotnet_api.Data;
using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_api.Services
{
    public class OvertimeTypeService : IOvertimeTypeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OvertimeTypeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OvertimeTypeDTO>> GetAllOvertimeTypesAsync()
        {
            var overtimeTypes = await _context.OvertimeTypes.ToListAsync();
            return _mapper.Map<IEnumerable<OvertimeTypeDTO>>(overtimeTypes);
        }

        public async Task<OvertimeTypeDTO> GetOvertimeTypeByIdAsync(int id)
        {
            var overtimeType = await _context.OvertimeTypes.FirstOrDefaultAsync(ot => ot.ID == id);
            return _mapper.Map<OvertimeTypeDTO>(overtimeType);
        }
    }
}




