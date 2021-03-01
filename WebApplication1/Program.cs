using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApplication1
{
    //public class Program
    //{
    //    public static void Main(string[] args)
    //    {
    //        CreateHostBuilder(args).Build().Run();
    //    }

    //    public static IHostBuilder CreateHostBuilder(string[] args) =>
    //        Host.CreateDefaultBuilder(args)
    //            .ConfigureWebHostDefaults(webBuilder =>
    //            {
    //                webBuilder.UseStartup<Startup>();
    //            });
    //}

    class Program
    {
        static void Main()
        {
            Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>())
            .Build()
            .Run();
        }
    }

    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment hostingEnvironment, IWebHostEnvironment webHostEnvironment)
        {
            Debug.Assert(configuration != null);
            Debug.Assert(hostingEnvironment != null);
            Debug.Assert(webHostEnvironment != null);
            Debug.Assert(ReferenceEquals(hostingEnvironment, webHostEnvironment));
        }
        public void Configure(IApplicationBuilder app) { }
    }
}
