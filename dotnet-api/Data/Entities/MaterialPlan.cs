using dotnet_api.Data.Enums;
namespace dotnet_api.Data.Entities
{
    public class MaterialPlan
    {
        public int ImportOrderID { get; set; }
        public int MaterialID { get; set; }
        public int ConstructionItemID { get; set; }
        public int ImportQuantity { get; set; }
        public string? Note { get; set; }

        // Navigation properties
        public Material Material { get; set; }
        public ImportOrder ImportOrder { get; set; }
        public ConstructionItem ConstructionItem { get; set; }
    }
}
