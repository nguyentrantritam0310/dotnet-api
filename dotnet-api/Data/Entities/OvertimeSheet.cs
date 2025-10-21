using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_api.Data.Entities
{
    public class OvertimeSheet
    {
        public int ID { get; set; }
        
        [Required]
        public string EmployeeID { get; set; }
        
        [Required]
        public string EmployeeName { get; set; }
        
        public decimal TotalOvertimeDays { get; set; }
        public decimal TotalOvertimeHours { get; set; }
        public decimal OvertimeSalary { get; set; }
        public decimal OvertimeCoefficient { get; set; }
        public DateTime OvertimeClosingDate { get; set; }
        public string OvertimeNotes { get; set; }
        
        // Status để đánh dấu đã chốt hay chưa
        public bool IsClosed { get; set; } = false;
        public DateTime? ClosedAt { get; set; }
        public string? ClosedBy { get; set; }
        
        // Foreign key
        public int PayrollID { get; set; }
        
        // Navigation properties
        public ApplicationUser Employee { get; set; }
        public Payroll Payroll { get; set; }
    }
}
