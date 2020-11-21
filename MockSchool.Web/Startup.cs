using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MockSchool.Web.DataRepositories;

namespace MockSchool.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddIdentity<IdentityUser, IdentityRole>()
            //        .AddEntityFrameworkStores<AppDbContext>();
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MockSchoolDBConnection")));
            //services.AddSingleton<IStudentRepository, SQLStudentRpository>();//应用程序启动创建一次,一直存在,直到服务器重启
            services.AddScoped<IStudentRepository, SQLStudentRpository>();//每次请求创建新实例,在同一个HTTP请求中,使用相同实例
            //services.AddTransient<IStudentRepository, SQLStudentRpository>();//每次HTTP请求创建新实例,在同一个HTTP请求中也使用新实例

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {



            if (env.IsDevelopment())
            {
                //环境变量配置了开发模式,不进入F5,也会进入此逻辑
                //显示异常信息中间件
                //app.UseDeveloperExceptionPage();

            }
            //else if (env.IsStaging() || env.IsProduction() || env.IsEnvironment("UAT"))
            //{
            //    app.UseExceptionHandler("/Error");
            //    app.UseStatusCodePagesWithReExecute("Error/{0}");
            //}
            else
            {
                //生产环境,不应该显示错误信息

                //StateCode中间件->Mvc处理页面(200)->再到StatusCode中间件(404)
                app.UseStatusCodePagesWithReExecute("/Error/{0}");

                //处理400~599之前的状态码,进行重定向(error/code),所以最终显示的页面状态是200
                app.UseStatusCodePagesWithRedirects("/Error/{0}");

                //异常捕获中间件
                //app.UseExceptionHandler("/Error");

                //使用带有错误代码的中间件,不实用
                //app.UseStatusCodePages();

                 //app.UseExceptionHandler("/Error/{0}");



            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
