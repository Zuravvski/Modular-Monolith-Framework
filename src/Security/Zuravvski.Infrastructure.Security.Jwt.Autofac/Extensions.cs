using Autofac;
using Zuravvski.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

namespace Zuravvski.Infrastructure.Security.Jwt
{
    public static class Extensions
    {
        public static ContainerBuilder UseJwt(this ContainerBuilder builder, IConfiguration configuration)
        {
            builder.RegisterInstance(configuration.GetOptions<JwtOptions>()).SingleInstance();
            builder.RegisterType<JwtHandler>().As<IJwtHandler>().InstancePerLifetimeScope();
            return builder;
        }
    }
}
