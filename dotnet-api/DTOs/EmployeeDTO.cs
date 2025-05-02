using dotnet_api.Data.Enums;

namespace dotnet_api.DTOs
{
    public class EmployeeDTO
    {
        public int ID { get; set; }
        public int RoleID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EmployeeStatusEnum Status { get; set; }

    }
}
