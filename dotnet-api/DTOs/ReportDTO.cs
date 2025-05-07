using dotnet_api.Data.Enums;

namespace dotnet_api.DTOs
{
    public class ReportDTO
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int ConstructionID { get; set; }
        public DateTime ReportDate { get; set; }
        public string ReportType { get; set; }
        public string Content { get; set; }
        public string Level { get; set; }
        public string ProblemType { get; set; }
        public List<ReportStatusLogDTO> StatusLogs { get; set; }
        public List<ReportAttachmentDTO> Attachments { get; set; }

    }
}
