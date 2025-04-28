namespace dotnet_api.Data.Entities
{
    public class ReportAttachment
    {
        public int ID { get; set; }
        public int ReportID { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }

        // Navigation properties
        public Report Report { get; set; }
    }
}
