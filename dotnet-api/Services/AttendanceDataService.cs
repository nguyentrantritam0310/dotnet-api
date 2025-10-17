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

        public async Task<List<AttendanceDataDto>> GetAttendanceDataByEmployeeAndDateAsync(string employeeCode, DateTime date)
        {
            var attendanceData = await _context.Attendances
                .Include(a => a.ShiftAssignment)
                .ThenInclude(sa => sa.Employee)
                .Include(a => a.ShiftAssignment)
                .ThenInclude(sa => sa.WorkShift)
                .Where(a => a.ShiftAssignment.Employee.Id == employeeCode && 
                           a.ShiftAssignment.WorkDate.Date == date.Date)
                .OrderBy(a => a.ShiftAssignment.WorkDate)
                .Select(a => new AttendanceDataDto
                {
                    ImageCheckIn = a.ImageCheckIn,
                    ImageCheckOut = a.ImageCheckOut,
                    EmployeeName = $"{a.ShiftAssignment.Employee.FirstName} {a.ShiftAssignment.Employee.LastName}",
                    EmployeeCode = a.ShiftAssignment.Employee.Id,
                    ShiftName = a.ShiftAssignment.WorkShift.ShiftName,
                    WorkDate = a.ShiftAssignment.WorkDate,
                    CheckInTime = a.CheckIn,
                    CheckOutTime = a.CheckOut,
                    Status = a.Status,
                    RefCode = $"MP{a.ID ?? 0:D3}",
                    WorkShiftID = a.ShiftAssignment.WorkShiftID
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

        public async Task<List<AttendanceDataDto>> GetAttendanceDataByWeekAsync(int year, int weekNumber)
        {
            // Calculate the start date of the week
            var jan1 = new DateTime(year, 1, 1);
            var daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;
            var firstMonday = jan1.AddDays(daysOffset);
            var startDate = firstMonday.AddDays((weekNumber - 1) * 7);
            var endDate = startDate.AddDays(6);

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
                    EmployeeCode = a.ShiftAssignment.Employee.Id,
                    ShiftName = a.ShiftAssignment.WorkShift.ShiftName,
                    WorkDate = a.ShiftAssignment.WorkDate,
                    CheckInTime = a.CheckIn,
                    CheckOutTime = a.CheckOut,
                    Status = a.Status,
                    RefCode = $"MP{a.ID ?? 0:D3}",
                    WorkShiftID = a.ShiftAssignment.WorkShiftID
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

        public async Task<object> GetDebugInfoAsync()
        {
            try
            {
                // Count total records in each table
                var totalAttendances = await _context.Attendances.CountAsync();
                var totalShiftAssignments = await _context.ShiftAssignments.CountAsync();
                var totalEmployees = await _context.ApplicationUsers.CountAsync();
                var totalWorkShifts = await _context.WorkShifts.CountAsync();

                // Sample data
                var sampleAttendances = await _context.Attendances
                    .Include(a => a.ShiftAssignment)
                    .ThenInclude(sa => sa.Employee)
                    .Include(a => a.ShiftAssignment)
                    .ThenInclude(sa => sa.WorkShift)
                    .Take(5)
                    .Select(a => new
                    {
                        AttendanceId = a.ID,
                        EmployeeId = a.ShiftAssignment.Employee.Id,
                        EmployeeName = $"{a.ShiftAssignment.Employee.FirstName} {a.ShiftAssignment.Employee.LastName}",
                        WorkDate = a.ShiftAssignment.WorkDate,
                        CheckIn = a.CheckIn,
                        CheckOut = a.CheckOut,
                        ShiftName = a.ShiftAssignment.WorkShift.ShiftName
                    })
                    .ToListAsync();

                var sampleEmployees = await _context.ApplicationUsers
                    .Take(5)
                    .Select(e => new
                    {
                        Id = e.Id,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        EmployeeName = $"{e.FirstName} {e.LastName}"
                    })
                    .ToListAsync();

                var sampleShiftAssignments = await _context.ShiftAssignments
                    .Include(sa => sa.Employee)
                    .Include(sa => sa.WorkShift)
                    .Take(5)
                    .Select(sa => new
                    {
                        Id = sa.ID,
                        EmployeeId = sa.Employee.Id,
                        EmployeeName = $"{sa.Employee.FirstName} {sa.Employee.LastName}",
                        WorkDate = sa.WorkDate,
                        ShiftName = sa.WorkShift.ShiftName
                    })
                    .ToListAsync();

                return new
                {
                    TotalRecords = new
                    {
                        Attendances = totalAttendances,
                        ShiftAssignments = totalShiftAssignments,
                        Employees = totalEmployees,
                        WorkShifts = totalWorkShifts
                    },
                    SampleAttendances = sampleAttendances,
                    SampleEmployees = sampleEmployees,
                    SampleShiftAssignments = sampleShiftAssignments
                };
            }
            catch (Exception ex)
            {
                return new { Error = ex.Message, StackTrace = ex.StackTrace };
            }
        }
    }
}
