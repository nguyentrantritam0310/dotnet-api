using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MaterialService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MaterialDTO> CreateMaterialAsync(MaterialDTOPOST MaterialDTO)
        {
            var Material = _mapper.Map<Material>(MaterialDTO);
            _context.Materials.Add(Material);
            await _context.SaveChangesAsync();
            return _mapper.Map<MaterialDTO>(Material);
        }

        public async Task<MaterialDTO> GetMaterialByIdAsync(int id)
        {
            var Material = await _context.Materials
                //.Include(c => c.Construction)
                //.Include(c => c.Employee)
                .Include(c => c.MaterialType)

                .FirstOrDefaultAsync(c => c.ID == id);

            return Material == null ? null : _mapper.Map<MaterialDTO>(Material);
        }

        public async Task<IEnumerable<MaterialDTO>> GetAllMaterialAsync()
        {   
            var Materials = await _context.Materials
                .Include(c => c.MaterialType)
                .Include(c => c.UnitOfMeasurement)
                .ToListAsync();

            return _mapper.Map<IEnumerable<MaterialDTO>>(Materials);
        }

        public async Task<MaterialDTO> UpdateMaterialAsync(MaterialDTOPOST MaterialDTO)
        {
            var existingMaterial = await _context.Materials.FindAsync(MaterialDTO.ID);
            if (existingMaterial == null)
            {
                return null;
            }

            _mapper.Map(MaterialDTO, existingMaterial);
            await _context.SaveChangesAsync();
            return _mapper.Map<MaterialDTO>(existingMaterial);
        }

        public async Task<bool> DeleteMaterialAsync(int id)
        {
            var Material = await _context.Materials.FindAsync(id);
            if (Material == null)
            {
                return false;
            }

            _context.Materials.Remove(Material);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<MaterialUpdateStockQuantityDTO> UpdateStockQuantityMaterialAsync(MaterialUpdateStockQuantityDTO materialUpdateStockQuantityDTO)
        {
            var existingMaterial = await _context.Materials.FindAsync(materialUpdateStockQuantityDTO.ID);
            if (existingMaterial == null)
            {
                return null;
            }

            _mapper.Map(materialUpdateStockQuantityDTO, existingMaterial);
            await _context.SaveChangesAsync();
            return _mapper.Map<MaterialUpdateStockQuantityDTO>(existingMaterial);
        }
    }
}
