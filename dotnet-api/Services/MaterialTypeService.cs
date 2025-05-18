using AutoMapper;
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
    public class MaterialTypeService : IMaterialTypeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MaterialTypeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<MaterialTypeDTO> GetMaterialTypeByIdAsync(int id)
        {
            var MaterialType = await _context.MaterialTypes
         
                .FirstOrDefaultAsync(c => c.ID == id);

            return MaterialType == null ? null : _mapper.Map<MaterialTypeDTO>(MaterialType);
        }

        public async Task<IEnumerable<MaterialTypeDTO>> GetAllMaterialTypeAsync()
        {
            var constructionsPlan = await _context.MaterialTypes
                           .ToListAsync();

            return _mapper.Map<IEnumerable<MaterialTypeDTO>>(constructionsPlan);
        }




    }
}
