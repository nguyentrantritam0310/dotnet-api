using System.ComponentModel.DataAnnotations;
using dotnet_api.Data.Enums;

namespace dotnet_api.DTOs.POST
{
    public class EmployeeDTOPOST
    {
        [Required(ErrorMessage = "Mã nhân viên là bắt buộc")]
        public string EmployeeCode { get; set; }

        [Required(ErrorMessage = "Họ và tên đệm là bắt buộc")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Tên nhân viên là bắt buộc")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Ngày vào làm là bắt buộc")]
        public DateTime JoinDate { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Giới tính là bắt buộc")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Chức danh là bắt buộc")]
        public int RoleID { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [MinLength(8, ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự")]
        public string Password { get; set; }

        public EmployeeStatusEnum Status { get; set; } = EmployeeStatusEnum.Active;
    }
}
