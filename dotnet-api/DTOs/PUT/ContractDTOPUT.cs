using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dotnet_api.DTOs.PUT
{
    public class ContractDTOPUT
    {
        [Required(ErrorMessage = "ID là bắt buộc")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Số hợp đồng là bắt buộc")]
        public string ContractNumber { get; set; }

        [Required(ErrorMessage = "Loại hợp đồng là bắt buộc")]
        public int ContractTypeID { get; set; }

        [Required(ErrorMessage = "Hình thức hợp đồng là bắt buộc")]
        public int ContractFormID { get; set; }

        [Required(ErrorMessage = "ID nhân viên là bắt buộc")]
        public string EmployeeID { get; set; }

        [Required(ErrorMessage = "Trạng thái là bắt buộc")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu là bắt buộc")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc là bắt buộc")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Lương hợp đồng là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Lương hợp đồng phải lớn hơn 0")]
        public decimal ContractSalary { get; set; }

        [Required(ErrorMessage = "Lương bảo hiểm là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Lương bảo hiểm phải lớn hơn 0")]
        public decimal InsuranceSalary { get; set; }

        public string ApproveStatus { get; set; }
        public List<ContractAllowanceDTOPUT> Allowances { get; set; } = new List<ContractAllowanceDTOPUT>();
    }

    public class ContractAllowanceDTOPUT
    {
        public int ContractID { get; set; }

        [Required(ErrorMessage = "ID phụ cấp là bắt buộc")]
        public int AllowanceID { get; set; }

        [Required(ErrorMessage = "Giá trị phụ cấp là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá trị phụ cấp phải lớn hơn 0")]
        public decimal Value { get; set; }
    }
}

