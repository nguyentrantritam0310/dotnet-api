using System;

namespace dotnet_api.Data.Entities
{
    public class PayrollFeedback
    {
        public int PayrollID { get; set; } // FK to Payroll
        public string EmployeeID { get; set; } // Người nhận phản ánh (ApplicationUser)
        public string Title { get; set; }
        public DateTime PayrollFeedbackDate { get; set; }
        public string Content { get; set; }

        // Navigation properties
        public Payroll Payroll { get; set; }
        public ApplicationUser Employee { get; set; }
    }
}
