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

    public class AttendanceCheckInNoImageRequest
    {
        [Required]
        public string EmployeeId { get; set; } = string.Empty;

        public DateTime CheckInDateTime { get; set; } = DateTime.Now;

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string? Location { get; set; }

        public int? AttendanceMachineId { get; set; }

        public string? Notes { get; set; }

        // Face recognition metadata (optional)
        public string? MatchedFaceId { get; set; }
        public float? MatchConfidence { get; set; }
        public float? FaceQualityScore { get; set; }
    }

    public class AttendanceCheckOutNoImageRequest
    {
        [Required]
        public string EmployeeId { get; set; } = string.Empty;

        public DateTime CheckOutDateTime { get; set; } = DateTime.Now;

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string? Location { get; set; }

        public string? Notes { get; set; }
    }
}



