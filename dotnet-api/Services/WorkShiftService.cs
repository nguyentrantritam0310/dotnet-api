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

        public async Task<WorkShiftDTO> GetWorkShiftByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<WorkShiftDTO> CreateWorkShiftAsync(WorkShiftDTOPOST workShiftDTO)
        {
            var entity = _mapper.Map<WorkShift>(workShiftDTO);
            _context.WorkShifts.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<WorkShiftDTO>(entity);
        }

        public async Task<WorkShiftDTO> UpdateWorkShiftAsync(WorkShiftDTOPUT dto)
        {
            var entity = await _context.WorkShifts
                .Include(w => w.ShiftDetails)
                .FirstOrDefaultAsync(w => w.ID == dto.ID);

            if (entity == null) return null;

            entity.ShiftName = dto.ShiftName;

            // Xoá chi tiết cũ
            _context.ShiftDetails.RemoveRange(entity.ShiftDetails);
            // Thêm chi tiết mới
            entity.ShiftDetails = _mapper.Map<List<ShiftDetail>>(dto.ShiftDetails);

            await _context.SaveChangesAsync();
            return _mapper.Map<WorkShiftDTO>(entity);
        }

        public async Task<bool> DeleteWorkShiftAsync(int id)
        {
            var entity = await _context.WorkShifts
                .Include(w => w.ShiftDetails)
                .FirstOrDefaultAsync(w => w.ID == id);

            if (entity == null) return false;

            _context.ShiftDetails.RemoveRange(entity.ShiftDetails);
            _context.WorkShifts.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
