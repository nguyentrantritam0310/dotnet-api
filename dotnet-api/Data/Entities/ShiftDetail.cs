namespace dotnet_api.Data.Entities
{
    public class ShiftDetail
    {
        public int ID { get; set; }
        public int WorkShiftID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime BreakStart { get; set; }
        public DateTime BreakEnd { get; set; }
        public WorkShift WorkShift { get; set; }
    }
}
