using System.ComponentModel.DataAnnotations;

namespace dotnet_api.Data.Enums
{
    public enum AttendanceStatusEnum { 
        [Display(Name = "Có mặt")]
        Present,
        [Display(Name = "Vắng mặt")]
        Absent,
        [Display(Name = "Quên checkin")]
        MissCheckIn,
        [Display(Name = "Quên checkout")]
        MissCheckout,

    }
}
