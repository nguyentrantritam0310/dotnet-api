using dotnet_api.Data.Enums;

namespace dotnet_api.Data.Entities
{
    public class Attendance
    {
        public int? ID { get; set; }
        public int? ShiftAssignmentID { get; set; }
        public TimeSpan? CheckIn { get; set; }
        public TimeSpan? CheckOut { get; set; }
        public string? ImageCheckIn { get; set; }
        public string? ImageCheckOut { get; set; }
        public AttendanceStatusEnum? Status { get; set; } 

        // Navigation properties
        public ShiftAssignment ShiftAssignment { get; set; }
    }
}
