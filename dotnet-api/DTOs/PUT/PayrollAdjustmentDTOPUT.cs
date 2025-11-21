using System.ComponentModel.DataAnnotations;

namespace dotnet_api.DTOs.PUT
{
    public class PayrollAdjustmentDTOPUT
    {
        [Required(ErrorMessage = "Voucher number is required")]
        public string voucherNo { get; set; }
        
        [Required(ErrorMessage = "Decision date is required")]
        public DateTime decisionDate { get; set; }
        
        [Required(ErrorMessage = "Month is required")]
        [Range(1, 12, ErrorMessage = "Month must be between 1 and 12")]
        public int month { get; set; }
        
        [Required(ErrorMessage = "Year is required")]
        [Range(2020, 2030, ErrorMessage = "Year must be between 2020 and 2030")]
        public int year { get; set; }
        
        public string reason { get; set; }
        
        [Required(ErrorMessage = "Adjustment type ID is required")]
        public int adjustmentTypeID { get; set; }
        
        public int? adjustmentItemID { get; set; }
        
        public List<PayrollAdjustmentEmployeeDTOPUT> employees { get; set; } = new List<PayrollAdjustmentEmployeeDTOPUT>();
        
        public string? Notes { get; set; } // Ghi chú khi duyệt/từ chối/trả lại
    }

    public class PayrollAdjustmentEmployeeDTOPUT
    {
        [Required(ErrorMessage = "Employee ID is required")]
        public string employeeID { get; set; }
        
        [Required(ErrorMessage = "Employee name is required")]
        public string employeeName { get; set; }
        
        [Required(ErrorMessage = "Value is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Value must be greater than or equal to 0")]
        public float value { get; set; }
    }
}
