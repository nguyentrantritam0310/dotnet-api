using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Services
{
    public class ConstructionItemService : IConstructionItemService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ConstructionItemService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ConstructionItemDTO> CreateConstructionItemAsync(ConstructionItemDTO constructionItemDTO)
        {
            var constructionItem = _mapper.Map<ConstructionItem>(constructionItemDTO);
            _context.ConstructionItems.Add(constructionItem);
            await _context.SaveChangesAsync();
            return _mapper.Map<ConstructionItemDTO>(constructionItem);
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

        public async Task<IEnumerable<ConstructionItemDTO>> GetAllConstructionsItemAsync()
        {
            var constructionsItem = await _context.ConstructionItems
                .Include(c => c.ConstructionPlans)
                .Include(c => c.Construction)

                .Include(c => c.ConstructionStatus)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ConstructionItemDTO>>(constructionsItem);
        }

        public async Task<ConstructionItemDTO> UpdateConstructionItemAsync(ConstructionItemDTO constructionItemDTO)
        {
            var existingConstructionItem = await _context.ConstructionItems.FindAsync(constructionItemDTO.ID);
            if (existingConstructionItem == null)
            {
                return null;
            }

            _mapper.Map(constructionItemDTO, existingConstructionItem);
            await _context.SaveChangesAsync();
            return _mapper.Map<ConstructionItemDTO>(existingConstructionItem);
        }

        public async Task<bool> DeleteConstructionItemAsync(int id)
        {
            var constructionItem = await _context.ConstructionItems.FindAsync(id);
            if (constructionItem == null)
            {
                return false;
            }

            _context.ConstructionItems.Remove(constructionItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
