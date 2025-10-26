using System.ComponentModel.DataAnnotations;

namespace dotnet_api.DTOs
{
    public class AttendanceCheckOutRequest
    {
        [Required]
        public string EmployeeId { get; set; } = string.Empty;

        public string? ImageBase64 { get; set; }

        public DateTime CheckOutDateTime { get; set; } = DateTime.Now;

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string? Location { get; set; }

        public string? Notes { get; set; }
    }
}



