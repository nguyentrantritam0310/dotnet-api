using dotnet_api.Data.Entities;

namespace dotnet_api.DTOs
{
    public class WorkSubTypeDTO
    {
        public int ID { get; set; }
        public string WorkSubTypeName { get; set; }
        public int WorkTypeID { get; set; }
        public WorkType WorkType { get; set; }
    }
}
