namespace dotnet_api.Data.Entities
{
    public class OvertimeForm
    {
        public int ID { get; set; }
        public string OvertimeFormName { get; set; }
        public ICollection<EmployeeRequests>? EmployeeRequests { get; set; }
    }
}
