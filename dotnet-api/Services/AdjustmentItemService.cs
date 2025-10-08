using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Services
{
    public class AdjustmentItemService : IAdjustmentItemService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AdjustmentItemService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AdjustmentItemDTO>> GetAllAdjustmentItemsAsync()
        {
            var adjustmentItems = await _context.AdjustmentItems
                .Include(ai => ai.adjustmentType)
                .ToListAsync();

            return _mapper.Map<IEnumerable<AdjustmentItemDTO>>(adjustmentItems);
        }

        public async Task<IEnumerable<AdjustmentItemDTO>> GetAdjustmentItemsByTypeIdAsync(int adjustmentTypeId)
        {
            var adjustmentItems = await _context.AdjustmentItems
                .Include(ai => ai.adjustmentType)
                .Where(ai => ai.AdjustmentTypeID == adjustmentTypeId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<AdjustmentItemDTO>>(adjustmentItems);
        }

        public async Task<AdjustmentItemDTO> GetAdjustmentItemByIdAsync(int id)
        {
            var adjustmentItem = await _context.AdjustmentItems
                .Include(ai => ai.adjustmentType)
                .FirstOrDefaultAsync(x => x.ID == id);
            
            if (adjustmentItem == null) return null;
            return _mapper.Map<AdjustmentItemDTO>(adjustmentItem);
        }
    }
}

