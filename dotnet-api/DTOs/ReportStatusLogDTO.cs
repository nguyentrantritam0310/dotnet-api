namespace dotnet_api.DTOs
{
    public class ReportStatusLogDTO
    {
        public int ID { get; set; }
        public int ReportID { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public DateTime ReportDate { get; set; }
    }
}
