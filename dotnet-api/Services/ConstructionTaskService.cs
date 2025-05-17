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
    public class ConstructionTaskService : IConstructionTaskService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ConstructionTaskService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ConstructionTaskDTO> CreateConstructionTaskAsync(ConstructionTaskDTOPOST constructionTaskDTO)
        {
            var constructionTask = _mapper.Map<ConstructionTask>(constructionTaskDTO);
            _context.ConstructionTasks.Add(constructionTask);
            await _context.SaveChangesAsync();
            return _mapper.Map<ConstructionTaskDTO>(constructionTask);
        }

        public async Task<ConstructionTaskDTO> GetConstructionTaskByIdAsync(int id)
        {
            var constructionTask = await _context.ConstructionTasks
                .Include(c => c.ConstructionStatus)
                .Include(c => c.ConstructionPlan)
                .ThenInclude(cp => cp.ConstructionItem)
                .ThenInclude(ci => ci.UnitOfMeasurement)
                .Include(c => c.ConstructionStatus)
                .FirstOrDefaultAsync(c => c.ID == id);

            return constructionTask == null ? null : _mapper.Map<ConstructionTaskDTO>(constructionTask);
        }

        public async Task<IEnumerable<ConstructionTaskDTO>> GetAllConstructionsTaskByPlanAsync(int id)
        {
            var constructionsTask = await _context.ConstructionTasks
                .Include(c => c.ConstructionStatus)
                .Include(c => c.ConstructionPlan)
                .ThenInclude(cp => cp.ConstructionItem)
                .ThenInclude(ci => ci.UnitOfMeasurement)
                 .OrderByDescending(c => c.ID)
                 .Where(c => c.ConstructionPlanID == id)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ConstructionTaskDTO>>(constructionsTask);
        }

        public async Task<IEnumerable<ConstructionTaskDTO>> GetAllConstructionsTaskByItemAsync(int id)
        {
            var constructionsTask = await _context.ConstructionTasks
                .Include(c => c.ConstructionStatus)
                .Include(c => c.ConstructionPlan)
                .ThenInclude(cp => cp.ConstructionItem)
                .ThenInclude(ci => ci.UnitOfMeasurement)
                 .OrderByDescending(c => c.ID)
                 .Where(c => c.ConstructionPlan.ConstructionItem.ID == id)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ConstructionTaskDTO>>(constructionsTask);
        }

        public async Task<ConstructionTaskDTO> UpdateConstructionTaskAsync(ConstructionTaskDTOPOST constructionTaskDTO)
        {

            var existingConstructionTask = await _context.ConstructionTasks
                .FirstOrDefaultAsync(c => c.ID == constructionTaskDTO.ID);

            if (existingConstructionTask == null)
            {
                return null;
            }

            _mapper.Map(constructionTaskDTO, existingConstructionTask);
            await _context.SaveChangesAsync();
            return _mapper.Map<ConstructionTaskDTO>(existingConstructionTask);
        }

        public async Task<ConstructionTaskDTO> UpdateConstructionTaskStatusAsync(int id, int status)
        {
            var existingConstructionTask = await _context.ConstructionTasks
                 .Include(c => c.ConstructionStatus)
                .Include(c => c.ConstructionPlan)
                .ThenInclude(cp => cp.ConstructionItem)
                .ThenInclude(ci => ci.UnitOfMeasurement)
                .FirstOrDefaultAsync(c => c.ID == id);

            if (existingConstructionTask == null)
            {
                return null;
            }

            existingConstructionTask.ConstructionStatusID = status;
            await _context.SaveChangesAsync();
            return _mapper.Map<ConstructionTaskDTO>(existingConstructionTask);
        }

        public async Task<ConstructionTaskDTO> UpdateConstructionTaskActualAsync(int id, float? actualWorkload)
        {
            var existingConstructionTask = await _context.ConstructionTasks
              .Include(c => c.ConstructionStatus)
             .Include(c => c.ConstructionPlan)
             .ThenInclude(cp => cp.ConstructionItem)
             .ThenInclude(ci => ci.UnitOfMeasurement)
             .FirstOrDefaultAsync(c => c.ID == id);

            if (existingConstructionTask == null)
            {
                return null;
            }

            existingConstructionTask.ActualWorkload = actualWorkload;
            await _context.SaveChangesAsync();
            return _mapper.Map<ConstructionTaskDTO>(existingConstructionTask);
        }
    }
}
