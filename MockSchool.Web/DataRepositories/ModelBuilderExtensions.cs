using Microsoft.EntityFrameworkCore;
using MockSchool.Web.DataRepositories.EnumTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSchool.Web.DataRepositories
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// 数据迁移时创建种子数据
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(new Student
            {
                Id = 1,
                Name = "Karas",
                Major = MajorEnum.ComputerScience,
                Email = "cy85818@163.com"

            });

            modelBuilder.Entity<Student>().HasData(new Student
            {
                Id = 2,
                Name = "Karasyier",
                Major = MajorEnum.Mathematics,
                Email = "karasyier@hotmail.com"

            });
        }
    }
}
