using System;
using System.ComponentModel.DataAnnotations;

namespace dotnet_api.DTOs.PUT
{
    public class FamilyRelationDTOPUT
    {
        [Required(ErrorMessage = "ID là bắt buộc")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Tên người quan hệ là bắt buộc")]
        public string RelativeName { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu là bắt buộc")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc là bắt buộc")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "ID nhân viên là bắt buộc")]
        public string EmployeeID { get; set; }

        [Required(ErrorMessage = "Mối quan hệ là bắt buộc")]
        public string RelationShipName { get; set; }
    }
}
