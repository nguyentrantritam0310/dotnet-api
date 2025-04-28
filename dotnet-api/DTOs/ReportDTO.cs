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

    }
}
