namespace dotnet_api.Data.Entities
{
    public class MaterialNorm
    {
        public int MaterialID { get; set; }
        public int WorkSubTypeVariantID { get; set; }
        public int Quantity { get; set; }

        // Navigation properties
        public Material Material { get; set; }
        public WorkSubTypeVariant WorkSubTypeVariant { get; set; }
    }
}
