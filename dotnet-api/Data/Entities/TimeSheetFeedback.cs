using System;

namespace dotnet_api.Data.Entities
{
    public class TimeSheetFeedback
    {
        public int TimeSheetID { get; set; }
        public string EmployeeID { get; set; }
        public string Title { get; set; }
        public DateTime TimeSheetFeedbackDate { get; set; }
        public string Content { get; set; }

        // Navigation properties
        public TimeSheet TimeSheet { get; set; }
        public ApplicationUser Employee { get; set; }
    }
}
