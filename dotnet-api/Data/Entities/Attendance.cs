namespace dotnet_api.Data.Entities
{
    public class Attendance
    {
        public int EmployeeID { get; set; }
        public int ConstructionTaskID { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string Status { get; set; }

        // Navigation properties
        public Employee Employee { get; set; }
        public ConstructionTask ConstructionTask { get; set; }
 


    }
}
