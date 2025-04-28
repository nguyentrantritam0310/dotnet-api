namespace dotnet_api.Data.Entities
{
    public class ImportOrder
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime ImportDate { get; set; }

        // Navigation properties
        public Employee Employee { get; set; }
        public ICollection<MaterialPlan> MaterialPlans { get; set; }

    }
}
