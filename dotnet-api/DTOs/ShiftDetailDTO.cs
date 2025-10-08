namespace dotnet_api.DTOs
{
    public class ShiftDetailDTO
    {
        public int ID { get; set; }
        public int WorkShiftID { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan BreakStart { get; set; }
        public TimeSpan BreakEnd { get; set; }
    }

    public class ShiftDetailDTOPOST
    {
        public int WorkShiftID { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan BreakStart { get; set; }
        public TimeSpan BreakEnd { get; set; }
    }
}
