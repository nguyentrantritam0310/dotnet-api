using dotnet_api.Data.Enums;

namespace dotnet_api.DTOs
{
    public class MaterialPlanDTO
    {
        public int ImportOrderID { get; set; }
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public string UnitOfMeasurement { get; set; }
        public int ConstructionItemID { get; set; }
        public int ImportQuantity { get; set; }
        public string? Note { get; set; }
        public string Price { get; set; }
    }

    public class MaterialPlanDTOPOST
    {
        public int ImportOrderID { get; set; }
        public int MaterialID { get; set; }
        public int ConstructionItemID { get; set; }
        public int ImportQuantity { get; set; }
        public string? Note { get; set; }
    }
}
