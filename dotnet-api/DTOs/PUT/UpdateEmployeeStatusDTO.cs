using System.ComponentModel.DataAnnotations;
using dotnet_api.Data.Enums;

namespace dotnet_api.DTOs.PUT
{
    public class UpdateEmployeeStatusDTO
    {
        [Required(ErrorMessage = "Trạng thái là bắt buộc")]
        public EmployeeStatusEnum Status { get; set; }
    }
}
