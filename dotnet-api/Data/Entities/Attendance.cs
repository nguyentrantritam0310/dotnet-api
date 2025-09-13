using dotnet_api.Data.Enums;

namespace dotnet_api.Data.Entities
{
    public class Attendance
    {
        public string EmployeeID { get; set; }
        public int WorkShiftID { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string Status { get; set; }
        public DateTime CheckInOut { get; set; }
        public string Image { get; set; }

        // Navigation properties
        public ApplicationUser Employee { get; set; }
        public WorkShift WorkShift { get; set; }
 


    }
}
