using System.ComponentModel.DataAnnotations;

namespace dotnet_api.Data.Enums
{
    public enum MaterialTypeEnum
    {
        [Display(Name = "Vật liệu xây dựng cơ bản")]
        BasicBuildingMaterial,
        [Display(Name = "Vật tư hoàn thiện")]
        FinishingMaterial,
        [Display(Name = "Hệ thống điện - nước")]
        ElectricalWaterSystem,
        [Display(Name = "Cơ khí & kết cấu")]
        MechanicalStructure,
        [Display(Name = "Vật tư phụ trợ")]
        SupportingMaterial
    }

}
