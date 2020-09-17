using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Dmt.DM.EntityFrameWork
{
    public class HsDbContextFactory: IDesignTimeDbContextFactory<HsDbContext>
    {
        public HsDbContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();
            Console.WriteLine($"Using `{basePath}` as the BasePath");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<HsDbContext>();
            var connectionString = configuration.GetConnectionString("Default")
                /*.Replace("|DataDirectory|", Path.Combine(basePath, "wwwroot", "app_data"))*/;
            builder.UseMySql(connectionString);
            return new HsDbContext(builder.Options);
        }
    }
}
