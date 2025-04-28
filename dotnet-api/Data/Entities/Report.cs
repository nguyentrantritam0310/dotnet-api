namespace dotnet_api.Data.Entities
{
    public class Report
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int ConstructionID { get; set; }
        public DateTime ReportDate { get; set; }
        public string ReportType { get; set; }
        public string Content { get; set; }
        public string Level { get; set; }
        public string ProblemType { get; set; }

        // Navigation properties
        public Employee Employee { get; set; }
        public Construction Construction { get; set; }
        public ICollection<ReportAttachment> ReportAttachments { get; set; }
        public ICollection<ReportStatusLog> ReportStatusLogs { get; set; }

    }
}
