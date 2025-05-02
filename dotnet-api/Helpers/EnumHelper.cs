using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace dotnet_api.Helpers
{
    public static class EnumHelper
    {
        public static string GetDisplayName(Enum enumValue)
        {
            if (enumValue == null) return null;

            var type = enumValue.GetType();
            var memberInfo = type.GetMember(enumValue.ToString());

            if (memberInfo != null && memberInfo.Length > 0)
            {
                var attr = memberInfo[0].GetCustomAttribute<DisplayAttribute>();
                if (attr != null)
                {
                    return attr.Name;
                }
            }

            return enumValue.ToString();
        }
    }
}
