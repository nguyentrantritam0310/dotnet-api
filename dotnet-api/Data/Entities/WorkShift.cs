namespace dotnet_api.Data.Entities
{
    public class EmployeeRequest
    {
        public int ID { get; set; }
        public string ShiftName { get; set; }
        public ICollection<ShiftDetail> ShiftDetails { get; set; }
        public ICollection<ShiftAssignment> ShiftAssignments { get; set; }

    }
}
