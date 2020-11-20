using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchool.Web.ViewModels
{
    public class StudentEditViewModel : StudentCreateViewModel
    {
        public int Id { get; set; }


        /// <summary>
        /// 已经存在的头像路径
        /// </summary>
        public string ExistintPhotoPath { get; set; }
    }
}
