namespace dotnet_api.Data.Entities
{
    public class ConstructionTemplateItem
    {
        public int ID { get; set; }
        public int WorkSubTypeVarientID { get; set; }
        public int ConstructionTypeID { get; set; }
        public string ConstructionTemplateItemName { get; set; }

        // Navigation properties
        public ConstructionType ConstructionType { get; set; }
        public WorkSubTypeVariant WorkSubTypeVariant { get; set; }
    }
}
