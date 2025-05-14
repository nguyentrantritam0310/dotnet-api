using dotnet_api.Data.Entities;

namespace dotnet_api.DTOs
{
    public class ImportOrderDTOPOST
    {
        public int ID { get; set; }
        public DateTime ImportDate { get; set; }
        public string Status { get; set; }

    }

    public class ImportOrderDTO
    {
        public int ID { get; set; }
        public DateTime ImportDate { get; set; }
        public List<ImportOrderEmployeeDTO> ImportOrderEmployee { get; set; }
        public string Status { get; set; }

    }
}
