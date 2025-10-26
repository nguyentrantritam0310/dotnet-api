using System.ComponentModel.DataAnnotations;

namespace dotnet_api.DTOs
{
    public class FaceRegistrationDTO
    {
        public int ID { get; set; }
        public string EmployeeId { get; set; }
        public string FaceId { get; set; }
        public string ImagePath { get; set; }
        public string EmbeddingData { get; set; }
        public float Confidence { get; set; }
        public DateTime RegisteredDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsActive { get; set; }
        public string RegisteredBy { get; set; }
        public string Notes { get; set; }
        
        // Additional info
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
    }

    public class CreateFaceRegistrationDTO
    {
        [Required]
        public string EmployeeId { get; set; }
        
        [Required]
        public string ImageBase64 { get; set; }
        
        public string Notes { get; set; }
    }

    public class FaceRegistrationResultDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public FaceRegistrationDTO FaceRegistration { get; set; }
        public float Confidence { get; set; }
        public string FaceId { get; set; }
    }

    public class FaceVerificationRequestDTO
    {
        [Required]
        public string EmployeeId { get; set; }
        
        [Required]
        public string ImageBase64 { get; set; }
    }

    public class FaceVerificationResultDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public float Confidence { get; set; }
        public bool IsMatch { get; set; }
        public string MatchedFaceId { get; set; }
        public string EmployeeName { get; set; }
    }

    public class FaceRegistrationListDTO
    {
        public int ID { get; set; }
        public string FaceId { get; set; }
        public float Confidence { get; set; }
        public DateTime RegisteredDate { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public string EmployeeName { get; set; }
    }
}
