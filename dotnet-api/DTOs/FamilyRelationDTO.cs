using System;

namespace dotnet_api.DTOs
{
    public class FamilyRelationDTO
    {
        public int ID { get; set; }
        public string RelativeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string RelationShipName { get; set; }
    }
}
