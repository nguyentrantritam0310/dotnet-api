using System;
using System.Collections.Generic;

namespace dotnet_api.Data.Entities
{
    public class Contract
    {
        public int ID { get; set; }
        public string ContractNumber { get; set; }
        public int ContractTypeID { get; set; }
        public int ContractFormID { get; set; }
        public string EmployeeID { get; set; } // FK to ApplicationUser
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal ContractSalary { get; set; }
        public decimal InsuranceSalary { get; set; }
        public string ApproveStatus { get; set; }

        // Navigation properties
        public ApplicationUser Employee { get; set; }
        public ContractType ContractType { get; set; }
        public ContractForm ContractFormEntity { get; set; }
        public ICollection<Contract_Allowance> ContractAllowances { get; set; }
    }
}
