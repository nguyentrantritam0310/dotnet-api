namespace dotnet_api.DTOs
{
    public class ConstructionPlanDTO
    {
        public int ID { get; set; }
        public int ConstructionStatusID { get; set; }
        public int EmployeeID { get; set; }
        public int ConstructionItemID { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime ExpectedCompletionDate { get; set; }
        public DateTime ActualCompletionDate { get; set; }
    }
}
