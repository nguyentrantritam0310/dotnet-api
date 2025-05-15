namespace dotnet_api.Data.Entities
{
    public class ConstructionItem
    {
        public int ID { get; set; }
        public int ConstructionStatusID { get; set; }
        public int WorkSubTypeVariantID { get; set; }
        public int ConstructionID { get; set; }
        public int UnitOfMeasurementID { get; set; }
        public string ConstructionItemName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedCompletionDate { get; set; }
        public DateTime? ActualCompletionDate { get; set; }
        public float TotalVolume { get; set; }

        // Navigation properties
        public Construction Construction { get; set; }
        public UnitofMeasurement UnitOfMeasurement { get; set; }
        public ConstructionStatus ConstructionStatus { get; set; }
        public WorkSubTypeVariant WorkSubTypeVariant { get; set; }
        public ICollection<MaterialPlan> MaterialPlans { get; set; }
        public ICollection<ConstructionPlan> ConstructionPlans { get; set; }
        public ICollection<ExportOrder> ExportOrders { get; set; }

    }
}
