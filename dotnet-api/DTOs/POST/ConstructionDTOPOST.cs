using System;
using System.Collections.Generic;

namespace dotnet_api.DTOs.POST
{
    public class ConstructionDTOPOST
    {
        public int ConstructionTypeID { get; set; }
        public string ConstructionName { get; set; }
        public string Location { get; set; }
        public float TotalArea { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedCompletionDate { get; set; }
        public List<ConstructionItemDTO> ConstructionItems { get; set; }
    }

    public class ConstructionItemDTOPOST
    {
        public string ConstructionItemName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalVolume { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string TaskType { get; set; }
        public string Description { get; set; }
    }
} 