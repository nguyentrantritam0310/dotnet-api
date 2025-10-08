using System;
using System.ComponentModel.DataAnnotations;

namespace dotnet_api.DTOs.POST
{
    public class ShiftAssignmentDTOPOST
    {
        [Required(ErrorMessage = "ID nhân viên là bắt buộc")]
        public string EmployeeID { get; set; }

        [Required(ErrorMessage = "ID ca làm việc là bắt buộc")]
        public int WorkShiftID { get; set; }

        [Required(ErrorMessage = "Ngày làm việc là bắt buộc")]
        public DateTime WorkDate { get; set; }
    }
}

