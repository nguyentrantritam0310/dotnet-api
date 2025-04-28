namespace dotnet_api.Data.Entities
{
    public class ConstructionTask
    {
        public int ID { get; set; }
        public int ConstructionStatusID { get; set; }
        public int ConstructionPlanID { get; set; }
        public float Workload { get; set; }

        // Navigation properties
        public ConstructionPlan ConstructionPlan { get; set; }
        public ConstructionStatus ConstructionStatus { get; set; }
        public ICollection<Attendance> Attendances { get; set; }

    }
}
