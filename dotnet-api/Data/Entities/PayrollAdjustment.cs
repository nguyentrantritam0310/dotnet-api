using dotnet_api.Data.Enums;

namespace dotnet_api.Data.Entities
{
    public class PayrollAdjustment
    {
        public string voucherNo { get; set; }
        public DateTime decisionDate { get; set; }
        public int Month { get; set; } // month year
        public int Year { get; set; } // month year

        public string Reason { get; set; }
        public ApproveStatusEnum ApproveStatus { get; set; }
     
        // Navigation properties
        public AdjustmentItem AdjustmentItem { get; set; }
        public AdjustmentType AdjustmentType { get; set; }

    }
}
