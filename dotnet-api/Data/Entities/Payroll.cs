using System;

namespace dotnet_api.Data.Entities
{
    public class Payroll
    {
        public int ID { get; set; }
        public string EmployeeID { get; set; }
        public string ContractType { get; set; }
        public decimal ContractSalary { get; set; }
        public decimal InsuranceSalary { get; set; }
        public decimal TotalContractSalary { get; set; }
        public decimal DailySalary { get; set; }
        public decimal LeaveSalary { get; set; }
        public decimal ActualSalary { get; set; }
        public decimal OvertimeSalary { get; set; }
        public decimal EatAllowance { get; set; }
        public decimal PetrolAllowance { get; set; }
        public decimal MealAllowance { get; set; }
        public decimal TotalAllowance { get; set; }
        public decimal SocialInsuranceEmployee { get; set; }
        public decimal HealthInsuranceEmployee { get; set; }
        public decimal UnemploymentInsuranceEmployee { get; set; }
        public decimal SocialInsuranceEmployer { get; set; }
        public decimal HealthInsuranceEmployer { get; set; }
        public decimal UnemploymentInsuranceEmployer { get; set; }
        public decimal UnionFee { get; set; }
        public decimal GrossIncome { get; set; }
        public decimal TaxableIncome { get; set; }
        public decimal PersonalDeduction { get; set; }
        public decimal DependentDeduction { get; set; }
        public decimal Bonus { get; set; }
        public decimal OtherIncome { get; set; }
        public decimal PersonalIncomeTax { get; set; }
        public decimal NetIncome { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal NetPay { get; set; }
        public DateTime PayrollClosingDate { get; set; }
        public string PayrollNotes { get; set; }

        // Navigation properties
        public ApplicationUser Employee { get; set; }
    }
}
