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
            //services.AddSingleton<IStudentRepository, SQLStudentRpository>();//Ӧ�ó�����������һ��,һֱ����,ֱ������������
            services.AddScoped<IStudentRepository, SQLStudentRpository>();//ÿ�����󴴽���ʵ��,��ͬһ��HTTP������,ʹ����ͬʵ��
            //services.AddTransient<IStudentRepository, SQLStudentRpository>();//ÿ��HTTP���󴴽���ʵ��,��ͬһ��HTTP������Ҳʹ����ʵ��

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {



            if (env.IsDevelopment())
            {
                //�������������˿���ģʽ,������F5,Ҳ�������߼�
                //��ʾ�쳣��Ϣ�м��
                //app.UseDeveloperExceptionPage();

            }
            //else if (env.IsStaging() || env.IsProduction() || env.IsEnvironment("UAT"))
            //{
            //    app.UseExceptionHandler("/Error");
            //    app.UseStatusCodePagesWithReExecute("Error/{0}");
            //}
            else
            {
                //��������,��Ӧ����ʾ������Ϣ

                //StateCode�м��->Mvc����ҳ��(200)->�ٵ�StatusCode�м��(404)
                app.UseStatusCodePagesWithReExecute("/Error/{0}");

                //����400~599֮ǰ��״̬��,�����ض���(error/code),����������ʾ��ҳ��״̬��200
                app.UseStatusCodePagesWithRedirects("/Error/{0}");

                //�쳣�����м��
                //app.UseExceptionHandler("/Error");

                //ʹ�ô��д��������м��,��ʵ��
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
