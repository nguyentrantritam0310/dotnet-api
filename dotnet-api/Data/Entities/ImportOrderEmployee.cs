using dotnet_api.Data.Enums;
namespace dotnet_api.Data.Entities
{
    public class ImportOrderEmployee
    {
        public int ImportOrderId { get; set; }
        public ImportOrder ImportOrder { get; set; }

        public string EmployeeID { get; set; }
        public ApplicationUser Employee { get; set; }

        public ImportOrderRoleEnum Role { get; set; }
    }
}
