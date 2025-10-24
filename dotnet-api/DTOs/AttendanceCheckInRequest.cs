using System.ComponentModel.DataAnnotations;

namespace dotnet_api.DTOs
{
    public class AttendanceCheckInRequest
    {
        [Required]
        public string EmployeeId { get; set; } = string.Empty;

        [Required]
        public string ImageBase64 { get; set; } = string.Empty;

        public DateTime CheckInDateTime { get; set; } = DateTime.Now;

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string? Location { get; set; }

        public int? AttendanceMachineId { get; set; }

        public string? Notes { get; set; }
    }
}
