namespace dotnet_api.DTOs.POST
{
    public class ConstructionPlanDTOPOST
    {
        public int ID { get; set; }
        public int ConstructionID { get; set; }
        public int ConstructionItemID { get; set; }
        public string EmployeeID { get; set; }
        public int ConstructionStatusID { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime ExpectedCompletionDate { get; set; }
    }
}

