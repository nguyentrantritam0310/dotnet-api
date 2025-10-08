namespace dotnet_api.Data.Entities
{
    public class Contract_Allowance
    {
        public int ContractID { get; set; }
        public int AllowanceID { get; set; }
        public decimal Value { get; set; }

        // Navigation properties
        public Contract Contract { get; set; }
        public Allowance Allowance { get; set; }
    }
}
