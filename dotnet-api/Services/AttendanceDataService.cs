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
            try
            {
                var attendanceData = await _context.Attendances
                    .Include(a => a.Employee) // Lấy trực tiếp từ Employee
                    .Include(a => a.ShiftAssignment)
                        .ThenInclude(sa => sa.WorkShift)
                    .Include(a => a.AttendanceMachine) // Lấy thông tin máy chấm công
                    .Where(a => a.Employee != null) // Chỉ lấy records có Employee
                    .OrderBy(a => a.CheckInDateTime ?? a.CreatedDate)
                    .Select(a => new AttendanceDataDto
                    {
                        ID = a.ID,
                        EmployeeName = $"{a.Employee.FirstName} {a.Employee.LastName}",
                        EmployeeCode = a.Employee.Id,
                        ShiftName = a.ShiftAssignment != null && a.ShiftAssignment.WorkShift != null ? 
                                   a.ShiftAssignment.WorkShift.ShiftName : "Chưa phân ca",
                        WorkDate = a.CheckInDateTime.HasValue ? a.CheckInDateTime.Value.Date : a.CreatedDate.Date,
                        CheckInTime = a.CheckIn,
                        CheckOutTime = a.CheckOut,
                        Status = a.Status,
                        RefCode = null, // Không set RefCode cho attendance, chỉ có cho nghỉ phép
                        WorkShiftID = a.ShiftAssignment != null ? a.ShiftAssignment.WorkShiftID : null,
                        MachineName = a.AttendanceMachine != null ? a.AttendanceMachine.AttendanceMachineName : null, // Lấy tên máy từ AttendanceMachine
                        Location = a.CheckInLocation ?? a.CheckOutLocation // Lấy location từ CheckInLocation hoặc CheckOutLocation (có thể là coordinates)
                    })
                    .ToListAsync();

                // Process data after query
                foreach (var item in attendanceData)
                {
                    item.CheckInOutType = GetCheckInOutType(item.CheckInTime, item.CheckOutTime);
                    // MachineName và Location đã được set trong Select từ AttendanceMachine và CheckInLocation/CheckOutLocation
                    // Chỉ cần fallback nếu chưa có
                    if (string.IsNullOrEmpty(item.MachineName))
                    {
                        item.MachineName = "Máy chấm công di động";
                    }
                    if (string.IsNullOrEmpty(item.Location))
                    {
                        item.Location = "Vị trí di động";
                    }
                }

                // Add STT
                for (int i = 0; i < attendanceData.Count; i++)
                {
                    attendanceData[i].STT = i + 1;
                }

                return attendanceData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllAttendanceDataAsync: {ex.Message}");
                return new List<AttendanceDataDto>();
            }
        }

        public async Task<List<AttendanceDataDto>> GetAttendanceDataByEmployeeAsync(string employeeCode)
        {
            try
            {
                var attendanceData = await _context.Attendances
                    .Include(a => a.Employee)
                    .Include(a => a.ShiftAssignment)
                        .ThenInclude(sa => sa.WorkShift)
                    .Include(a => a.AttendanceMachine) // Lấy thông tin máy chấm công
                    .Where(a => a.Employee != null && a.Employee.Id == employeeCode)
                    .OrderBy(a => a.CheckInDateTime ?? a.CreatedDate)
                    .Select(a => new AttendanceDataDto
                    {
                        ID = a.ID,
                        EmployeeName = $"{a.Employee.FirstName} {a.Employee.LastName}",
                        EmployeeCode = a.Employee.Id,
                        ShiftName = a.ShiftAssignment != null && a.ShiftAssignment.WorkShift != null ? 
                                   a.ShiftAssignment.WorkShift.ShiftName : "Chưa phân ca",
                        WorkDate = a.CheckInDateTime.HasValue ? a.CheckInDateTime.Value.Date : a.CreatedDate.Date,
                        CheckInTime = a.CheckIn,
                        CheckOutTime = a.CheckOut,
                        Status = a.Status,
                        RefCode = null, // Không set RefCode cho attendance, chỉ có cho nghỉ phép
                        WorkShiftID = a.ShiftAssignment != null ? a.ShiftAssignment.WorkShiftID : null,
                        MachineName = a.AttendanceMachine != null ? a.AttendanceMachine.AttendanceMachineName : null, // Lấy tên máy từ AttendanceMachine
                        Location = a.CheckInLocation ?? a.CheckOutLocation // Lấy location từ CheckInLocation hoặc CheckOutLocation
                    })
                    .ToListAsync();

                // Process data after query
                foreach (var item in attendanceData)
                {
                    item.CheckInOutType = GetCheckInOutType(item.CheckInTime, item.CheckOutTime);
                    // MachineName và Location đã được set trong Select từ AttendanceMachine và CheckInLocation/CheckOutLocation
                    // Chỉ cần fallback nếu chưa có
                    if (string.IsNullOrEmpty(item.MachineName))
                    {
                        item.MachineName = "Máy chấm công di động";
                    }
                    if (string.IsNullOrEmpty(item.Location))
                    {
                        item.Location = "Vị trí di động";
                    }
                }

                // Add STT
                for (int i = 0; i < attendanceData.Count; i++)
                {
                    attendanceData[i].STT = i + 1;
                }

                return attendanceData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAttendanceDataByEmployeeAsync: {ex.Message}");
                return new List<AttendanceDataDto>();
            }
        }

        public async Task<List<AttendanceDataDto>> GetAttendanceDataByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var attendanceData = await _context.Attendances
                    .Include(a => a.Employee)
                    .Include(a => a.ShiftAssignment)
                        .ThenInclude(sa => sa.WorkShift)
                    .Include(a => a.AttendanceMachine) // Lấy thông tin máy chấm công
                    .Where(a => a.Employee != null && 
                               (a.CheckInDateTime != null ? a.CheckInDateTime.Value.Date >= startDate.Date && a.CheckInDateTime.Value.Date <= endDate.Date :
                                a.CreatedDate.Date >= startDate.Date && a.CreatedDate.Date <= endDate.Date))
                    .OrderBy(a => a.CheckInDateTime ?? a.CreatedDate)
                    .Select(a => new AttendanceDataDto
                    {
                        ID = a.ID,
                        EmployeeName = $"{a.Employee.FirstName} {a.Employee.LastName}",
                        EmployeeCode = a.Employee.Id,
                        ShiftName = a.ShiftAssignment != null && a.ShiftAssignment.WorkShift != null ? 
                                   a.ShiftAssignment.WorkShift.ShiftName : "Chưa phân ca",
                        WorkDate = a.CheckInDateTime.HasValue ? a.CheckInDateTime.Value.Date : a.CreatedDate.Date,
                        CheckInTime = a.CheckIn,
                        CheckOutTime = a.CheckOut,
                        Status = a.Status,
                        RefCode = null, // Không set RefCode cho attendance, chỉ có cho nghỉ phép
                        WorkShiftID = a.ShiftAssignment != null ? a.ShiftAssignment.WorkShiftID : null,
                        MachineName = a.AttendanceMachine != null ? a.AttendanceMachine.AttendanceMachineName : null, // Lấy tên máy từ AttendanceMachine
                        Location = a.CheckInLocation ?? a.CheckOutLocation // Lấy location từ CheckInLocation hoặc CheckOutLocation
                    })
                    .ToListAsync();

                // Process data after query
                foreach (var item in attendanceData)
                {
                    item.CheckInOutType = GetCheckInOutType(item.CheckInTime, item.CheckOutTime);
                    // MachineName và Location đã được set trong Select từ AttendanceMachine và CheckInLocation/CheckOutLocation
                    // Chỉ cần fallback nếu chưa có
                    if (string.IsNullOrEmpty(item.MachineName))
                    {
                        item.MachineName = "Máy chấm công di động";
                    }
                    if (string.IsNullOrEmpty(item.Location))
                    {
                        item.Location = "Vị trí di động";
                    }
                }

                // Add STT
                for (int i = 0; i < attendanceData.Count; i++)
                {
                    attendanceData[i].STT = i + 1;
                }

                return attendanceData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAttendanceDataByDateRangeAsync: {ex.Message}");
                return new List<AttendanceDataDto>();
            }
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

        private string GetMachineNameFromAttendance(AttendanceDataDto item)
        {
            // Nếu đã có MachineName từ CheckInLocation, dùng nó
            if (!string.IsNullOrEmpty(item.MachineName))
            {
                return item.MachineName;
            }
            
            // Fallback: nếu không có, dùng WorkShiftID
            if (item.WorkShiftID.HasValue)
            {
                return $"Máy chấm công {item.WorkShiftID.Value}";
            }
            
            return "Máy chấm công di động";
        }

        private string GetLocationFromAttendance(AttendanceDataDto item)
        {
            // Có thể lấy từ CheckInLocation/CheckOutLocation trong tương lai
            // Hiện tại dựa trên WorkShiftID
            if (item.WorkShiftID.HasValue)
            {
                return $"Công trường {item.WorkShiftID.Value}";
            }
            
            return "Vị trí di động";
        }

        public async Task<List<AttendanceDataDto>> GetAttendanceDataByEmployeeAndDateAsync(string employeeCode, DateTime date)
        {
            try
            {
                var attendanceData = await _context.Attendances
                    .Include(a => a.Employee)
                    .Include(a => a.ShiftAssignment)
                        .ThenInclude(sa => sa.WorkShift)
                    .Include(a => a.AttendanceMachine) // Lấy thông tin máy chấm công
                    .Where(a => a.Employee != null && a.Employee.Id == employeeCode && 
                               (a.CheckInDateTime.HasValue ? a.CheckInDateTime.Value.Date == date.Date :
                                a.CreatedDate.Date == date.Date))
                    .OrderBy(a => a.CheckInDateTime ?? a.CreatedDate)
                    .Select(a => new AttendanceDataDto
                    {
                        ID = a.ID,
                        EmployeeName = $"{a.Employee.FirstName} {a.Employee.LastName}",
                        EmployeeCode = a.Employee.Id,
                        ShiftName = a.ShiftAssignment != null && a.ShiftAssignment.WorkShift != null ? 
                                   a.ShiftAssignment.WorkShift.ShiftName : "Chưa phân ca",
                        WorkDate = a.ShiftAssignment != null ? a.ShiftAssignment.WorkDate : 
                                  (a.CheckInDateTime.HasValue ? a.CheckInDateTime.Value.Date : a.CreatedDate.Date),
                        CheckInTime = a.CheckIn,
                        CheckOutTime = a.CheckOut,
                        Status = a.Status,
                        RefCode = null, // Không set RefCode cho attendance, chỉ có cho nghỉ phép
                        WorkShiftID = a.ShiftAssignment != null ? a.ShiftAssignment.WorkShiftID : null,
                        MachineName = a.AttendanceMachine != null ? a.AttendanceMachine.AttendanceMachineName : null, // Lấy tên máy từ AttendanceMachine
                        Location = a.CheckInLocation ?? a.CheckOutLocation // Lấy location từ CheckInLocation hoặc CheckOutLocation
                    })
                    .ToListAsync();

                // Process data after query
                foreach (var item in attendanceData)
                {
                    item.CheckInOutType = GetCheckInOutType(item.CheckInTime, item.CheckOutTime);
                    // MachineName và Location đã được set trong Select từ AttendanceMachine và CheckInLocation/CheckOutLocation
                    // Chỉ cần fallback nếu chưa có
                    if (string.IsNullOrEmpty(item.MachineName))
                    {
                        item.MachineName = "Máy chấm công di động";
                    }
                    if (string.IsNullOrEmpty(item.Location))
                    {
                        item.Location = "Vị trí di động";
                    }
                }

                // Add STT
                for (int i = 0; i < attendanceData.Count; i++)
                {
                    attendanceData[i].STT = i + 1;
                }

                return attendanceData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAttendanceDataByEmployeeAndDateAsync: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw; // Re-throw để controller có thể trả về 500 error
            }
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
                .Include(a => a.AttendanceMachine) // Lấy thông tin máy chấm công
                .Where(a => a.ShiftAssignment.WorkDate >= startDate && a.ShiftAssignment.WorkDate <= endDate)
                .OrderBy(a => a.ShiftAssignment.WorkDate)
                .Select(a => new AttendanceDataDto
                {
                    ID = a.ID,
                    EmployeeName = $"{a.ShiftAssignment.Employee.FirstName} {a.ShiftAssignment.Employee.LastName}",
                    EmployeeCode = a.ShiftAssignment.Employee.Id,
                    ShiftName = a.ShiftAssignment.WorkShift.ShiftName,
                    WorkDate = a.ShiftAssignment.WorkDate,
                    CheckInTime = a.CheckIn,
                    CheckOutTime = a.CheckOut,
                    Status = a.Status,
                    RefCode = null, // Không set RefCode cho attendance, chỉ có cho nghỉ phép
                    WorkShiftID = a.ShiftAssignment.WorkShiftID,
                    MachineName = a.AttendanceMachine != null ? a.AttendanceMachine.AttendanceMachineName : null, // Lấy tên máy từ AttendanceMachine
                    Location = a.CheckInLocation ?? a.CheckOutLocation // Lấy location từ CheckInLocation hoặc CheckOutLocation
                })
                .ToListAsync();

            // Process data after query
            foreach (var item in attendanceData)
            {
                item.CheckInOutType = GetCheckInOutType(item.CheckInTime, item.CheckOutTime);
                    item.MachineName = GetMachineNameFromAttendance(item);
                    item.Location = GetLocationFromAttendance(item);
            }

            // Add STT
            for (int i = 0; i < attendanceData.Count; i++)
            {
                attendanceData[i].STT = i + 1;
            }

            return attendanceData;
        }

        public async Task<List<AttendanceDataDto>> GetAttendanceDataByMonthAsync(int year, int month)
        {
            try
            {
                var startDate = new DateTime(year, month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                
                return await GetAttendanceDataByDateRangeAsync(startDate, endDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAttendanceDataByMonthAsync: {ex.Message}");
                return new List<AttendanceDataDto>();
            }
        }

        public async Task<object> GetDebugInfoAsync()
        {
            try
            {
                return new
                {
                    Message = "Debug endpoint working",
                    Timestamp = DateTime.Now
                };
            }
            catch (Exception ex)
            {
                return new { Error = ex.Message, StackTrace = ex.StackTrace };
            }
        }
    }
}
