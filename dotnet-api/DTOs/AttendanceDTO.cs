namespace dotnet_api.DTOs
{
    public class AttendanceDTO
    {
        public string EmployeeID { get; set; }
        public int WorkShiftID { get; set; }
        public DateTime AttendanceDate { get; set; }
        public TimeSpan CheckInOut { get; set; }
        public string Image { get; set; }
    }
}
