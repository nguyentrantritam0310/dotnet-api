namespace dotnet_api.Data.Entities
{
    public class WorkSubTypeVariant_WorkAttribute
    {
        public int WorkSubTypeVariantID { get; set; }
        public int WorkAttributeID { get; set; }
        public string? Value { get; set; }
        // Navigation properties
        public WorkSubTypeVariant WorkSubTypeVariant { get; set; }
        public WorkAttribute WorkAttribute { get; set; }
    }
}
