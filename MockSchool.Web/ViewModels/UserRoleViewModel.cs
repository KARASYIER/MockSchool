using System.ComponentModel.DataAnnotations;

namespace MockSchool.Web.ViewModels
{
    public class UserRoleViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        
        public bool IsSelected { get; set; }
    }
}
