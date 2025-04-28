namespace dotnet_api.Data.Entities
{
    public class WorkSubType
    {
        public int ID { get; set; }
        public string WorkSubTypeName { get; set; }
        public int WorkTypeID { get; set; }
        public WorkType WorkType { get; set; }
        public ICollection<WorkSubTypeVariant> WorkSubTypeVariants { get; set; }


    }
}
