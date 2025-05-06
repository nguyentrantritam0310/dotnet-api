using System.ComponentModel.DataAnnotations;

namespace dotnet_api.DTOs
{
    public class AssignRoleDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }
    }
} 