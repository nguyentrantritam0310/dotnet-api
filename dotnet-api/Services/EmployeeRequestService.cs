using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.Data.Enums;
using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.DTOs.PUT;
using dotnet_api.Helpers;
using dotnet_api.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text.Json;

namespace dotnet_api.Services
{
    public class EmployeeRequestService : IEmployeeRequestService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private const string UPLOAD_DIRECTORY = "uploads/blueprints";

        public EmployeeRequestService(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }


        public async Task<IEnumerable<EmployeeRequestDTO>> GetAllEmployeeRequestsAsync()
        {
            var employeeRequests = await _context.EmployeeRequests
                .Include(io => io.Employee)
                .Include(io => io.LeaveType)
                .Include(io => io.OvertimeType)
                .Include(io => io.OvertimeForm)
                .ToListAsync();

            return _mapper.Map<IEnumerable<EmployeeRequestDTO>>(employeeRequests);
        }

        public async Task<EmployeeRequestDTO> GetEmployeeRequestByIdAsync(string VoucherCode)
        {
            var machine = await _context.EmployeeRequests.FirstOrDefaultAsync(x => x.VoucherCode == VoucherCode);
            if (machine == null) return null;
            return _mapper.Map<EmployeeRequestDTO>(machine);
        }

        public async Task<EmployeeRequestDTO> CreateEmployeeRequestAsync(EmployeeRequestDTO employeeRequestDTO)
        {
            var entity = _mapper.Map<EmployeeRequests>(employeeRequestDTO);
            _context.EmployeeRequests.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<EmployeeRequestDTO>(entity);
        }

