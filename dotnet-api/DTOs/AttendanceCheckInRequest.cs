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

        // Shift assignment - WorkShiftID selected by user
        public int? WorkShiftID { get; set; }
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

        // Shift assignment - WorkShiftID selected by user
        public int? WorkShiftID { get; set; }

        // Face recognition metadata (REQUIRED for security)
        [Required(ErrorMessage = "MatchedFaceId is required for face verification")]
        public string? MatchedFaceId { get; set; }
        
        [Required(ErrorMessage = "MatchConfidence is required for face verification")]
        public float? MatchConfidence { get; set; }
        
        public float? FaceQualityScore { get; set; }
        
        // Security fields for replay attack protection
        [Required(ErrorMessage = "VerificationTimestamp is required")]
        public DateTime? VerificationTimestamp { get; set; }
        
        [Required(ErrorMessage = "VerificationToken is required")]
        public string? VerificationToken { get; set; }
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

        // Face recognition metadata (REQUIRED for security - same as checkin)
        [Required(ErrorMessage = "MatchedFaceId is required for face verification")]
        public string? MatchedFaceId { get; set; }
        
        [Required(ErrorMessage = "MatchConfidence is required for face verification")]
        public float? MatchConfidence { get; set; }
        
        public float? FaceQualityScore { get; set; }
        
        // Security fields for replay attack protection (same as checkin)
        [Required(ErrorMessage = "VerificationTimestamp is required")]
        public DateTime? VerificationTimestamp { get; set; }
        
        [Required(ErrorMessage = "VerificationToken is required")]
        public string? VerificationToken { get; set; }
    }
}



