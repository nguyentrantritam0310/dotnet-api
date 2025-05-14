using dotnet_api.Data.Enums;

namespace dotnet_api.Data.Entities
{
    public class ImportOrder
    {
        public int ID { get; set; }
        public DateTime ImportDate { get; set; }
        public ImportOrderStatusEnum Status { get; set; }

        // Navigation properties
        public ICollection<MaterialPlan> MaterialPlans { get; set; }
        public ICollection<ImportOrderEmployee> ImportOrderEmployees { get; set; }
    }
}
