using dotnet_api.Data.Enums;

namespace dotnet_api.Data.Entities
{
    public class AdjustmentType
    {
        public int ID { get; set; }
        public string adjustmentTypeName { get; set; }
        // Navigation properties
        public AdjustmentItem AdjustmentItem { get; set; }

    }
}
