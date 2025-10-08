using System.ComponentModel.DataAnnotations;

namespace dotnet_api.DTOs.POST
{
    public class LeaveRequestDTOPOST
    {
        [Required(ErrorMessage = "Số phiếu là bắt buộc")]
        public string VoucherCode { get; set; }

        [Required(ErrorMessage = "Mã nhân viên là bắt buộc")]
        public string EmployeeID { get; set; }

        [Required(ErrorMessage = "Loại nghỉ phép là bắt buộc")]
        public int LeaveTypeID { get; set; }

        [Required(ErrorMessage = "Ca làm việc là bắt buộc")]
        public int WorkShiftID { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu là bắt buộc")]
        public DateTime StartDateTime { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc là bắt buộc")]
        public DateTime EndDateTime { get; set; }

        [Required(ErrorMessage = "Lý do là bắt buộc")]
        [StringLength(500, ErrorMessage = "Lý do không được vượt quá 500 ký tự")]
        public string Reason { get; set; }
    }
}
