using dotnet_api.Data.Enums;

namespace dotnet_api.Data.Entities
{
    public class ShiftAssignment
    {
        public int ID { get; set; }
        public string EmployeeID { get; set; }
        public int WorkShiftID { get; set; }
        public DateTime WorkDate { get; set; }
        public int? ConstructionTaskID { get; set; }

        // Navigation properties
        public ApplicationUser Employee { get; set; }
        public WorkShift WorkShift { get; set; }
        public Attendance Attendance { get; set; }
        public ConstructionTask ConstructionTask { get; set; }
    }
}
