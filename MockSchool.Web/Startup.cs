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
            //services.AddSingleton<IStudentRepository, SQLStudentRpository>();//Ӧ�ó�����������һ��,һֱ����,ֱ������������
            services.AddScoped<IStudentRepository, SQLStudentRpository>();//ÿ�����󴴽���ʵ��,��ͬһ��HTTP������,ʹ����ͬʵ��
            //services.AddTransient<IStudentRepository, SQLStudentRpository>();//ÿ��HTTP���󴴽���ʵ��,��ͬһ��HTTP������Ҳʹ����ʵ��

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddErrorDescriber<CustomerIdentityErrorDescriber>()
                    .AddEntityFrameworkStores<AppDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8; //������С����
                options.Password.RequiredUniqueChars = 1; //��������ظ��ַ���
                options.Password.RequireNonAlphanumeric = false; //���ٰ���һ������ĸ�����ַ�
                options.Password.RequireLowercase = true; //�Ƿ�������Сд��ĸ
                options.Password.RequireUppercase = false; //�Ƿ���������д��ĸ
                options.Password.RequireDigit = true; //�Ƿ�����������
            });

            services.AddControllersWithViews(config =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

                config.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddXmlSerializerFormatters();

            services.AddMiniProfiler().AddEntityFramework(); ;

            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //�������������˿���ģʽ,������F5,Ҳ�������߼�
                //��ʾ�쳣��Ϣ�м��
                app.UseDeveloperExceptionPage();

            }
            ////��ʾ����/��������/�û��������
            else if (env.IsStaging() || env.IsProduction() || env.IsEnvironment("UAT"))
            {
                //�쳣�����м��
                app.UseExceptionHandler("/Error");

                //StateCode�м��->Mvc����ҳ��(200)->�ٵ�StatusCode�м��(404)
                //ע ����Ѿ�����ExceptionHandler,���ٽ���Code

                //app.UseStatusCodePages(context =>
                //{
                //    var contextType = context.HttpContext.Response.ContentType;

                //    return Task.CompletedTask;
                //});

                //ת����Notfoundҳ��,�ٽ�״̬����Ϊ404,�����������ַ
                //app.UseStatusCodePagesWithReExecute("/Error/{0}");

                //����400~599֮ǰ��״̬��,�����ض���(error/code),���������ַ,����������ʾ��ҳ��״̬��200
                //app.UseStatusCodePagesWithRedirects("/Error/{0}");


                //ʹ�ô��д��������м��
                //app.UseStatusCodePages();



            }

            //app.UseHttpsRedirection();

            //��̬�ļ��м��
            app.UseStaticFiles();

            app.UseMiniProfiler();

            //�����֤�м��
            app.UseAuthentication();

            app.UseRouting();

            //��Ȩ�м��
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
