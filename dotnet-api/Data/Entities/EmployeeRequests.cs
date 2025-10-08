using dotnet_api.Data.Enums;

namespace dotnet_api.Data.Entities
{
    public class EmployeeRequests
    {
        public string VoucherCode { get; set; }
        public string RequestType { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Reason { get; set; }
        public ApproveStatusEnum ApproveStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? OvertimeTypeID { get; set; }
        public int? OvertimeFormID { get; set; }
        public int? LeaveTypeID { get; set; }
        public string EmployeeID { get; set; }
    public int? WorkShiftID { get; set; } // FK to WorkShift
        // Navigation properties
        public ApplicationUser Employee { get; set; }
        public OvertimeType OvertimeType { get; set; }
        public OvertimeForm OvertimeForm { get; set; }
        public LeaveType LeaveType { get; set; }
    public WorkShift WorkShift { get; set; }

    }
}
