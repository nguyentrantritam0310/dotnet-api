namespace dotnet_api.DTOs
{
    public class AttendanceDTO
    {
        public string EmployeeID { get; set; }
        public int ConstructionTaskID { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string Status { get; set; }
    }

    public class AttendanceDTOGET
    {
        public string EmployeeID { get; set; }
        public int ConstructionTaskID { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string Status { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
    }

    public class UpdateAttendanceStatusDTO
    {
        public string EmployeeID { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string Status { get; set; }
    }
}
