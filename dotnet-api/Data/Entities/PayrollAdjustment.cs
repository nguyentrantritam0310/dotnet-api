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
        public int AdjustmentTypeID { get; set; }
        public int? AdjustmentItemID { get; set; } // Nullable since it's optional
        public ApproveStatusEnum ApproveStatus { get; set; }
     
        // Navigation properties
        public AdjustmentType AdjustmentType { get; set; }
        public AdjustmentItem? AdjustmentItem { get; set; } // Nullable navigation property
        public ICollection<ApplicationUser_PayrollAdjustment> applicationUser { get; set; }
        public ICollection<ApplicationUser_PayrollAdjustment> applicationUser_PayrollAdjustment { get; set; }



    }
}
