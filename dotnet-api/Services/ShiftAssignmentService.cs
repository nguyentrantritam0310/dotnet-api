using AutoMapper;
using dotnet_api.Data;
using dotnet_api.Data.Entities;
using dotnet_api.DTOs;
using dotnet_api.DTOs.POST;
using dotnet_api.DTOs.PUT;
using dotnet_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Services
{
    public class ShiftAssignmentService : IShiftAssignmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ShiftAssignmentService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShiftAssignmentDTO>> GetAllShiftAssignmentsAsync()
        {
            var shiftAssignments = await _context.ShiftAssignments
                .Include(sa => sa.Employee)
                .Include(sa => sa.WorkShift)
                    .ThenInclude(ws => ws.ShiftDetails)
                .OrderBy(sa => sa.WorkDate)
                .ThenBy(sa => sa.Employee.FirstName)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ShiftAssignmentDTO>>(shiftAssignments);
        }

        public async Task<ShiftAssignmentDTO> GetShiftAssignmentByIdAsync(int id)
        {
            var shiftAssignment = await _context.ShiftAssignments
                .Include(sa => sa.Employee)
                .Include(sa => sa.WorkShift)
                    .ThenInclude(ws => ws.ShiftDetails)
                .FirstOrDefaultAsync(sa => sa.ID == id);

            return shiftAssignment == null ? null : _mapper.Map<ShiftAssignmentDTO>(shiftAssignment);
        }

        public async Task<IEnumerable<ShiftAssignmentDTO>> GetShiftAssignmentsByEmployeeIdAsync(string employeeId)
        {
            var shiftAssignments = await _context.ShiftAssignments
                .Include(sa => sa.Employee)
                .Include(sa => sa.WorkShift)
                    .ThenInclude(ws => ws.ShiftDetails)
                .Where(sa => sa.EmployeeID == employeeId)
                .OrderBy(sa => sa.WorkDate)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ShiftAssignmentDTO>>(shiftAssignments);
        }

        public async Task<IEnumerable<ShiftAssignmentDTO>> GetShiftAssignmentsByDateAsync(DateTime date)
        {
            var shiftAssignments = await _context.ShiftAssignments
                .Include(sa => sa.Employee)
                .Include(sa => sa.WorkShift)
                    .ThenInclude(ws => ws.ShiftDetails)
                .Where(sa => sa.WorkDate.Date == date.Date)
                .OrderBy(sa => sa.Employee.FirstName)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ShiftAssignmentDTO>>(shiftAssignments);
        }

        public async Task<IEnumerable<ShiftAssignmentDTO>> GetShiftAssignmentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var shiftAssignments = await _context.ShiftAssignments
                .Include(sa => sa.Employee)
                .Include(sa => sa.WorkShift)
                    .ThenInclude(ws => ws.ShiftDetails)
                .Where(sa => sa.WorkDate.Date >= startDate.Date && sa.WorkDate.Date <= endDate.Date)
                .OrderBy(sa => sa.WorkDate)
                .ThenBy(sa => sa.Employee.FirstName)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ShiftAssignmentDTO>>(shiftAssignments);
        }

        public async Task<IEnumerable<ShiftScheduleDTO>> GetShiftScheduleByWeekAsync(DateTime weekStartDate)
        {
            // Calculate week end date (7 days from start date)
            var weekEndDate = weekStartDate.AddDays(6);

            // Get all shift assignments for the week
            var shiftAssignments = await _context.ShiftAssignments
                .Include(sa => sa.Employee)
                .Include(sa => sa.WorkShift)
                    .ThenInclude(ws => ws.ShiftDetails)
                .Where(sa => sa.WorkDate.Date >= weekStartDate.Date && sa.WorkDate.Date <= weekEndDate.Date)
                .OrderBy(sa => sa.Employee.FirstName)
                .ThenBy(sa => sa.WorkDate)
                .ToListAsync();

            // Get all employees to ensure we have all employees in the result
            var allEmployees = await _context.ApplicationUsers
                .Where(e => e.Status == dotnet_api.Data.Enums.EmployeeStatusEnum.Active)
                .OrderBy(e => e.FirstName)
                .ToListAsync();

            var result = new List<ShiftScheduleDTO>();

            foreach (var employee in allEmployees)
            {
                var employeeSchedule = new ShiftScheduleDTO
                {
                    EmployeeID = employee.Id,
                    EmployeeName = employee.FirstName + " " + employee.LastName,
                    EmployeeCode = employee.EmployeeCode,
                    RoleName = employee.Role?.RoleName ?? "",
                    WeekShifts = new List<DayShiftDTO>()
                };

                // Create day shifts for the week (Monday = 1, Sunday = 7)
                for (int dayOffset = 0; dayOffset < 7; dayOffset++)
                {
                    var currentDate = weekStartDate.AddDays(dayOffset);
                    var dayOfWeek = (int)currentDate.DayOfWeek;
                    if (dayOfWeek == 0) dayOfWeek = 7; // Convert Sunday from 0 to 7

                    var dayShift = new DayShiftDTO
                    {
                        DayOfWeek = dayOfWeek,
                        Date = currentDate,
                        Shifts = new List<ShiftAssignmentDetailDTO>()
                    };

                    // Find shift assignments for this employee on this date
                    var employeeShifts = shiftAssignments
                        .Where(sa => sa.EmployeeID == employee.Id && sa.WorkDate.Date == currentDate.Date)
                        .ToList();

                    foreach (var shiftAssignment in employeeShifts)
                    {
                        foreach (var shiftDetail in shiftAssignment.WorkShift.ShiftDetails)
                        {
                            dayShift.Shifts.Add(new ShiftAssignmentDetailDTO
                            {
                                WorkShiftID = shiftAssignment.WorkShiftID,
                                WorkShiftName = shiftAssignment.WorkShift.ShiftName,
                                StartTime = shiftDetail.StartTime.ToString(@"hh\:mm"),
                                EndTime = shiftDetail.EndTime.ToString(@"hh\:mm"),
                                ShiftAssignmentID = shiftAssignment.ID
                            });
                        }
                    }

                    employeeSchedule.WeekShifts.Add(dayShift);
                }

                result.Add(employeeSchedule);
            }

            return result;
        }

        public async Task<ShiftAssignmentDTO> CreateShiftAssignmentAsync(ShiftAssignmentDTOPOST shiftAssignmentDTO)
        {
            // Check if employee exists
            var employee = await _context.ApplicationUsers.FindAsync(shiftAssignmentDTO.EmployeeID);
            if (employee == null)
            {
                throw new Exception("Không tìm thấy nhân viên với ID đã cho");
            }

            // Check if work shift exists
            var workShift = await _context.WorkShifts.FindAsync(shiftAssignmentDTO.WorkShiftID);
            if (workShift == null)
            {
                throw new Exception("Không tìm thấy ca làm việc với ID đã cho");
            }

            // Check if shift assignment already exists for this employee on this date
            var existingAssignment = await _context.ShiftAssignments
                .FirstOrDefaultAsync(sa => sa.EmployeeID == shiftAssignmentDTO.EmployeeID && 
                                         sa.WorkDate.Date == shiftAssignmentDTO.WorkDate.Date);
            if (existingAssignment != null)
            {
                throw new Exception("Nhân viên đã được phân ca cho ngày này");
            }

            var shiftAssignment = _mapper.Map<ShiftAssignment>(shiftAssignmentDTO);
            _context.ShiftAssignments.Add(shiftAssignment);
            await _context.SaveChangesAsync();

            return await GetShiftAssignmentByIdAsync(shiftAssignment.ID);
        }

        public async Task<ShiftAssignmentDTO> UpdateShiftAssignmentAsync(ShiftAssignmentDTOPUT shiftAssignmentDTO)
        {
            var shiftAssignment = await _context.ShiftAssignments.FindAsync(shiftAssignmentDTO.ID);
            if (shiftAssignment == null)
            {
                throw new Exception("Không tìm thấy phân ca với ID đã cho");
            }

            // Check if employee exists
            var employee = await _context.ApplicationUsers.FindAsync(shiftAssignmentDTO.EmployeeID);
            if (employee == null)
            {
                throw new Exception("Không tìm thấy nhân viên với ID đã cho");
            }

            // Check if work shift exists
            var workShift = await _context.WorkShifts.FindAsync(shiftAssignmentDTO.WorkShiftID);
            if (workShift == null)
            {
                throw new Exception("Không tìm thấy ca làm việc với ID đã cho");
            }

            // Check if shift assignment already exists for this employee on this date (excluding current assignment)
            var existingAssignment = await _context.ShiftAssignments
                .FirstOrDefaultAsync(sa => sa.EmployeeID == shiftAssignmentDTO.EmployeeID && 
                                         sa.WorkDate.Date == shiftAssignmentDTO.WorkDate.Date &&
                                         sa.ID != shiftAssignmentDTO.ID);
            if (existingAssignment != null)
            {
                throw new Exception("Nhân viên đã được phân ca cho ngày này");
            }

            shiftAssignment.EmployeeID = shiftAssignmentDTO.EmployeeID;
            shiftAssignment.WorkShiftID = shiftAssignmentDTO.WorkShiftID;
            shiftAssignment.WorkDate = shiftAssignmentDTO.WorkDate;

            await _context.SaveChangesAsync();

            return await GetShiftAssignmentByIdAsync(shiftAssignment.ID);
        }

        public async Task<bool> DeleteShiftAssignmentAsync(int id)
        {
            var shiftAssignment = await _context.ShiftAssignments.FindAsync(id);
            if (shiftAssignment == null)
            {
                throw new Exception("Không tìm thấy phân ca với ID đã cho");
            }

            _context.ShiftAssignments.Remove(shiftAssignment);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
