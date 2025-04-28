using System.ComponentModel.DataAnnotations;

namespace dotnet_api.Data.Enums
{
    public enum ConstructionTypeEnum
    {
        [Display(Name = "Cầu đường")]
        RoadBridge,
        [Display(Name = "Dân dụng")]
        House,
        [Display(Name = "Công nghiệp")]
        Industrial,
        [Display(Name = "Thủy lợi")]
        Irrigation
    }
}
