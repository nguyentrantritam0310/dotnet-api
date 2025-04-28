using dotnet_api.Data.Enums;
namespace dotnet_api.Data.Entities
{
    public class ReportStatusLog
    {
        public int ID { get; set; }
        public int ReportID { get; set; }
        public ReportStatusEnum Status { get; set; }
        public string Note { get; set; }
        public DateTime ReportDate { get; set; }

        // Navigation properties
        public Report Report { get; set; }
    }
}
