using System;

namespace dotnet_api.DTOs
{
    public class Material_ExportOrderDTO
    {
        public int ID { get; set; }
        public int MaterialID { get; set; }
        public int ExportOrderID { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }
    }
}

