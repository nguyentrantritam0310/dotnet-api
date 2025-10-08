namespace dotnet_api.Data.Entities
{
    public class Employee_FamilyRelation
    {
        public int FamilyRelationID { get; set; }
        public string EmployeeID { get; set; }
        public string RelationShipName { get; set; }

        // Navigation properties
        public FamilyRelation FamilyRelation { get; set; }
        public ApplicationUser Employee { get; set; }
    }
}
