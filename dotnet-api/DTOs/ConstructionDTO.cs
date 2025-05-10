namespace dotnet_api.DTOs
{
    public class ConstructionDTO
    {
        public int ID { get; set; }
        public int ConstructionTypeID { get; set; }
        public int ConstructionStatusID { get; set; }
        public string ConstructionName { get; set; }
        public string Location { get; set; }
        public float TotalArea { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedCompletionDate { get; set; }
        public DateTime ActualCompletionDate { get; set; }
        public string DesignBlueprint { get; set; }
        public string StatusName { get; set; }
        public string ItemStatusName { get; set; }
        public string unitName { get; set; }

        public List<ConstructionItemDTO> ConstructionItems { get; set; }
    }
}
