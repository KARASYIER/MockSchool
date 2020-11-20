using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MockSchool.Web.Extensions
{
    public static class EnumExtension
    {
        public static string GetDisplayName(this Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attr = memInfo[0].GetCustomAttributes(typeof(DisplayAttribute), true);

                if (attr != null && attr.Length > 0)
                {
                    return ((DisplayAttribute)attr[0]).Name;
                }
            }

            return en.ToString();
        }
    }
}
