using dotnet_api.Data.Entities;
using dotnet_api.Data.Enums;

namespace dotnet_api.DTOs
{
    public class ImportOrderEmployeeDTOPOST
    {
        public int ImportOrderId { get; set; }
        public string EmployeeID { get; set; }
        public string Role { get; set; }

    }

    public class ImportOrderEmployeeDTO
    {
        public int ImportOrderId { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Role { get; set; }

    }
}
