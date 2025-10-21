using System;
using System.Collections.Generic;

namespace dotnet_api.DTOs
{
    public class ClosingResultDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public DateTime? ClosedAt { get; set; }
        public List<string> Details { get; set; } = new List<string>();
    }

    public class OvertimeSheetDTO
    {
        public int ID { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public decimal TotalOvertimeDays { get; set; }
        public decimal TotalOvertimeHours { get; set; }
        public decimal OvertimeSalary { get; set; }
        public decimal OvertimeCoefficient { get; set; }
        public DateTime OvertimeClosingDate { get; set; }
        public string OvertimeNotes { get; set; }
        public bool IsClosed { get; set; }
        public DateTime? ClosedAt { get; set; }
        public string ClosedBy { get; set; }
    }

    public class ClosingStatusDTO
    {
        public bool IsTimeSheetClosed { get; set; }
        public bool IsPayrollClosed { get; set; }
        public bool IsOvertimeSheetClosed { get; set; }
        public DateTime? TimeSheetClosedAt { get; set; }
        public DateTime? PayrollClosedAt { get; set; }
        public DateTime? OvertimeSheetClosedAt { get; set; }
        public string TimeSheetClosedBy { get; set; }
        public string PayrollClosedBy { get; set; }
        public string OvertimeSheetClosedBy { get; set; }
    }
}
