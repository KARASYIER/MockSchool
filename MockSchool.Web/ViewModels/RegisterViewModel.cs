using MockSchool.Web.CustomerMiddlewares.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchool.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "电子邮箱格式不正确")]
        [Display(Name = "邮箱地址")]
        [ValidEmailDomain("163.com", ErrorMessage = "必须使用163的邮箱")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "两次密码输入不一致，请重新输入")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "城市")]
        public string City { get; set; }
    }
}
