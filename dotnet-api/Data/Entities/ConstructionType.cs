using dotnet_api.Data.Enums;

namespace dotnet_api.Data.Entities
{
    public class ConstructionType
    {
        public int ID { get; set; }
        public ConstructionTypeEnum ConstructionTypeName { get; set; }

        // Navigation properties
        public ICollection<Construction> Constructions { get; set; }
        public ICollection<ConstructionTemplateItem> ConstructionTemplateItems { get; set; }
    }
}
