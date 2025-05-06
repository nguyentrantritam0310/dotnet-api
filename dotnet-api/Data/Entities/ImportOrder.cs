using dotnet_api.Data.Enums;

namespace dotnet_api.Data.Entities
{
    public class ImportOrder
    {
        public int ID { get; set; }
        public string EmployeeID { get; set; }
        public DateTime ImportDate { get; set; }

        // Navigation properties
        public ApplicationUser Employee { get; set; }
        public ICollection<MaterialPlan> MaterialPlans { get; set; }
    }
}
