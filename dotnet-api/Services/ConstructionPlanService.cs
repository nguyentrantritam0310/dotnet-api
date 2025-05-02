using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Services
{
    public class ConstructionPlanService : IConstructionPlanService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ConstructionPlanService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ConstructionPlanDTO> CreateConstructionPlanAsync(ConstructionPlanDTO constructionPlanDTO)
        {
            var constructionPlan = _mapper.Map<ConstructionPlan>(constructionPlanDTO);
            _context.ConstructionPlans.Add(constructionPlan);
            await _context.SaveChangesAsync();
            return _mapper.Map<ConstructionPlanDTO>(constructionPlan);
        }

        public async Task<ConstructionPlanDTO> GetConstructionPlanByIdAsync(int id)
        {
            var constructionPlan = await _context.ConstructionPlans
                .Include(c => c.Employee)
                .Include(c => c.ConstructionItem)

                .Include(c => c.ConstructionStatus)
                .FirstOrDefaultAsync(c => c.ID == id);

            return constructionPlan == null ? null : _mapper.Map<ConstructionPlanDTO>(constructionPlan);
        }

        public async Task<IEnumerable<ConstructionPlanDTO>> GetAllConstructionsPlanAsync()
        {
            var constructionsPlan = await _context.ConstructionPlans
                .Include(c => c.Employee)
                .Include(c => c.ConstructionItem)

                .Include(c => c.ConstructionStatus)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ConstructionPlanDTO>>(constructionsPlan);
        }

        public async Task<ConstructionPlanDTO> UpdateConstructionPlanAsync(ConstructionPlanDTO constructionPlanDTO)
        {
            var existingConstructionPlan = await _context.ConstructionPlans.FindAsync(constructionPlanDTO.ID);
            if (existingConstructionPlan == null)
            {
                return null;
            }

            _mapper.Map(constructionPlanDTO, existingConstructionPlan);
            await _context.SaveChangesAsync();
            return _mapper.Map<ConstructionPlanDTO>(existingConstructionPlan);
        }

        public async Task<bool> DeleteConstructionPlanAsync(int id)
        {
            var constructionPlan = await _context.ConstructionPlans.FindAsync(id);
            if (constructionPlan == null)
            {
                return false;
            }

            _context.ConstructionPlans.Remove(constructionPlan);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
