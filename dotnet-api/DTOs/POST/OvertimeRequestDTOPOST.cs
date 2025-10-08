using System;
using System.ComponentModel.DataAnnotations;

namespace dotnet_api.DTOs.POST
{
    public class OvertimeRequestDTOPOST
    {
        [Required]
        public string VoucherCode { get; set; }
        [Required]
        public string EmployeeID { get; set; }
        [Required]
        public int OvertimeTypeID { get; set; }
        [Required]
        public int OvertimeFormID { get; set; }
        [Required]
        public DateTime StartDateTime { get; set; }
        [Required]
        public DateTime EndDateTime { get; set; }
        [Required]
        public string Reason { get; set; }
    }
}
