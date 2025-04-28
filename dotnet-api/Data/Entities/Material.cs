namespace dotnet_api.Data.Entities
{
    public class Material
    {
        public int ID { get; set; }
        public int UnitOfMeasurementID { get; set; }
        public string MaterialName { get; set; }
        public int StockQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Status { get; set; }
        public string? Specification { get; set; }
        public string? Note { get; set; }
        public int MaterialTypeID { get; set; }

        // Navigation properties
        public UnitofMeasurement UnitOfMeasurement { get; set; }
        public MaterialType MaterialType { get; set; }
        public ICollection<MaterialNorm> MaterialNorms { get; set; }
        public ICollection<MaterialPlan> MaterialPlans { get; set; }
        public ICollection<Material_ExportOrder> Material_ExportOrders { get; set; }

    }
}
