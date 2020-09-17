using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Dmt.DM.Application
{
    /// <summary>
    /// 注册实现了IScopedAppService接口的类
    /// </summary>
    public static class ConfigureServicesExtensions
    {
        public static IServiceCollection AddCustomApplication(this IServiceCollection services)
        {
            var fileName = Assembly.GetExecutingAssembly().FullName;
            var assembly = Assembly.Load(fileName);
            var interfaces = assembly.DefinedTypes.Where(t =>
                t.IsInterface && t.ImplementedInterfaces.Any(r=>r.Name.Equals("IScopedAppService"))).ToList();
            var classes = assembly.DefinedTypes.Where(t =>
                t.IsClass && t.ImplementedInterfaces.Any(r => r.Name.Equals("IScopedAppService"))).ToList();
            foreach (var item in interfaces)
            {
                var find = classes.FirstOrDefault(t => t.ImplementedInterfaces.Any(r => r.Name.Equals(item.Name)));
                if (find != null)
                {
                    services.AddScoped(item, find);
                }
            }
            return services;
        }
    }
}
