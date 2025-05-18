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
    public class UnitofMeasurementService : IUnitofMeasurementService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UnitofMeasurementService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        public async Task<IEnumerable<UnitofMeasurementDTO>> GetAllUnitofMeasurementAsync()
        {
            var constructionsPlan = await _context.UnitofMeasuremens
                           .ToListAsync();

            return _mapper.Map<IEnumerable<UnitofMeasurementDTO>>(constructionsPlan);
        }




    }
}