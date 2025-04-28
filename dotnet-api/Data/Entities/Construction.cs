namespace dotnet_api.Data.Entities
{
    public class Construction
    {
        public int ID { get; set; }
        public int ConstructionTypeID { get; set; }
        public int ConstructionStatusID { get; set; }
        public string ConstructionName { get; set; }
        public string Location { get; set; }
        public float TotalArea { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedCompletionDate { get; set; }
        public DateTime? ActualCompletionDate { get; set; }
        public string DesignBlueprint { get; set; }

        // Navigation properties
        public ConstructionType ConstructionType { get; set; }
        public ConstructionStatus ConstructionStatus { get; set; }
        public ICollection<ConstructionItem> ConstructionItems { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}
