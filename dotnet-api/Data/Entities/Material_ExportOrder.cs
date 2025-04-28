namespace dotnet_api.Data.Entities
{
    public class Material_ExportOrder
    {
        public int Quantity { get; set; }
        public int ExportOrderID { get; set; }
        public int MaterialID { get; set; }
        // Navigation properties
        public ExportOrder ExportOrder { get; set; }
        public Material Material { get; set; }
    }
}
