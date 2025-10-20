using dotnet_api.Data.Enums;

namespace dotnet_api.DTOs
{
    public class FaceRegistrationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string FaceId { get; set; }
        public float Confidence { get; set; }
        public int FaceRegistrationId { get; set; }
    }

    public class FaceRecognitionResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public float Confidence { get; set; }
        public int FaceRegistrationId { get; set; }
    }

    public class RegisteredEmployee
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string FaceId { get; set; }
        public float Confidence { get; set; }
        public bool IsActive { get; set; }
    }

    public class FaceEmbedding
    {
        public string EmployeeId { get; set; }
        public string FaceId { get; set; }
        public float[] Embedding { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class FaceRegistrationRequest
    {
        public string EmployeeId { get; set; }
        public string ImageBase64 { get; set; }
    }

    // DTOs for Attendance
    public class AttendanceCheckInRequest
    {
        public string EmployeeId { get; set; }
        public string ImageBase64 { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Location { get; set; }
        public int? AttendanceMachineId { get; set; }
        public DateTime CheckInDateTime { get; set; }
    }

    public class AttendanceCheckInResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int AttendanceId { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime CheckInDateTime { get; set; }
        public float FaceRecognitionConfidence { get; set; }
        public AttendanceStatusEnum Status { get; set; }
    }

    // DTO for Face Registration POST
    public class FaceRegistrationDTO
    {
        public string EmployeeId { get; set; }
        public string FaceId { get; set; }
        public string ImagePath { get; set; }
        public string EmbeddingData { get; set; }
        public float Confidence { get; set; }
        public string RegisteredBy { get; set; }
        public string Notes { get; set; }
    }
}
