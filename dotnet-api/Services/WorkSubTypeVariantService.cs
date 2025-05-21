using AutoMapper;
using dotnet_api.Data;
using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Services
{
    public class WorkSubTypeVariantService : IWorkSubTypeVariantService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public WorkSubTypeVariantService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WorkSubTypeVariantDTO>> GetAllWorkSubTypeVariantsAsync()
        {
            var variants = await _context.WorkSubTypeVariants
                .Include(w => w.WorkSubType)
                .ToListAsync();
            return _mapper.Map<IEnumerable<WorkSubTypeVariantDTO>>(variants);
        }

        public async Task<WorkSubTypeVariantDTO> GetWorkSubTypeVariantByIdAsync(int id)
        {
            var variant = await _context.WorkSubTypeVariants
                .Include(w => w.WorkSubType)
                .FirstOrDefaultAsync(w => w.ID == id);
            return _mapper.Map<WorkSubTypeVariantDTO>(variant);
        }
    }
} 