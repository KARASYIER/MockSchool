using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MockSchool.Web.Models;
using System.Linq;

namespace MockSchool.Web.DataRepositories
{
    /// <summary>
    /// 可利用迁移创建数据库
    /// 需要添加EntityFramework.Tools
    /// Nuget控制台指令
    /// Add-Migration
    /// 如果继承IdentityDbContext系统类,则生成aspidentity相关数据库
    /// 
    /// 使用迁移,可以用代码的形式保存修改的数据库,避免直接修改数据库后导致不同开发人之间的数据库与Model类的不同步
    /// 
    /// 
    /// </summary>
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }

        /// <summary>
        /// 种子数据,在利用迁移创建数据库,添加种子数据的方法
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Seed();

            var foreignKeys = builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());

            foreach(var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
