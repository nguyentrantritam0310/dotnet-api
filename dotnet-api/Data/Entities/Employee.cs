using dotnet_api.Data.Enums;

namespace dotnet_api.Data.Entities
{
    public class Employee
    {
        public int ID { get; set; }
        public int RoleID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EmployeeStatusEnum Status { get; set; }

        // Navigation properties
        public Role Role { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<ExportOrder> ExportOrders { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<ConstructionPlan> ConstructionPlans { get; set; }
        public ICollection<ImportOrder> ImportOrders { get; set; }

    }
}
