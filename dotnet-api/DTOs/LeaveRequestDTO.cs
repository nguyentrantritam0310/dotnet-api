using dotnet_api.Data.Enums;

namespace dotnet_api.DTOs
{
    public class LeaveRequestDTO
    {
        public string VoucherCode { get; set; }
        public string EmployeeID { get; set; }
        public string UserName { get; set; }
        public int LeaveTypeID { get; set; }
        public string LeaveTypeName { get; set; }
        public int WorkShiftID { get; set; }
        public string WorkShiftName { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Reason { get; set; }
        public string ApproveStatus { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
