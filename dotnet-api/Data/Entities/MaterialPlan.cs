using dotnet_api.Data.Enums;
namespace dotnet_api.Data.Entities
{
    public class MaterialPlan
    {
        public int ImportOrderID { get; set; }
        public int MaterialID { get; set; }
        public int ConstructionPlanID { get; set; }
        public int ImportQuantity { get; set; }
        public ReportStatusEnum Status { get; set; }
        // Navigation properties
        public Material Material { get; set; }
        public ImportOrder ImportOrder { get; set; }

        public ConstructionPlan ConstructionPlan { get; set; }


    }
}
