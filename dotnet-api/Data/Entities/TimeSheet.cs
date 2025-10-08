using System;

namespace dotnet_api.Data.Entities
{
    public class TimeSheet
    {
        public int ID { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public decimal TotalStandardWorkdays { get; set; }
        public decimal TotalUnpaidLeave { get; set; }
        public decimal TotalPaidLeave { get; set; }
        public decimal TotalWorkdays { get; set; }
        public decimal CompensatedOvertime { get; set; }
        public decimal PayableOvertime { get; set; }
        public decimal TotalActualWorkdays { get; set; }
        public int LateArrivalCount { get; set; }
        public int EarlyLeaveCount { get; set; }
        public int UnexcusedAbsenceCount { get; set; }
        public DateTime TimeSheetClosingDate { get; set; }
        public string TimeSheetNotes { get; set; }

        // 1-1 với Payroll
        public Payroll Payroll { get; set; }
        public ApplicationUser Employee { get; set; }
    }
}
