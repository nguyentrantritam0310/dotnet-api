namespace dotnet_api.Data.Entities
{
    public class Role
    {
        public int ID { get; set; }
        public string? RoleName { get; set; }

        // Navigation properties
        public ICollection<Employee>? Employees { get; set; }
    }
}
