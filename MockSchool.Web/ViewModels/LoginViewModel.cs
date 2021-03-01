using Microsoft.AspNetCore.Mvc;
using MockSchool.Web.CustomerMiddlewares.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchool.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email必须填写")]
        [EmailAddress]
        [Remote("IsEmailUsed", "Account")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住我")]
        public bool RememberMe { get; set; }
    }
}
