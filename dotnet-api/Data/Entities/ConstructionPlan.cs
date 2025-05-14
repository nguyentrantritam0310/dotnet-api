using dotnet_api.Data.Enums;

namespace dotnet_api.Data.Entities
{
    public class ConstructionPlan
    {
        public int ID { get; set; }
        public int ConstructionID { get; set; }
        public string EmployeeID { get; set; }
        public int ConstructionStatusID { get; set; }
        public int ConstructionItemID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedCompletionDate { get; set; }
        public DateTime? ActualCompletionDate { get; set; }

        // Navigation properties
        public ApplicationUser Employee { get; set; }
        public ConstructionStatus ConstructionStatus { get; set; }
        public ConstructionItem ConstructionItem { get; set; }
        public ICollection<ConstructionTask> ConstructionTasks { get; set; }
        
        public ICollection<ExportOrder> ExportOrders { get; set; }
    }
}
