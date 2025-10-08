using System;
using System.Collections.Generic;

namespace dotnet_api.Data.Entities
{
    public class FamilyRelation
    {
        public int ID { get; set; }
        public string RelativeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Employee_FamilyRelation> EmployeeFamilyRelations { get; set; }
    }
}
