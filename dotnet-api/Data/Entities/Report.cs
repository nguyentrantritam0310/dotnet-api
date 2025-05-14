using dotnet_api.Data.Enums;

namespace dotnet_api.Data.Entities
{
    public class Report
    {
        public int ID { get; set; }
        public string EmployeeID { get; set; }
        public int ConstructionID { get; set; }
        public DateTime ReportDate { get; set; }
        public string ReportType { get; set; }
        public string Content { get; set; }
        public string Level { get; set; }

        // Navigation properties
        public ApplicationUser Employee { get; set; }
        public Construction Construction { get; set; }
        public ICollection<ReportAttachment> ReportAttachments { get; set; }
        public ICollection<ReportStatusLog> ReportStatusLogs { get; set; }
    }
}
