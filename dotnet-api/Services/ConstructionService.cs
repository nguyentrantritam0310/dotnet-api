using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;

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

            if (createdConstruction == null)
                throw new Exception("Không tìm thấy công trình đã tạo");

            return _mapper.Map<ConstructionDTO>(createdConstruction);
        }

        public async Task<ConstructionDTO> CreateConstructionAsync(ConstructionDTOPOST constructionDTO, IFormFile designBlueprint)
        {
            string? fileName = null;
            if (designBlueprint != null && designBlueprint.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "blueprints");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);
                fileName = $"{Guid.NewGuid()}_{designBlueprint.FileName}";
                var filePath = Path.Combine(uploadsFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await designBlueprint.CopyToAsync(stream);
                }
            }
            constructionDTO.DesignBlueprint = fileName ?? string.Empty;
            return await CreateConstructionAsync(constructionDTO);
        }

        public async Task<ConstructionDTO> GetConstructionByIdAsync(int id)
        {
            var construction = await _context.Constructions
                .Include(c => c.ConstructionType)
                .Include(c => c.ConstructionStatus)
                .Include(c => c.ConstructionItems)
                    .ThenInclude(ci => ci.ConstructionStatus)
                .Include(c => c.ConstructionItems)
                    .ThenInclude(ci => ci.UnitOfMeasurement)
                .FirstOrDefaultAsync(c => c.ID == id);

            if (construction == null)
                throw new Exception("Không tìm thấy công trình với ID đã cung cấp");

            return _mapper.Map<ConstructionDTO>(construction);
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
                throw new Exception("Không tìm thấy công trình để cập nhật");
            }

            _mapper.Map(constructionDTO, existingConstruction);
            await _context.SaveChangesAsync();
            return _mapper.Map<ConstructionDTO>(existingConstruction);
        }

        public async Task<ConstructionDTO> UpdateConstructionAsync(ConstructionDTO constructionDTO, IFormFile designBlueprint)
        {
            string? fileName = constructionDTO.DesignBlueprint;
            if (designBlueprint != null && designBlueprint.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "blueprints");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);
                fileName = $"{Guid.NewGuid()}_{designBlueprint.FileName}";
                var filePath = Path.Combine(uploadsFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await designBlueprint.CopyToAsync(stream);
                }
                constructionDTO.DesignBlueprint = fileName;
            }
            var result = await UpdateConstructionAsync(constructionDTO);
            if (result == null)
                throw new Exception("Không tìm thấy công trình để cập nhật");
            return result;
        }

        public async Task<ConstructionDTO> UpdateConstructionStatusAsync(int id, int status)
        {
            var existingConstruction = await _context.Constructions
                .FirstOrDefaultAsync(c => c.ID == id);

            if (existingConstruction == null)
            {
                throw new Exception("Không tìm thấy công trình để cập nhật trạng thái");
            }

            existingConstruction.ConstructionStatusID = status;
            await _context.SaveChangesAsync();
            return _mapper.Map<ConstructionDTO>(existingConstruction);
        }
    }
}
