using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Zuravvski.Infrastructure.Exceptions
{
    public static class Extensions
    {
        private static readonly string NamespaceRegex = @"(?:\.[a-zA-Z0-9]+)*";

        public static IServiceCollection AddErrorHandler(this IServiceCollection services)
        {
            services.AddTransient<ErrorHandlerMiddleware>();
            services.AddSingleton<IExceptionMapperRegistry, DefaultExceptionMapperRegistry>();
            return services;
        }

        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
            return app;
        }

        public static IApplicationBuilder UseErrorHandler<TFallbackExceptionMapper>(this IApplicationBuilder app)
            where TFallbackExceptionMapper : class, IExceptionToResponseMapper, new()
        {
            app.UseErrorHandler();
            app.ApplicationServices.GetService<IExceptionMapperRegistry>().RegisterFallbackMapper<TFallbackExceptionMapper>();
            return app;
        }

        public static IApplicationBuilder RegisterExceptionMapperForThisModule<TExceptionMapper>(this IApplicationBuilder app)
            where TExceptionMapper : class, IExceptionToResponseMapper, new()
        {
            var @namespace = Assembly.GetCallingAssembly().GetName().Name.Replace(".", @"\.") + NamespaceRegex;
            return app.RegisterExceptionMapper<TExceptionMapper>(@namespace);
        }

        public static IApplicationBuilder RegisterExceptionMapper<TExceptionMapper>(this IApplicationBuilder app, string namespaceRegex = null)
            where TExceptionMapper : class, IExceptionToResponseMapper, new()
        {
            app.ApplicationServices.GetService<IExceptionMapperRegistry>().Register<TExceptionMapper>(namespaceRegex);
            return app;
        }

        internal static bool IsDefault<T>(this T obj)
            => default(T).Equals(obj);
    }
}
