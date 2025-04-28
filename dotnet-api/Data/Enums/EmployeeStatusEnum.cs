using System.ComponentModel.DataAnnotations;

namespace dotnet_api.Data.Enums
{
    public enum EmployeeStatusEnum
    {
        [Display(Name = "Đang làm việc")]
        Active,
        [Display(Name = "Nghỉ việc")]
        Inactive
    }

}
