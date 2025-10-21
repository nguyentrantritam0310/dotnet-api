using System.ComponentModel.DataAnnotations;

namespace dotnet_api.DTOs
{
    public class UpdateStatusDTO
    {
        [Required(ErrorMessage = "Trạng thái là bắt buộc")]
        public int Status { get; set; }
    }
}
