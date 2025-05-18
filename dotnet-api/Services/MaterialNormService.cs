using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Python.Runtime;

namespace dotnet_api.Services
{
    public class MaterialNormService : IMaterialNormService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MaterialNormService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MaterialNormDTO>> GetAllMaterialNormByConstructionAsync(int id)
        {
            var materialNorm = await _context.MaterialNorms
                .Include(c => c.Material)
                    .ThenInclude(w => w.UnitOfMeasurement)
                .Include(c => c.WorkSubTypeVariant)
                    .ThenInclude(w => w.ConstructionItems)
                .Include(c => c.WorkSubTypeVariant)
                    .ThenInclude(w => w.ConstructionItems)
                        .ThenInclude(w => w.Construction)
                .Where(c => c.WorkSubTypeVariant.ConstructionItems
                .Any(ci => ci.Construction.ID == id)) 
                .ToListAsync();

            return _mapper.Map<IEnumerable<MaterialNormDTO>>(materialNorm);
        }

        public async Task<IEnumerable<MaterialNormItemDTO>> GetAllMaterialNormByConstructionItemAsync(int id)
        {
            var materialNorm = await _context.MaterialNorms
                .Include(c => c.Material)
                    .ThenInclude(w => w.UnitOfMeasurement)
                .Include(c => c.WorkSubTypeVariant)
                    .ThenInclude(w => w.ConstructionItems)
                .Where(c => c.WorkSubTypeVariant.ConstructionItems
                .Any(ci => ci.ID == id))
                .ToListAsync();

            return _mapper.Map<IEnumerable<MaterialNormItemDTO>>(materialNorm);
        }
    }
}
