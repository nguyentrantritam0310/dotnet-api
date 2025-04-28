namespace dotnet_api.Data.Entities
{
    public class WorkAttribute
    {
        public int ID { get; set; }
        public string WorkAttributeName { get; set; }
        public int UnitOfMeasurementID { get; set; }

        public UnitofMeasurement UnitOfMeasurement { get; set; }
        public ICollection<WorkSubTypeVariant_WorkAttribute> WorkSubTypeVariant_WorkAttributes { get; set; }

    }
}
