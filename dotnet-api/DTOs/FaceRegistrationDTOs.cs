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
        public string FaceFeaturesData { get; set; }
        public float Confidence { get; set; }
        public float FaceQualityScore { get; set; }
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
        public string FaceFeatures { get; set; } // JSON string containing ML Kit face features
        
        public string Notes { get; set; }
    }

    // ML Kit Face Detection DTOs
    public class FaceLandmarkDTO
    {
        public string Type { get; set; } // leftEye, rightEye, nose, mouth, leftEar, rightEar, leftCheek, rightCheek
        public float X { get; set; }
        public float Y { get; set; }
    }

    public class FaceContourDTO
    {
        public string Type { get; set; } // face, leftEyebrow, rightEyebrow, leftEye, rightEye, upperLip, lowerLip, nose
        public List<FaceLandmarkDTO> Points { get; set; }
    }

    public class FaceFeaturesDTO
    {
        public FaceBoundsDTO Bounds { get; set; }
        public List<FaceLandmarkDTO> Landmarks { get; set; }
        public List<FaceContourDTO> Contours { get; set; }
        public FaceEulerAnglesDTO HeadEulerAngles { get; set; }
        public FaceProbabilitiesDTO Probabilities { get; set; }
        public int? TrackingId { get; set; }
        public float? Confidence { get; set; }
    }

    public class FaceBoundsDTO
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
    }

    public class FaceEulerAnglesDTO
    {
        public float X { get; set; } // Up/down tilt
        public float Y { get; set; } // Left/right tilt  
        public float Z { get; set; } // Left/right rotation
    }

    public class FaceProbabilitiesDTO
    {
        public float LeftEyeOpenProbability { get; set; }
        public float RightEyeOpenProbability { get; set; }
        public float SmilingProbability { get; set; }
    }

    public class FaceRegistrationResultDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public FaceRegistrationDTO FaceRegistration { get; set; }
        public float Confidence { get; set; }
        public string FaceId { get; set; }
        public float FaceQualityScore { get; set; }
    }

    public class FaceVerificationRequestDTO
    {
        [Required]
        public string EmployeeId { get; set; }
        
        [Required]
        public string ImageBase64 { get; set; }
        
        public string FaceFeatures { get; set; } // Optional: ML Kit face features JSON string for quality validation
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

    // Embedding-only registration (no image upload)
    public class FaceEmbeddingRegisterRequestDTO
    {
        [Required]
        public string EmployeeId { get; set; }

        [Required]
        public float[] Embedding { get; set; }

        public float? FaceQualityScore { get; set; }

        public string? Notes { get; set; }

        // Pose label: front | left | right | up
        public string? Pose { get; set; }
    }

    public class FaceEmbeddingVerifyRequestDTO
    {
        [Required]
        public string EmployeeId { get; set; }

        [Required]
        public float[] Embedding { get; set; }
    }

    public class FaceRegistrationListDTO
    {
        public int ID { get; set; }
        public string FaceId { get; set; }
        public float Confidence { get; set; }
        public float FaceQualityScore { get; set; }
        public DateTime RegisteredDate { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public string EmployeeName { get; set; }
    }
}
