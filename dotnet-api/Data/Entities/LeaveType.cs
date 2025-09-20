namespace dotnet_api.Data.Entities
{
    public class LeaveType
    {
        public int ID { get; set; }
        public string LeaveTypeName { get; set; }
        public ICollection<EmployeeRequests>? EmployeeRequests { get; set; }
    }
}
