using dotnet_api.Data.Enums;

namespace dotnet_api.DTOs
{
    public class PayrollAdjustmentDTO
    {
        public string voucherNo { get; set; }
        public DateTime decisionDate { get; set; }
        public int Month { get; set; } // month year
        public int Year { get; set; } // month year
        public string Reason { get; set; }
        public int AdjustmentTypeID { get; set; }
        public string AdjustmentTypeName { get; set; }
        public int AdjustmentItemID { get; set; }
        public string AdjustmentItemName { get; set; }
        public string ApproveStatus { get; set; }
        public List<PayrollAdjustmentEmployeeDTO> Employees { get; set; }

    }

    public class PayrollAdjustmentEmployeeDTO
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public float Value { get; set; }
    }
}
