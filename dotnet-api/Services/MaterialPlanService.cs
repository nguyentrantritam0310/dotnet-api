using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Services
{
    public class MaterialPlanService : IMaterialPlanService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MaterialPlanService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MaterialPlanDTOPOST> CreateMaterialPlanAsync(MaterialPlanDTOPOST MaterialPlanDTO)
        {
            var MaterialPlan = _mapper.Map<MaterialPlan>(MaterialPlanDTO);
            _context.MaterialPlans.Add(MaterialPlan);
            await _context.SaveChangesAsync();
            return _mapper.Map<MaterialPlanDTOPOST>(MaterialPlan);
        }

        public async Task<IEnumerable<MaterialPlanDTO>> GetMaterialPlanByIdAsync(int importOrderId)
        {
            var materialPlan = await _context.MaterialPlans
                .Include(c => c.Material)
                .ThenInclude(c => c.UnitOfMeasurement)
                     .Where(c => c.ImportOrderID == importOrderId)
            .ToListAsync();

            return materialPlan == null ? null : _mapper.Map<IEnumerable<MaterialPlanDTO>>(materialPlan);
        }
        public async Task<MaterialPlanDTOPOST> UpdateMaterialPlanQuantityAndNoteAsync(MaterialPlanDTOPOST dto)
        {
            var materialPlan = await _context.MaterialPlans
                .FirstOrDefaultAsync(mp => mp.ImportOrderID == dto.ImportOrderID && mp.MaterialID == dto.MaterialID);

            if (materialPlan == null)
                return null;

            materialPlan.ImportQuantity = dto.ImportQuantity;
            materialPlan.Note = dto.Note;
            await _context.SaveChangesAsync();

            return _mapper.Map<MaterialPlanDTOPOST>(materialPlan);
        }

        //public async Task<IEnumerable<MaterialPlanDTO>> GetAllMaterialPlanAsync()
        //{   
        //    var MaterialPlans = await _context.MaterialPlans
        //        .Include(c => c.MaterialPlanType)
        //        .ToListAsync();

        //    return _mapper.Map<IEnumerable<MaterialPlanDTO>>(MaterialPlans);
        //}

        //public async Task<MaterialPlanDTO> UpdateMaterialPlanAsync(MaterialPlanDTO MaterialPlanDTO)
        //{
        //    var existingMaterialPlan = await _context.MaterialPlans.FindAsync(MaterialPlanDTO.ID);
        //    if (existingMaterialPlan == null)
        //    {
        //        return null;
        //    }

        //    _mapper.Map(MaterialPlanDTO, existingMaterialPlan);
        //    await _context.SaveChangesAsync();
        //    return _mapper.Map<MaterialPlanDTO>(existingMaterialPlan);
        //}

        //public async Task<bool> DeleteMaterialPlanAsync(int id)
        //{
        //    var MaterialPlan = await _context.MaterialPlans.FindAsync(id);
        //    if (MaterialPlan == null)
        //    {
        //        return false;
        //    }

        //    _context.MaterialPlans.Remove(MaterialPlan);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}
    }
}
