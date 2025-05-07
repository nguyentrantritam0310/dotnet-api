using dotnet_api.Data.Enums;

namespace dotnet_api.Data.Entities
{
    public class ExportOrder
    {
        public int ID { get; set; }
        public string EmployeeID { get; set; }
        public int ConstructionPlanID { get; set; }
        public DateTime ExportDate { get; set; }

        // Navigation properties
        public ApplicationUser Employee { get; set; }
        public ConstructionPlan ConstructionPlan { get; set; }
        public ICollection<Material_ExportOrder> Material_ExportOrders { get; set; }
    }
}
