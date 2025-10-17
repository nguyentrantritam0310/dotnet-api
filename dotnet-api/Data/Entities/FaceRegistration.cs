using dotnet_api.Data.Enums;

namespace dotnet_api.Data.Entities
{
    public class FaceRegistration
    {
        public int ID { get; set; }
        public string EmployeeId { get; set; } // Foreign key to ApplicationUser
        public string FaceId { get; set; } // Unique identifier for face
        public string ImagePath { get; set; } // Path to face image
        public string EmbeddingData { get; set; } // FaceNet embedding as JSON string
        public float Confidence { get; set; } // Registration confidence
        public DateTime RegisteredDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsActive { get; set; } // For soft delete
        public string RegisteredBy { get; set; } // Who registered this face
        public string Notes { get; set; } // Additional notes

        // Navigation properties
        public ApplicationUser Employee { get; set; }
    }
}

