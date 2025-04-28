using dotnet_api.Data.Enums;
namespace dotnet_api.Data.Entities
{
    public class ConstructionStatus
    {
        public int ID { get; set; }
        public ConstructionStatusEnum Name { get; set; }

        // Navigation properties
        public ICollection<Construction> Constructions { get; set; }
        public ICollection<ConstructionItem> ConstructionItems { get; set; }
        public ICollection<ConstructionPlan> ConstructionPlans { get; set; }
        public ICollection<ConstructionTask> ConstructionTasks { get; set; }
    }
}
