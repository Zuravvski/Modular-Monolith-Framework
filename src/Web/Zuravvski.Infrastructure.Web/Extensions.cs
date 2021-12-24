using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Zuravvski.Infrastructure.Web
{
    public static class Extensions
    {
        public static IServiceCollection RouteModuleControllers(this IServiceCollection services)
        {
            services.AddMvcCore().AddApplicationPart(Assembly.GetCallingAssembly()).AddControllersAsServices();
            return services;
        }
    }
}
