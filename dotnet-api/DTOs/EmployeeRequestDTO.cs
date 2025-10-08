using dotnet_api.Data.Enums;

namespace dotnet_api.DTOs
{
    public class EmployeeRequestDTO
    {
        public string VoucherCode { get; set; }
        public string RequestType { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Reason { get; set; }
        public string ApproveStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public string EmployeeID { get; set; }
        public string UserName { get; set; }
        public int? LeaveTypeID { get; set; }
        public string LeaveTypeName { get; set; }
        public int OvertimeTypeID { get; set; }
        public string OvertimeTypeName { get; set; }
        public float coefficient { get; set; }
        public int OvertimeFormID { get; set; }
        public string OvertimeFormName { get; set; }
        public int WorkShiftID { get; set; }
    }
}
