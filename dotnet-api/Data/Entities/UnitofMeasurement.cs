namespace dotnet_api.Data.Entities
{
    public class UnitofMeasurement
    {
        public int ID { get; set; }
        public string UnitName { get; set; }
        public string ShortName { get; set; }
        public string Category { get; set; }
        public ICollection<Material> Materials { get; set; }
        public ICollection<ConstructionItem> ConstructionItems { get; set; }
        public ICollection<WorkAttribute> WorkAttributes { get; set; }

    }
}
