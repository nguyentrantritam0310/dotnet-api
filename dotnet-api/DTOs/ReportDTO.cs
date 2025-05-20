using dotnet_api.Data.Enums;
using Microsoft.AspNetCore.Http;

namespace dotnet_api.DTOs
{
    public class ReportDTO
    {
        public int ID { get; set; }
        public string EmployeeID { get; set; }
        public string constructionName { get; set; }
        public int ConstructionID { get; set; }
        public DateTime ReportDate { get; set; }
        public string ReportType { get; set; } // incident or technical // equipment, material, construction, other
        public string Content { get; set; }
        public string Level { get; set; } // low, medium, high, critical
        public List<string> ImagePaths { get; set; } = new List<string>();
        public List<ReportStatusLogDTO> StatusLogs { get; set; }
        public List<ReportAttachmentDTO> Attachments { get; set; }
    }

    public class ReportCreateDTO
    {
        public int ConstructionID { get; set; }
        public string ReportType { get; set; }
        public string Content { get; set; }
        public string Level { get; set; }
        public List<IFormFile>? Images { get; set; }
    }

    public class ReportUpdateDTO
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public string Level { get; set; }
        public List<IFormFile>? NewImages { get; set; } = null;
        public List<string>? DeletedImagePaths { get; set; } = null;
    }
}
