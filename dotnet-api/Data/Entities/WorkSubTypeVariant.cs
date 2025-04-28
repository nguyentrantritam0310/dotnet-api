namespace dotnet_api.Data.Entities
{
    public class WorkSubTypeVariant
    {
        public int ID { get; set; }
        public int WorkSubTypeID { get; set; }
        public string Description { get; set; }
        // Navigation properties
        public WorkSubType WorkSubType { get; set; }
        public ICollection<WorkSubTypeVariant_WorkAttribute> WorkSubTypeVariant_WorkAttributes { get; set; }
        public ICollection<MaterialNorm> MaterialNorms { get; set; }
        public ICollection<ConstructionTemplateItem> ConstructionTemplateItems { get; set; }
        public ICollection<ConstructionItem> ConstructionItems { get; set; }
    }
}
