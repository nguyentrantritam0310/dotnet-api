using dotnet_api.Data.Enums;

namespace dotnet_api.Data.Entities
{
    public class Attendance
    {
        public int? ID { get; set; }
        public int? ShiftAssignmentID { get; set; }
        public string? EmployeeId { get; set; } // Foreign key to ApplicationUser
        public DateTime? CheckInDateTime { get; set; } // Full datetime for check-in
        public DateTime? CheckOutDateTime { get; set; } // Full datetime for check-out
        public TimeSpan? CheckIn { get; set; } // Keep for compatibility
        public TimeSpan? CheckOut { get; set; } // Keep for compatibility
        public string? ImageCheckIn { get; set; } // Path to check-in image
        public string? ImageCheckOut { get; set; } // Path to check-out image
        public string? CheckInLocation { get; set; } // GPS coordinates or location name
        public string? CheckOutLocation { get; set; } // GPS coordinates or location name
        public int? AttendanceMachineId { get; set; } // Which machine was used
        public AttendanceStatusEnum? Status { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public string? Notes { get; set; } // Additional notes

        // Navigation properties
        public ShiftAssignment ShiftAssignment { get; set; }
        public ApplicationUser Employee { get; set; }
        public AttendanceMachine AttendanceMachine { get; set; }
    }
}
