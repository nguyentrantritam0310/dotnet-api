namespace dotnet_api.DTOs
{
    public class MaterialPlanDTO
    {
        public int ImportOrderID { get; set; }
        public int MaterialID { get; set; }
        public int ConstructionPlanID { get; set; }
        public int ImportQuantity { get; set; }
        public int Status { get; set; }
    }
}
