using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using dotnet_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AttendanceService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AttendanceDTO> CreateAttendanceAsync(AttendanceDTO AttendanceDTO)
        {
            var Attendance = _mapper.Map<Attendance>(AttendanceDTO);
            _context.Attendances.Add(Attendance);
            await _context.SaveChangesAsync();
            return _mapper.Map<AttendanceDTO>(Attendance);
        }

        public async Task<IEnumerable<AttendanceDTOGET>> GetAttendanceByIdAsync(int id)
        {
            var Attendance = await _context.Attendances
                .Include(c => c.Employee)
                .Where(c => c.ConstructionTaskID == id)
                .ToListAsync();

            return Attendance == null ? null : _mapper.Map<IEnumerable<AttendanceDTOGET>>(Attendance);
        }

        public async Task<IEnumerable<AttendanceDTOGET>> GetAllAttendanceAsync()
        {   
            var Attendances = await _context.Attendances
                .Include(c => c.Employee)
                .ToListAsync();

            return _mapper.Map<IEnumerable<AttendanceDTOGET>>(Attendances);
        }

        public async Task<bool> DeleteAttendanceAsync(int id)
        {
            var Attendance = await _context.Attendances.FindAsync(id);
            if (Attendance == null)
            {
                return false;
            }

            _context.Attendances.Remove(Attendance);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAttendanceByEmployeeAndTaskAsync(string employeeId, int taskId)
        {
            var attendances = await _context.Attendances
                .Where(a => a.EmployeeID == employeeId && a.ConstructionTaskID == taskId)
                .ToListAsync();

            if (!attendances.Any())
            {
                return false;
            }

            _context.Attendances.RemoveRange(attendances);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAttendanceStatusByEmployeeAsync(UpdateAttendanceStatusDTO dto)
        {
            var attendances = await _context.Attendances
                .Where(a => a.EmployeeID == dto.EmployeeID && a.AttendanceDate.Date == dto.AttendanceDate.Date)
                .ToListAsync();

            if (!attendances.Any())
            {
                return false;
            }

            foreach (var attendance in attendances)
            {
                attendance.Status = dto.Status;
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
