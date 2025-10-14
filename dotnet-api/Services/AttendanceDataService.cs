using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Services
{
    public class AttendanceDataService
    {
        private readonly ApplicationDbContext _context;

        public AttendanceDataService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AttendanceDataDto>> GetAllAttendanceDataAsync()
        {
            var attendanceData = await _context.Attendances
                .Include(a => a.ShiftAssignment)
                .ThenInclude(sa => sa.Employee)
                .Include(a => a.ShiftAssignment)
                .ThenInclude(sa => sa.WorkShift)
                .OrderBy(a => a.ShiftAssignment.WorkDate)
                .Select(a => new AttendanceDataDto
                {
                    ImageCheckIn = a.ImageCheckIn,
                    ImageCheckOut = a.ImageCheckOut,
                    EmployeeName = $"{a.ShiftAssignment.Employee.FirstName} {a.ShiftAssignment.Employee.LastName}",
                    ShiftName = a.ShiftAssignment.WorkShift.ShiftName,
                    WorkDate = a.ShiftAssignment.WorkDate,
                    CheckInTime = a.CheckIn,
                    CheckOutTime = a.CheckOut,
                    Status = a.Status
                })
                .ToListAsync();

            // Process data after query
            foreach (var item in attendanceData)
            {
                item.CheckInOutType = GetCheckInOutType(item.CheckInTime, item.CheckOutTime);
                item.MachineName = GetMachineName(item.ImageCheckIn, item.ImageCheckOut);
                item.Location = GetLocation(item.ImageCheckIn, item.ImageCheckOut);
            }

            // Add STT
            for (int i = 0; i < attendanceData.Count; i++)
            {
                attendanceData[i].STT = i + 1;
            }

            return attendanceData;
        }

        public async Task<List<AttendanceDataDto>> GetAttendanceDataByEmployeeAsync(string employeeCode)
        {
            var attendanceData = await _context.Attendances
                .Include(a => a.ShiftAssignment)
                .ThenInclude(sa => sa.Employee)
                .Include(a => a.ShiftAssignment)
                .ThenInclude(sa => sa.WorkShift)
                .Where(a => a.ShiftAssignment.Employee.Id == employeeCode)
                .OrderBy(a => a.ShiftAssignment.WorkDate)
                .Select(a => new AttendanceDataDto
                {
                    ImageCheckIn = a.ImageCheckIn,
                    ImageCheckOut = a.ImageCheckOut,
                    EmployeeName = $"{a.ShiftAssignment.Employee.FirstName} {a.ShiftAssignment.Employee.LastName}",
                    ShiftName = a.ShiftAssignment.WorkShift.ShiftName,
                    WorkDate = a.ShiftAssignment.WorkDate,
                    CheckInTime = a.CheckIn,
                    CheckOutTime = a.CheckOut,
                    Status = a.Status
                })
                .ToListAsync();

            // Process data after query
            foreach (var item in attendanceData)
            {
                item.CheckInOutType = GetCheckInOutType(item.CheckInTime, item.CheckOutTime);
                item.MachineName = GetMachineName(item.ImageCheckIn, item.ImageCheckOut);
                item.Location = GetLocation(item.ImageCheckIn, item.ImageCheckOut);
            }

            // Add STT
            for (int i = 0; i < attendanceData.Count; i++)
            {
                attendanceData[i].STT = i + 1;
            }

            return attendanceData;
        }

        public async Task<List<AttendanceDataDto>> GetAttendanceDataByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var attendanceData = await _context.Attendances
                .Include(a => a.ShiftAssignment)
                .ThenInclude(sa => sa.Employee)
                .Include(a => a.ShiftAssignment)
                .ThenInclude(sa => sa.WorkShift)
                .Where(a => a.ShiftAssignment.WorkDate >= startDate && a.ShiftAssignment.WorkDate <= endDate)
                .OrderBy(a => a.ShiftAssignment.WorkDate)
                .Select(a => new AttendanceDataDto
                {
                    ImageCheckIn = a.ImageCheckIn,
                    ImageCheckOut = a.ImageCheckOut,
                    EmployeeName = $"{a.ShiftAssignment.Employee.FirstName} {a.ShiftAssignment.Employee.LastName}",
                    ShiftName = a.ShiftAssignment.WorkShift.ShiftName,
                    WorkDate = a.ShiftAssignment.WorkDate,
                    CheckInTime = a.CheckIn,
                    CheckOutTime = a.CheckOut,
                    Status = a.Status
                })
                .ToListAsync();

            // Process data after query
            foreach (var item in attendanceData)
            {
                item.CheckInOutType = GetCheckInOutType(item.CheckInTime, item.CheckOutTime);
                item.MachineName = GetMachineName(item.ImageCheckIn, item.ImageCheckOut);
                item.Location = GetLocation(item.ImageCheckIn, item.ImageCheckOut);
            }

            // Add STT
            for (int i = 0; i < attendanceData.Count; i++)
            {
                attendanceData[i].STT = i + 1;
            }

            return attendanceData;
        }

        private string GetCheckInOutType(TimeSpan? checkIn, TimeSpan? checkOut)
        {
            if (checkIn.HasValue && checkOut.HasValue)
                return "Vào/Ra";
            else if (checkIn.HasValue)
                return "Vào";
            else if (checkOut.HasValue)
                return "Ra";
            else
                return "Chưa quét";
        }

        private string GetMachineName(string? imageCheckIn, string? imageCheckOut)
        {
            // Logic to extract machine name from image path or use default
            if (!string.IsNullOrEmpty(imageCheckIn) || !string.IsNullOrEmpty(imageCheckOut))
            {
                return "Máy quét 01"; // Default machine name
            }
            return "Chưa xác định";
        }

        private string GetLocation(string? imageCheckIn, string? imageCheckOut)
        {
            // Logic to determine location based on image or other criteria
            if (!string.IsNullOrEmpty(imageCheckIn) || !string.IsNullOrEmpty(imageCheckOut))
            {
                return "Văn phòng Hà Nội"; // Default location
            }
            return "Chưa xác định";
        }
    }
}
