using System.ComponentModel.DataAnnotations;

namespace dotnet_api.Data.Enums
{
    public enum ConstructionStatusEnum
    {
        [Display(Name = "Chờ khởi công")]
        Pending,
        [Display(Name = "Đang thi công")]
        InProgress,
        [Display(Name = "Hoàn thành")]
        Completed,
        [Display(Name = "Tạm dừng")]
        Paused,
        [Display(Name = "Hủy bỏ")]
        Cancelled
    }
}
