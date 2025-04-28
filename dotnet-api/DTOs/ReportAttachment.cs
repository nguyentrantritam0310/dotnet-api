namespace dotnet_api.DTOs
{
    public class ReportAttachmentDTO
    {
        public int ID { get; set; }
        public int ReportID { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
