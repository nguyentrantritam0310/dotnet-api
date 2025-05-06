using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Services
{
    public class ConstructionService : IConstructionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ConstructionService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ConstructionDTO> CreateConstructionAsync(ConstructionDTO constructionDTO)
        {
            var construction = _mapper.Map<Construction>(constructionDTO);
            _context.Constructions.Add(construction);
            await _context.SaveChangesAsync();
            return _mapper.Map<ConstructionDTO>(construction);
        }

        public async Task<ConstructionDTO> GetConstructionByIdAsync(int id)
        {
            var construction = await _context.Constructions
                .Include(c => c.ConstructionType)
                .Include(c => c.ConstructionStatus)
                .Include(c => c.ConstructionItems)
                .FirstOrDefaultAsync(c => c.ID == id);

            return construction == null ? null : _mapper.Map<ConstructionDTO>(construction);
        }

        public async Task<IEnumerable<ConstructionDTO>> GetAllConstructionsAsync()
        {
            var constructions = await _context.Constructions
                .Include(c => c.ConstructionType)
                .Include(c => c.ConstructionStatus)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ConstructionDTO>>(constructions);
        }

        public async Task<ConstructionDTO> UpdateConstructionAsync(ConstructionDTO constructionDTO)
        {
            var existingConstruction = await _context.Constructions.FindAsync(constructionDTO.ID);
            if (existingConstruction == null)
            {
                return null;
            }

            _mapper.Map(constructionDTO, existingConstruction);
            await _context.SaveChangesAsync();
            return _mapper.Map<ConstructionDTO>(existingConstruction);
        }

        public async Task<bool> DeleteConstructionAsync(int id)
        {
            var construction = await _context.Constructions.FindAsync(id);
            if (construction == null)
            {
                return false;
            }

            _context.Constructions.Remove(construction);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
