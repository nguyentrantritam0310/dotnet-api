using dotnet_api.Data.Enums;

namespace dotnet_api.Data.Entities
{
    public class ApplicationUser_PayrollAdjustment
    {
        public string PayrollAdjustmentID { get; set; }
        public string EmployeeID { get; set; }
        public float Value { get; set; }

        // Navigation properties
        public PayrollAdjustment PayrollAdjustment { get; set; }
        public ApplicationUser applicationUser { get; set; }


    }
}
