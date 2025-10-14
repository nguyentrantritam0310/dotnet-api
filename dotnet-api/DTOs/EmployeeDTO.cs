using dotnet_api.Data.Enums;

namespace dotnet_api.DTOs
{
    public class EmployeeDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeName { get; set; } // FirstName + LastName
        public DateTime Birthday { get; set; }
        public DateTime JoinDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public EmployeeStatusEnum Status { get; set; }
        public string StatusName { get; set; }
    }
}
