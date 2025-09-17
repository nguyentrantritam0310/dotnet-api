using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.Services.Interfaces;
using dotnet_api.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text.Json;

namespace dotnet_api.Services
{
    public class WorkShiftService : IWorkShiftService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private const string UPLOAD_DIRECTORY = "uploads/blueprints";

        public WorkShiftService(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }

        

        public async Task<IEnumerable<WorkShiftDTO>> GetAllWorkShiftsAsync()
        {
            var workshifts = await _context.WorkShifts
                .Include(c => c.ShiftDetails)
                .ToListAsync();

            return _mapper.Map<IEnumerable<WorkShiftDTO>>(workshifts);
        }

        public Task<WorkShiftDTO> GetWorkShiftByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
