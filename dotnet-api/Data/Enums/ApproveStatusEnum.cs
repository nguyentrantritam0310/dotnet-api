using System.ComponentModel.DataAnnotations;

namespace dotnet_api.Data.Enums
{
    public enum ApproveStatusEnum
    {
        [Display(Name = "Tạo mới")]
        Created,      

        [Display(Name = "Chờ duyệt")]
        Pending,  

        [Display(Name = "Đã duyệt")]
        Approved,  

        [Display(Name = "Từ chối")]
        Rejected     
    }
}
