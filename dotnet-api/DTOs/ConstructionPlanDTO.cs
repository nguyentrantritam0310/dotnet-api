namespace dotnet_api.DTOs
{
    public class ConstructionPlanDTO
    {
        public int ID { get; set; }
        public int ConstructionStatusID { get; set; }
        public string EmployeeID { get; set; }
        public int ConstructionItemID { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime ExpectedCompletionDate { get; set; }
        public DateTime ActualCompletionDate { get; set; }

        public string EmployeeName { get; set; }
        public string ConstructionItemName { get; set; }
        public string ConstructionName { get; set; }
        public string ConstructionID { get; set; }
        public string StatusName { get; set; }
    }
}

