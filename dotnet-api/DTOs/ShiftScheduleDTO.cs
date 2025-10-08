using System;
using System.Collections.Generic;

namespace dotnet_api.DTOs
{
    public class ShiftScheduleDTO
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public string RoleName { get; set; }
        public List<DayShiftDTO> WeekShifts { get; set; } = new List<DayShiftDTO>();
    }

    public class DayShiftDTO
    {
        public int DayOfWeek { get; set; } // 1 = Monday, 2 = Tuesday, ..., 7 = Sunday
        public DateTime Date { get; set; }
        public List<ShiftAssignmentDetailDTO> Shifts { get; set; } = new List<ShiftAssignmentDetailDTO>();
    }

    public class ShiftAssignmentDetailDTO
    {
        public int WorkShiftID { get; set; }
        public string WorkShiftName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int ShiftAssignmentID { get; set; }
    }
}
