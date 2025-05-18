using dotnet_api.Data.Enums;

namespace dotnet_api.Data.Entities
{
    public class Attendance
    {
        public string EmployeeID { get; set; }
        public int ConstructionTaskID { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string Status { get; set; }

        // Navigation properties
        public ApplicationUser Employee { get; set; }
        public ConstructionTask ConstructionTask { get; set; }
 


    }
}
