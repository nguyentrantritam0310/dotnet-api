using System.ComponentModel.DataAnnotations;

namespace dotnet_api.DTOs.POST
{
    public class AssignTaskToWorkShiftsRequest
    {
        [Required]
        public int ConstructionTaskID { get; set; }
        
        [Required]
        public List<string> EmployeeIds { get; set; }
        
        [Required]
        public List<int> WorkShiftIds { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }
    }
}

