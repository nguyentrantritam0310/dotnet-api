namespace dotnet_api.Data.Entities
{
    public class ConstructionPlan
    {
        public int ID { get; set; }
        public int ConstructionStatusID { get; set; }
        public int EmployeeID { get; set; }
        public int ConstructionItemID { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime ExpectedCompletionDate { get; set; }
        public DateTime? ActualCompletionDate { get; set; }

        // Navigation properties
        public Employee Employee { get; set; }
        public ConstructionStatus ConstructionStatus { get; set; }
        public ConstructionItem ConstructionItem { get; set; }
        public ICollection<ConstructionTask> ConstructionTasks { get; set; }
        public ICollection<MaterialPlan> MaterialPlans { get; set; }
        public ICollection<ExportOrder> ExportOrders { get; set; }

    }
}
