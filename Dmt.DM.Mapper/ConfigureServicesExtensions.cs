using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Dmt.DM.Mapper
{
    public static class ConfigureServicesExtensions
    {
        public static IServiceCollection AddCustomMapper(this IServiceCollection services)
        {
            var fileName = Assembly.GetExecutingAssembly().FullName;
            var assembly = Assembly.Load(fileName);
            services.AddAutoMapper(assembly);
            return services;
        }
    }
}
