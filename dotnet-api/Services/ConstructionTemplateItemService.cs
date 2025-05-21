using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Services
{
    public class ConstructionTemplateItemService : IConstructionTemplateItemService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ConstructionTemplateItemService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ConstructionTemplateItemDTO>> GetConstructionTemplateItemByConstructionTypeIdAsync(int id)
        {
            var constructionsTemplateItem = await _context.ConstructionTemplateItems
              .Include(c => c.ConstructionType)
                .Include(c => c.WorkSubTypeVariant)
              .Where(c => c.ConstructionTypeID == id)
              .ToListAsync();

            return constructionsTemplateItem == null ? null : _mapper.Map<IEnumerable<ConstructionTemplateItemDTO>>(constructionsTemplateItem);
        }

        public async Task<IEnumerable<ConstructionTemplateItemDTO>> GetAllConstructionsTemplateItemAsync()
        {
            var constructionsTemplateItem = await _context.ConstructionTemplateItems
                .Include(c => c.ConstructionType)
                .Include(c => c.WorkSubTypeVariant)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ConstructionTemplateItemDTO>>(constructionsTemplateItem);
        }

    }
}
