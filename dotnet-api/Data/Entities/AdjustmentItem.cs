using dotnet_api.Data.Enums;

namespace dotnet_api.Data.Entities
{
    public class AdjustmentItem
    {
        
        public int ID { get; set; }
        public string AdjustmentItemName { get; set; }
        public int AdjustmentTypeID { get; set; }
        public AdjustmentType adjustmentType { get; set; }


    }
}
