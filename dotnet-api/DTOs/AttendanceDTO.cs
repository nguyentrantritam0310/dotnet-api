namespace dotnet_api.DTOs
{
    public class AttendanceDTO
    {
        public int EmployeeID { get; set; }
        public int ConstructionTaskID { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string Status { get; set; }
    }
}
