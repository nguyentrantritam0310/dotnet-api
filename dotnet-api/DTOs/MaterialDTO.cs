namespace dotnet_api.DTOs
{
    public class MaterialDTO
    {
        public int ID { get; set; }
        public int UnitOfMeasurementID { get; set; }
        public string MaterialName { get; set; }
        public int StockQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Status { get; set; }
        public int MaterialTypeID { get; set; }

    }
}
