using Dmt.DM.Application;
using Dmt.DM.Code;
using Dmt.DM.EntityFrameWork;
using Dmt.DM.IoCConfig;
using Dmt.DM.IoCConfig.Middleware;
using Dmt.DM.Mapper;
using Dmt.DM.UOW;
using Dmt.DM.Web.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using Dmt.DM.Web.Services;
using Newtonsoft.Json.Serialization;

namespace Dmt.DM.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
            AppConsts.AppRootPath = env.ContentRootPath;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           

            services.AddCustomOptions(Configuration)
                .AddUnitOfWork<HsDbContext>()
                .AddCustomServices()
                .AddCustomDbContext(Configuration)
                .AddCustomJwtBearer(Configuration)
                .AddCustomCors(Configuration)
                .AddCustomAntiforgery()
                .AddCustomApplication()
                .AddCustomMapper();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSignalR(options =>
            {
                // Faster pings for testing
                options.KeepAliveInterval = TimeSpan.FromSeconds(5);
                options.EnableDetailedErrors = true;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            // 启用内存缓存
            services.AddMemoryCache();
            //后台任务-定时执行
            services.AddHostedService<TimedHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetService<IDbInitializerService>();
                dbInitializer.Initialize();
                dbInitializer.SeedData();
            }

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            app.UseMiddleware<CustomExceptionHandlingMiddleware>();

            //signalr

            //backgroudwork  

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "upload")),
                RequestPath = "/upload"
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "GuideBook")),
                RequestPath = "/DataStatistics/GuideBook/GuideBook"
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "GuideInstruction")),
                RequestPath = "/DataStatistics/GuideInstruction/GuideInstruction"
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "GuideRegulation")),
                RequestPath = "/DataStatistics/GuideRegulation/GuideRegulation"
            });
            //app.UseRouting(); 

            app.UseAuthentication();

           
            app.UseCors("CorsPolicy");

            app.UseSignalR(routes =>
            {
                //routes.MapHub<DynamicChat>("/chathub");
                routes.MapHub<HubTChat>("/chathub");
            });
            //app.UseAuthorization();

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();
            //app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}/{id?}");
                //routes.MapRoute(
                //    name: "areas",
                //    template: "{Areas:exists}/{controller}/{action}/{id?}");

                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller}/{action=Index}/{id?}"
                );
                //routes.MapRoute(
                //    name: "default",
                //    template: "{controller=Login}/{action}/{id?}",
                //    defaults: new { controller = "Home", action = "Index" });

                //routes.MapRoute(
                //    name: "areaRoute",
                //    template: "{area:exists}/{controller}/{action}",
                //    defaults: new { action = "Index" });

                //routes.MapRoute(
                //    name: "api",
                //    template: "{controller}/{id?}");

            });
    
            //logger.AddLog4Net();
        }
    }
}
