using System;

namespace dotnet_api.DTOs
{
    public class ConstructionItemDTO
    {
        public int ID { get; set; }
        public int ConstructionID { get; set; }
        public string ConstructionItemName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedCompletionDate { get; set; }
        public DateTime? ActualCompletionDate { get; set; }
        public double TotalVolume { get; set; }
        public int UnitOfMeasurementID { get; set; }
        public string UnitName { get; set; }
        public int WorkSubTypeVariantID { get; set; }
        public int ConstructionItemStatusID { get; set; }
        public string ConstructionItemStatusName { get; set; }
    }

    public class ConstructionItemCreateDTO
    {
        public int ConstructionID { get; set; }
        public string ConstructionItemName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedCompletionDate { get; set; }
        public double TotalVolume { get; set; }
        public int UnitOfMeasurementID { get; set; }
        public int WorkSubTypeVariantID { get; set; }
    }

    public class ConstructionItemUpdateDTO
    {
        public int ID { get; set; }
        public string ConstructionItemName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedCompletionDate { get; set; }
        public double TotalVolume { get; set; }
        public int UnitOfMeasurementID { get; set; }
        public int WorkSubTypeVariantID { get; set; }
    }

    public class UpdateConstructionItemStatusDTO
    {
        public int Status { get; set; }
    }
}