        public async Task<EmployeeRequestDTO> UpdateEmployeeRequestAsync(EmployeeRequestDTO dto)
        {
            var entity = await _context.EmployeeRequests.FirstOrDefaultAsync(x => x.VoucherCode == dto.VoucherCode);
            if (entity == null) return null;

            // Map dữ liệu mới vào entity hiện có
            _mapper.Map(dto, entity);

            _context.EmployeeRequests.Update(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<EmployeeRequestDTO>(entity);
        }

        public async Task<EmployeeRequestDTO> DeleteEmployeeRequestAsync(string voucherCode)
        {
            var entity = await _context.EmployeeRequests.FirstOrDefaultAsync(x => x.VoucherCode == voucherCode);
            if (entity == null) return null;

            _context.EmployeeRequests.Remove(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<EmployeeRequestDTO>(entity);
        }

        // Leave Request specific implementations
        public async Task<LeaveRequestDTO> CreateLeaveRequestAsync(LeaveRequestDTOPOST leaveRequestDTO)
        {
            var entity = _mapper.Map<EmployeeRequests>(leaveRequestDTO);
            _context.EmployeeRequests.Add(entity);
            await _context.SaveChangesAsync();

            // Reload with related data for proper mapping
            var createdEntity = await _context.EmployeeRequests
                .Include(e => e.Employee)
                .Include(e => e.LeaveType)
                .Include(e => e.WorkShift)
                .FirstOrDefaultAsync(e => e.VoucherCode == entity.VoucherCode);

            return _mapper.Map<LeaveRequestDTO>(createdEntity);
        }

        public async Task<LeaveRequestDTO> GetLeaveRequestByIdAsync(string voucherCode)
        {
            var entity = await _context.EmployeeRequests
                .Include(e => e.Employee)
                .Include(e => e.LeaveType)
                .Include(e => e.WorkShift)
                .FirstOrDefaultAsync(x => x.VoucherCode == voucherCode && (x.RequestType == "Leave" || x.RequestType == "Nghỉ phép"));
            
            if (entity == null) return null;
            return _mapper.Map<LeaveRequestDTO>(entity);
        }

        public async Task<IEnumerable<LeaveRequestDTO>> GetAllLeaveRequestsAsync()
        {
            var leaveRequests = await _context.EmployeeRequests
                .Include(e => e.Employee)
                .Include(e => e.LeaveType)
                .Include(e => e.WorkShift)
                .Where(e => e.RequestType == "Leave" || e.RequestType == "Nghỉ phép")
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();

            return _mapper.Map<IEnumerable<LeaveRequestDTO>>(leaveRequests);
        }

        public async Task<LeaveRequestDTO> UpdateLeaveRequestAsync(LeaveRequestDTOPUT leaveRequestDTO)
        {
            var entity = await _context.EmployeeRequests
                .FirstOrDefaultAsync(x => x.VoucherCode == leaveRequestDTO.VoucherCode && (x.RequestType == "Leave" || x.RequestType == "Nghỉ phép"));
            
            if (entity == null) return null;

            // Map dữ liệu mới vào entity hiện có
            _mapper.Map(leaveRequestDTO, entity);

            _context.EmployeeRequests.Update(entity);
            await _context.SaveChangesAsync();

            // Reload with related data for proper mapping
            var updatedEntity = await _context.EmployeeRequests
                .Include(e => e.Employee)
                .Include(e => e.LeaveType)
                .Include(e => e.WorkShift)
                .FirstOrDefaultAsync(e => e.VoucherCode == entity.VoucherCode);

            return _mapper.Map<LeaveRequestDTO>(updatedEntity);
        }

        public async Task<LeaveRequestDTO> DeleteLeaveRequestAsync(string voucherCode)
        {
            var entity = await _context.EmployeeRequests
                .FirstOrDefaultAsync(x => x.VoucherCode == voucherCode && (x.RequestType == "Leave" || x.RequestType == "Nghỉ phép"));
            
            if (entity == null) return null;

            _context.EmployeeRequests.Remove(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<LeaveRequestDTO>(entity);
        }

        // Overtime Request specific methods
        public async Task<OvertimeRequestDTO> CreateOvertimeRequestAsync(OvertimeRequestDTOPOST overtimeRequestDTO)
        {
            var entity = _mapper.Map<EmployeeRequests>(overtimeRequestDTO);
            _context.EmployeeRequests.Add(entity);
            await _context.SaveChangesAsync();

            var createdEntity = await _context.EmployeeRequests
                .Include(e => e.Employee)
                .Include(e => e.OvertimeType)
                .Include(e => e.OvertimeForm)
                .FirstOrDefaultAsync(e => e.VoucherCode == entity.VoucherCode);

            return _mapper.Map<OvertimeRequestDTO>(createdEntity);
        }

        public async Task<OvertimeRequestDTO> GetOvertimeRequestByIdAsync(string voucherCode)
        {
            var entity = await _context.EmployeeRequests
                .Include(e => e.Employee)
                .Include(e => e.OvertimeType)
                .Include(e => e.OvertimeForm)
                .FirstOrDefaultAsync(x => x.VoucherCode == voucherCode && x.RequestType == "Tăng ca");
            
            if (entity == null) return null;
            return _mapper.Map<OvertimeRequestDTO>(entity);
        }

        public async Task<IEnumerable<OvertimeRequestDTO>> GetAllOvertimeRequestsAsync()
        {
            var overtimeRequests = await _context.EmployeeRequests
                .Include(e => e.Employee)
                .Include(e => e.OvertimeType)
                .Include(e => e.OvertimeForm)
                .Where(e => e.RequestType == "Tăng ca")
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OvertimeRequestDTO>>(overtimeRequests);
        }

        public async Task<OvertimeRequestDTO> UpdateOvertimeRequestAsync(OvertimeRequestDTOPUT overtimeRequestDTO)
        {
            var entity = await _context.EmployeeRequests
                .FirstOrDefaultAsync(x => x.VoucherCode == overtimeRequestDTO.VoucherCode && x.RequestType == "Tăng ca");
            
            if (entity == null) return null;

            _mapper.Map(overtimeRequestDTO, entity);

            _context.EmployeeRequests.Update(entity);
            await _context.SaveChangesAsync();

            var updatedEntity = await _context.EmployeeRequests
                .Include(e => e.Employee)
                .Include(e => e.OvertimeType)
                .Include(e => e.OvertimeForm)
                .FirstOrDefaultAsync(e => e.VoucherCode == entity.VoucherCode);

            return _mapper.Map<OvertimeRequestDTO>(updatedEntity);
        }

        public async Task<OvertimeRequestDTO> DeleteOvertimeRequestAsync(string voucherCode)
        {
            var entity = await _context.EmployeeRequests
                .FirstOrDefaultAsync(x => x.VoucherCode == voucherCode && x.RequestType == "Tăng ca");
            
            if (entity == null) return null;

            _context.EmployeeRequests.Remove(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<OvertimeRequestDTO>(entity);
        }
    }
}
