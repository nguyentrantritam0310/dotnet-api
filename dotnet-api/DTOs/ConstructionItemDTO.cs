namespace dotnet_api.DTOs
{
    public class ConstructionItemDTO
    {
        public int ID { get; set; }
        public int ConstructionStatusID { get; set; }
        public int WorkSubTypeVariantID { get; set; }
        public int ConstructionID { get; set; }
        public int UnitOfMeasurementID { get; set; }
        public string ConstructionItemName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedCompletionDate { get; set; }
        public DateTime ActualCompletionDate { get; set; }
        public float TotalVolume { get; set; }

    }
}
