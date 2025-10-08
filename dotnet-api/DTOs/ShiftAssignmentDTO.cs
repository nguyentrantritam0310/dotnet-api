using System;

namespace dotnet_api.DTOs
{
    public class ShiftAssignmentDTO
    {
        public int ID { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int WorkShiftID { get; set; }
        public string WorkShiftName { get; set; }
        public DateTime WorkDate { get; set; }
        public string WorkDateFormatted { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}

