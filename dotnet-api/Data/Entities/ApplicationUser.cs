using Microsoft.AspNetCore.Identity;
using dotnet_api.Data.Enums;

namespace dotnet_api.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        
        // Fields from Employee
        public int RoleID { get; set; }
        public string? Phone { get; set; }
        public EmployeeStatusEnum Status { get; set; }

        // Navigation properties
        public Role Role { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<ExportOrder> ExportOrders { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<ConstructionPlan> ConstructionPlans { get; set; }
        public ICollection<ImportOrder> ImportOrders { get; set; }
        public ICollection<ImportOrderEmployee> ImportOrderEmployees { get; set; }
    }
} 