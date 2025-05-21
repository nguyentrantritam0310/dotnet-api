using System.ComponentModel.DataAnnotations;

namespace dotnet_api.DTOs
{
    public class ApplicationUserDTO
    {
        public string Id { get; set; }
        public string EmployeeName { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
    }
} 