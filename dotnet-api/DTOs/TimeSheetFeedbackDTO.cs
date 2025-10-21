using System;

namespace dotnet_api.DTOs
{
    // Response DTO
    public class TimeSheetFeedbackDto
    {
        public int TimeSheetID { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Title { get; set; }
        public DateTime TimeSheetFeedbackDate { get; set; }
        public string Content { get; set; }
    }

    // Create DTO
    public class CreateTimeSheetFeedbackDto
    {
        public int TimeSheetID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }

    // Update DTO
    public class UpdateTimeSheetFeedbackDto
    {
        public int TimeSheetID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}