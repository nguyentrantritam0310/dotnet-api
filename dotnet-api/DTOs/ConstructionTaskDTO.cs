namespace dotnet_api.DTOs
{
    public class ConstructionTaskDTO
    {
        public int ID { get; set; }
        public int ConstructionStatusID { get; set; }
        public int ConstructionPlanID { get; set; }
        public int ConstructionItemID { get; set; }
        public float Workload { get; set; }
        public string StatusName { get; set; }
        public string UnitOfMeasurementName { get; set; }
        public float totalWorkload { get; set; }
        public float? ActualWorkload { get; set; }

    }

    public class ConstructionTaskDTOPOST
    {
        public int ID { get; set; }
        public int ConstructionStatusID { get; set; }
        public int ConstructionPlanID { get; set; }
        public float Workload { get; set; }
        
    }

    public class ConstructionTaskDTOPUT
    {
        public float? ActualWorkload { get; set; }
    }
}
