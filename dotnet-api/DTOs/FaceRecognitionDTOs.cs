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

    // DTOs for Attendance - DEPRECATED, use new AttendanceCheckInRequest.cs instead
    // public class AttendanceCheckInRequest - MOVED TO AttendanceCheckInRequest.cs
    // public class AttendanceCheckInResult - MOVED TO AttendanceCheckInResult.cs
}
