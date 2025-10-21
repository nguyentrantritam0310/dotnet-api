using System;

namespace dotnet_api.DTOs
{
    // Response DTO
    public class PayrollFeedbackDto
    {
        public int PayrollID { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Title { get; set; }
        public DateTime PayrollFeedbackDate { get; set; }
        public string Content { get; set; }
    }

    // Create DTO
    public class CreatePayrollFeedbackDto
    {
        public int PayrollID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }

    // Update DTO
    public class UpdatePayrollFeedbackDto
    {
        public int PayrollID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}