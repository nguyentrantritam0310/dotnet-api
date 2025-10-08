namespace dotnet_api.Data.Entities
{
    public class ShiftDetail
    {
        public int ID { get; set; }
        public int WorkShiftID { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan BreakStart { get; set; }
        public TimeSpan BreakEnd { get; set; }
        public WorkShift WorkShift { get; set; }
    }
}
