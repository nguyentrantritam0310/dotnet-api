using dotnet_api.Data.Enums;

namespace dotnet_api.DTOs
{
    public class ApprovalHistoryDTO
    {
        public int ID { get; set; }
        public string RequestType { get; set; }
        public string RequestID { get; set; }
        public string ApproverID { get; set; }
        public string ApproverName { get; set; }
        public string Action { get; set; }
        public ApproveStatusEnum? OldStatus { get; set; }
        public ApproveStatusEnum NewStatus { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}


