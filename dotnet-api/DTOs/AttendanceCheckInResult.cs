using dotnet_api.Data.Enums;

namespace dotnet_api.DTOs
{
    public class AttendanceCheckInResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int AttendanceId { get; set; }
        public string EmployeeId { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public DateTime CheckInDateTime { get; set; }
        public AttendanceStatusEnum Status { get; set; }
        public string? ImagePath { get; set; }
        public string? Location { get; set; }
    }
}



