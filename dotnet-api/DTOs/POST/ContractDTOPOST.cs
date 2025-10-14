using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using dotnet_api.Data.Enums;

namespace dotnet_api.DTOs.POST
{
    public class ContractDTOPOST
    {
        [Required(ErrorMessage = "Số hợp đồng là bắt buộc")]
        public string ContractNumber { get; set; }

        [Required(ErrorMessage = "Loại hợp đồng là bắt buộc")]
        public int ContractTypeID { get; set; }

        [Required(ErrorMessage = "ID nhân viên là bắt buộc")]
        public string EmployeeID { get; set; }

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

        public int ApproveStatus { get; set; } = (int)ApproveStatusEnum.Created;
        
        [JsonIgnore]
        public ApproveStatusEnum ApproveStatusEnum => (ApproveStatusEnum)ApproveStatus;
        public List<ContractAllowanceDTOPOST> Allowances { get; set; } = new List<ContractAllowanceDTOPOST>();
    }

    public class ContractAllowanceDTOPOST
    {
        [Required(ErrorMessage = "ID phụ cấp là bắt buộc")]
        public int AllowanceID { get; set; }

        [Required(ErrorMessage = "Giá trị phụ cấp là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá trị phụ cấp phải lớn hơn 0")]
        public decimal Value { get; set; }
    }
}

