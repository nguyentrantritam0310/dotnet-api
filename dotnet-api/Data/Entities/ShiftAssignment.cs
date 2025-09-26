using dotnet_api.Data.Enums;

namespace dotnet_api.Data.Entities
{
    public class ShiftAssignment
    {
        public int ID { get; set; }
        public string EmployeeID { get; set; }
        public int WorkShiftID { get; set; }
        public DateTime WorkDate { get; set; }


        // Navigation properties
        public ApplicationUser Employee { get; set; }
        public EmployeeRequest WorkShift { get; set; }
        public Attendance Attendance { get; set; }
 


    }
}
