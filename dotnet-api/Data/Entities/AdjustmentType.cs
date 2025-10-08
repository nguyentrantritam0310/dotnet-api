using dotnet_api.Data.Enums;

namespace dotnet_api.Data.Entities
{
    public class AdjustmentType
    {
        public int ID { get; set; }
        public string AdjustmentTypeName { get; set; }
        // Navigation properties
        public ICollection<AdjustmentItem> AdjustmentItems { get; set; }
        public ICollection<PayrollAdjustment> PayrollAdjustments { get; set; }

    }
}
