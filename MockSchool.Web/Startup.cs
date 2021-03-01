using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MockSchool.Web.CustomerMiddlewares;
using MockSchool.Web.DataRepositories;
using MockSchool.Web.Models;

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

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddErrorDescriber<CustomerIdentityErrorDescriber>()
                    .AddEntityFrameworkStores<AppDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8; //密码最小长度
                options.Password.RequiredUniqueChars = 1; //允许最大重复字符数
                options.Password.RequireNonAlphanumeric = false; //至少包含一个非字母数字字符
                options.Password.RequireLowercase = true; //是否必须包含小写字母
                options.Password.RequireUppercase = false; //是否必须包含大写字母
                options.Password.RequireDigit = true; //是否必须包含数字
            });

            services.AddControllersWithViews(config =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

                config.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddXmlSerializerFormatters();

            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {



            if (env.IsDevelopment())
            {
                //环境变量配置了开发模式,不进入F5,也会进入此逻辑
                //显示异常信息中间件
                app.UseDeveloperExceptionPage();

            }
            ////演示环境/生产环境/用户体验测试
            else if (env.IsStaging() || env.IsProduction() || env.IsEnvironment("UAT"))
            {
                //异常捕获中间件
                app.UseExceptionHandler("/Error");

                //StateCode中间件->Mvc处理页面(200)->再到StatusCode中间件(404)
                //注 如果已经捕获ExceptionHandler,则不再进入Code

                //app.UseStatusCodePages(context =>
                //{
                //    var contextType = context.HttpContext.Response.ContentType;

                //    return Task.CompletedTask;
                //});

                //转跳到Notfound页面,再将状态更改为404,不更改请求地址
                //app.UseStatusCodePagesWithReExecute("/Error/{0}");

                //处理400~599之前的状态码,进行重定向(error/code),更改请求地址,所以最终显示的页面状态是200
                //app.UseStatusCodePagesWithRedirects("/Error/{0}");


                //使用带有错误代码的中间件
                //app.UseStatusCodePages();



            }

            //app.UseHttpsRedirection();

            //静态文件中间件
            app.UseStaticFiles();

            //身份验证中间件
            app.UseAuthentication();

            app.UseRouting();

            //授权中间件
            app.UseAuthorization();


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
