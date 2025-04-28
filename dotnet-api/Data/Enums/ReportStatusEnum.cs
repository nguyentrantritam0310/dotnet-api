using System.ComponentModel.DataAnnotations;

namespace dotnet_api.Data.Enums
{
    public enum ReportStatusEnum
    {
        [Display(Name = "Chờ duyệt")]
        Pending,
        [Display(Name = "Đã duyệt")]
        Approved,
        [Display(Name = "Từ chối")]
        Rejected
    }
}
