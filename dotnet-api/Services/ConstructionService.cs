using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
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

        public async Task<ConstructionDTO> CreateConstructionAsync(ConstructionDTOPOST constructionDTO)
        {
            var construction = _mapper.Map<Construction>(constructionDTO);
            
            // Set default status if not provided
            if (construction.ConstructionStatusID == 0)
            {
                construction.ConstructionStatusID = 1; // Default to "Chờ khởi công"
            }

            // Add construction first to get the ID
            _context.Constructions.Add(construction);
            await _context.SaveChangesAsync();

            // Map and add construction items
            if (constructionDTO.ConstructionItems != null && constructionDTO.ConstructionItems.Any())
            {
                foreach (var item in constructionDTO.ConstructionItems)
                {
                    var constructionItem = _mapper.Map<ConstructionItem>(item);
                    constructionItem.ConstructionID = construction.ID;
                    _context.ConstructionItems.Add(constructionItem);
                }
                await _context.SaveChangesAsync();
            }

            // Fetch the complete construction with items
            var createdConstruction = await _context.Constructions
                .Include(c => c.ConstructionType)
                .Include(c => c.ConstructionStatus)
                .Include(c => c.ConstructionItems)
                .ThenInclude(ci => ci.UnitOfMeasurement)
                .Include(c => c.ConstructionItems)
                .ThenInclude(ci => ci.ConstructionStatus)
                .FirstOrDefaultAsync(c => c.ID == construction.ID);

            return _mapper.Map<ConstructionDTO>(createdConstruction);
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
                .Include(c => c.ConstructionItems)
                .ThenInclude(ci => ci.UnitOfMeasurement)
                .Include(c => c.ConstructionItems)
                .ThenInclude(ci => ci.ConstructionStatus)
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

        public async Task<ConstructionDTO> UpdateConstructionStatusAsync(int id, int status)
        {
            var existingConstruction = await _context.Constructions
                .FirstOrDefaultAsync(c => c.ID == id);

            if (existingConstruction == null)
            {
                return null;
            }

            existingConstruction.ConstructionStatusID = status;
            await _context.SaveChangesAsync();
            return _mapper.Map<ConstructionDTO>(existingConstruction);
        }
    }
}
