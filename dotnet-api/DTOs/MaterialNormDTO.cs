namespace dotnet_api.DTOs
{
    public class MaterialNormDTO
    {
        public int MaterialID { get; set; }
        public int ConstructionItemID { get; set; }
        public string ConstructionItemName { get; set; }
        public int Quantity { get; set; }
        public string MaterialName { get; set; }
        public int StockQuantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string WorkSubTypeVariantDescription { get; set; }
        public string Price { get; set; }
        public string ConstructionId { get; set; }
    }

    public class MaterialNormItemDTO
    {
        public int MaterialID { get; set; }
        public int ConstructionItemID { get; set; }
        public string ConstructionItemName { get; set; }
        public int Quantity { get; set; }
        public string MaterialName { get; set; }
        public int StockQuantity { get; set; }
        public string UnitOfMeasurement { get; set; }
    }
}
