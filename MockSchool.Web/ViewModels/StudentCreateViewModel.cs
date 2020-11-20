using Microsoft.AspNetCore.Http;
using MockSchool.Web.DataRepositories.EnumTypes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MockSchool.Web.ViewModels
{
    public class StudentCreateViewModel
    {

        [Display(Name = "姓名")]
        [Required(ErrorMessage = "必须输入姓名"), MaxLength(10, ErrorMessage = "姓名最大长度10个 ")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "主修科目")]
        public MajorEnum Major { get; set; }

        [Display(Name = "电子邮箱")]
        [RegularExpression(@"^[a-zA-z0-9_.+-]+@[a-zA-z0-9-]+\.[a-zA-z0-9-.]+$", ErrorMessage = "电子邮箱格式不正确")]
        public string Email { get; set; }

        [Display(Name = "头像")]
        public List<IFormFile> Photos { get; set; }
    }


}
