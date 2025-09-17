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
    public class AttendanceMachineService : IAttendanceMachineService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private const string UPLOAD_DIRECTORY = "uploads/blueprints";

        public AttendanceMachineService(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }


        public async Task<IEnumerable<AttendanceMachineDTO>> GetAllAttendanceMachinesAsync()
        {
            var AttendanceMachines = await _context.AttendanceMachines
                .ToListAsync();

            return _mapper.Map<IEnumerable<AttendanceMachineDTO>>(AttendanceMachines);
        }

        public async Task<AttendanceMachineDTO> GetAttendanceMachineByIdAsync(int id)
        {
            var machine = await _context.AttendanceMachines.FirstOrDefaultAsync(x => x.ID == id);
            if (machine == null) return null;
            return _mapper.Map<AttendanceMachineDTO>(machine);
        }

        public async Task<AttendanceMachineDTO> CreateAttendanceMachineAsync(AttendanceMachineDTO attendanceMachineDTO)
        {
            var entity = _mapper.Map<AttendanceMachine>(attendanceMachineDTO);
            _context.AttendanceMachines.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<AttendanceMachineDTO>(entity);
        }

        public async Task<AttendanceMachineDTO> UpdateAttendanceMachineAsync(AttendanceMachineDTO dto)
        {
            var entity = await _context.AttendanceMachines.FirstOrDefaultAsync(x => x.ID == dto.ID);
            if (entity == null) return null;

            // Map dữ liệu mới vào entity hiện có
            _mapper.Map(dto, entity);

            _context.AttendanceMachines.Update(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<AttendanceMachineDTO>(entity);
        }

        public async Task<AttendanceMachineDTO> DeleteAttendanceMachineAsync(int id)
        {
            var entity = await _context.AttendanceMachines.FirstOrDefaultAsync(x => x.ID == id);
            if (entity == null) return null;

            _context.AttendanceMachines.Remove(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<AttendanceMachineDTO>(entity);
        }
    }
}
