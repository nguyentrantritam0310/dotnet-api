using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Services
{
    public class AdjustmentTypeService : IAdjustmentTypeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AdjustmentTypeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AdjustmentTypeDTO>> GetAllAdjustmentTypesAsync()
        {
            var adjustmentTypes = await _context.AdjustmentTypes
                .Include(at => at.AdjustmentItems)
                .ToListAsync();

            return _mapper.Map<IEnumerable<AdjustmentTypeDTO>>(adjustmentTypes);
        }

        public async Task<AdjustmentTypeDTO> GetAdjustmentTypeByIdAsync(int id)
        {
            var adjustmentType = await _context.AdjustmentTypes
                .Include(at => at.AdjustmentItems)
                .FirstOrDefaultAsync(x => x.ID == id);
            
            if (adjustmentType == null) return null;
            return _mapper.Map<AdjustmentTypeDTO>(adjustmentType);
        }
    }
}

