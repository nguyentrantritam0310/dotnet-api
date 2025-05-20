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

        public async Task<ConstructionItemDTO> CreateConstructionItemAsync(ConstructionItemCreateDTO itemDTO)
        {
            var constructionItem = _mapper.Map<ConstructionItem>(itemDTO);
            
            // Set default status if not provided
            constructionItem.ConstructionStatusID = 1; // Default to "Chờ khởi công"

            _context.ConstructionItems.Add(constructionItem);
            await _context.SaveChangesAsync();

            // Fetch the complete item with related data
            var createdItem = await GetCompleteConstructionItem(constructionItem.ID);
            return _mapper.Map<ConstructionItemDTO>(createdItem);
        }

        public async Task<ConstructionItemDTO> GetConstructionItemByIdAsync(int id)
        {
            var item = await GetCompleteConstructionItem(id);
            return item == null ? null : _mapper.Map<ConstructionItemDTO>(item);
        }

        public async Task<IEnumerable<ConstructionItemDTO>> GetConstructionItemsByConstructionIdAsync(int constructionId)
        {
            var items = await _context.ConstructionItems
                .Include(i => i.UnitOfMeasurement)
                .Include(i => i.WorkSubTypeVariant)
                .Include(i => i.ConstructionStatus)
                .Where(i => i.ConstructionID == constructionId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ConstructionItemDTO>>(items);
        }

        public async Task<ConstructionItemDTO> UpdateConstructionItemAsync(ConstructionItemUpdateDTO itemDTO)
        {
            var existingItem = await _context.ConstructionItems.FindAsync(itemDTO.ID);
            if (existingItem == null)
            {
                throw new Exception("Không tìm thấy hạng mục để cập nhật");
            }

            _mapper.Map(itemDTO, existingItem);
            await _context.SaveChangesAsync();

            // Fetch the updated item with related data
            var updatedItem = await GetCompleteConstructionItem(existingItem.ID);
            return _mapper.Map<ConstructionItemDTO>(updatedItem);
        }

        public async Task<ConstructionItemDTO> UpdateConstructionItemStatusAsync(int id, int status)
        {
            var existingItem = await _context.ConstructionItems.FindAsync(id);
            if (existingItem == null)
            {
                throw new Exception("Không tìm thấy hạng mục để cập nhật trạng thái");
            }

            existingItem.ConstructionStatusID = status;
            await _context.SaveChangesAsync();

            // Fetch the updated item with related data
            var updatedItem = await GetCompleteConstructionItem(id);
            return _mapper.Map<ConstructionItemDTO>(updatedItem);
        }

        public async Task DeleteConstructionItemAsync(int id)
        {
            var item = await _context.ConstructionItems.FindAsync(id);
            if (item == null)
            {
                throw new Exception("Không tìm thấy hạng mục để xóa");
            }

            _context.ConstructionItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        private async Task<ConstructionItem> GetCompleteConstructionItem(int id)
        {
            return await _context.ConstructionItems
                .Include(i => i.UnitOfMeasurement)
                .Include(i => i.WorkSubTypeVariant)
                .Include(i => i.ConstructionStatus)
                .FirstOrDefaultAsync(i => i.ID == id);
        }
    }
}
