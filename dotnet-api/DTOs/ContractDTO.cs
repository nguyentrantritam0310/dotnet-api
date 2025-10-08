using System;
using System.Collections.Generic;

namespace dotnet_api.DTOs
{
    public class ContractDTO
    {
        public int ID { get; set; }
        public string ContractNumber { get; set; }
        public int ContractTypeID { get; set; }
        public string ContractTypeName { get; set; }
        public int ContractFormID { get; set; }
        public string ContractFormName { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal ContractSalary { get; set; }
        public decimal InsuranceSalary { get; set; }
        public string ApproveStatus { get; set; }
        public List<ContractAllowanceDTO> Allowances { get; set; }
    }
}