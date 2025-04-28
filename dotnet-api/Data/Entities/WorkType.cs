namespace dotnet_api.Data.Entities
{
    public class WorkType
    {
        public int ID { get; set; }
        public string WorkTypeName { get; set; }
        public ICollection<WorkSubType> WorkSubTypes { get; set; }


    }
}
