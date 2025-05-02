using dotnet_api.Data.Enums;

namespace dotnet_api.DTOs
{
    public class MaterialPlanDTO
    {
        public int ImportOrderID { get; set; }
        public int MaterialID { get; set; }
        public int ConstructionPlanID { get; set; }
        public int ImportQuantity { get; set; }
        public ReportStatusEnum Status { get; set; }
    }
}
