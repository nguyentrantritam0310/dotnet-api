using System.ComponentModel.DataAnnotations;

namespace dotnet_api.Data.Enums
{
    public enum ImportOrderRoleEnum
    {
        [Display(Name = "Người lập kế hoạch")]
        Planner,
        [Display(Name = "Người nhận")]
        Receiver
    }
}
