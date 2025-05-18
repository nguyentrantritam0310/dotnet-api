using System;
using System.Collections.Generic;

namespace dotnet_api.DTOs.POST
{
    public class ConstructionDTOPOST
    {
        public string ConstructionName { get; set; }
        public int ConstructionTypeID { get; set; }
        public int ConstructionStatusID { get; set; }
        public string Location { get; set; }
        public double TotalArea { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedCompletionDate { get; set; }
        public string DesignBlueprint { get; set; }
        public List<ConstructionItemDTOPOST> ConstructionItems { get; set; }
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