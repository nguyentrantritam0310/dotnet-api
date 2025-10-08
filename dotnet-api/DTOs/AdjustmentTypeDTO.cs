namespace dotnet_api.DTOs
{
    public class AdjustmentTypeDTO
    {
        public int ID { get; set; }
        public string AdjustmentTypeName { get; set; }
        public List<AdjustmentItemDTO> AdjustmentItems { get; set; }
    }
}

