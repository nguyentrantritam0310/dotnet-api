namespace dotnet_api.DTOs
{
    public class FaceRegistrationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string FaceId { get; set; }
        public float Confidence { get; set; }
    }

    public class FaceRecognitionResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public float Confidence { get; set; }
    }

    public class RegisteredEmployee
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string FaceId { get; set; }
    }

    public class FaceEmbedding
    {
        public string EmployeeId { get; set; }
        public string FaceId { get; set; }
        public float[] Embedding { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
