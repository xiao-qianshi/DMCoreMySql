using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Dmt.DM.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(log =>
                {
                    //log.AddFilter("System", LogLevel.Warning);
                    log.AddFilter("Microsoft", LogLevel.Warning);
                    log.AddLog4Net("log4net.config", true);
                })
                .UseStartup<Startup>();
    }
}
