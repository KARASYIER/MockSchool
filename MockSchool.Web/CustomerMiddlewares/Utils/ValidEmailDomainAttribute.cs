using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchool.Web.CustomerMiddlewares.Utils
{
    /// <summary>
    /// 验证指定的邮箱域名
    /// </summary>
    public class ValidEmailDomainAttribute : ValidationAttribute
    {
        private readonly string allowedDomain;

        public ValidEmailDomainAttribute(string allowedDomain)
        {
            this.allowedDomain = allowedDomain;
        }

        public override bool IsValid(object value)
        {
            string[] strings = value.ToString().Split('@');

            return strings[1].ToLower() == this.allowedDomain.ToLower();
        }
    }
}
