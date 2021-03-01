using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchool.Web.CustomerMiddlewares
{
    public class CustomerIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordMismatch()
        {
            return new IdentityError { Code = nameof(PasswordMismatch), Description = "密码错误" };
        }

    }
}
