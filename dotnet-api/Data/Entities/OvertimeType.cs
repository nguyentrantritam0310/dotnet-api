namespace dotnet_api.Data.Entities
{
    public class OvertimeType
    {
        public int ID { get; set; }
        public string OvertimeTypeName { get; set; }
        public float coefficient { get; set; } 
        public ICollection<EmployeeRequests>? EmployeeRequests { get; set; }
    }
}
