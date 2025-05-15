using System;
using System.Collections.Generic;

namespace dotnet_api.DTOs
{
    public class ExportOrderDTO
    {
        public int ID { get; set; }
        public DateTime ExportDate { get; set; }
        public string EmployeeName { get; set; }
        public string ConstructionItemName { get; set; }
        public string ConstructionName { get; set; }
        public List<Material_ExportOrderDTO> Material_ExportOrders { get; set; }
        public List<string> materialName { get; set; }
    }

    public class ExportOrderDTOPOST
    {
        public int ID { get; set; }
        public string EmployeeID { get; set; }
        public int ConstructionItemID { get; set; }
        public DateTime ExportDate { get; set; }
    }
}
