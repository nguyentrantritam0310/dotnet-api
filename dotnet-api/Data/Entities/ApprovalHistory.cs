using dotnet_api.Data.Enums;

namespace dotnet_api.Data.Entities
{
    public class ApprovalHistory
    {
        public int ID { get; set; }
        public string RequestType { get; set; } // "LeaveRequest", "OvertimeRequest", "PayrollAdjustment", "Contract"
        public string RequestID { get; set; } // VoucherCode hoặc ID tùy loại
        public string ApproverID { get; set; } // FK to ApplicationUser
        public string ApproverName { get; set; } // Lưu tên để tránh join
        public string Action { get; set; } // "Approve", "Reject", "Return", "Submit"
        public ApproveStatusEnum? OldStatus { get; set; } // Nullable vì có thể là lần đầu submit
        public ApproveStatusEnum NewStatus { get; set; }
        public string? Notes { get; set; } // Ghi chú khi duyệt/từ chối/trả lại
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public ApplicationUser Approver { get; set; }
    }
}


