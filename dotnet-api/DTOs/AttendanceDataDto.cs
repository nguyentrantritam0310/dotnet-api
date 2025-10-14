using dotnet_api.Data.Enums;

namespace dotnet_api.DTOs
{
    public class AttendanceDataDto
    {
        public int STT { get; set; }
        public string? ImageCheckIn { get; set; }
        public string? ImageCheckOut { get; set; }
        public string? EmployeeCode { get; set; }
        public string? EmployeeName { get; set; }
        public string? ShiftName { get; set; }
        public DateTime WorkDate { get; set; }
        public TimeSpan? CheckInTime { get; set; }
        public TimeSpan? CheckOutTime { get; set; }
        public string? CheckInOutType { get; set; }
        public string? MachineName { get; set; }
        public string? Location { get; set; }
        public AttendanceStatusEnum? Status { get; set; }
    }
}
