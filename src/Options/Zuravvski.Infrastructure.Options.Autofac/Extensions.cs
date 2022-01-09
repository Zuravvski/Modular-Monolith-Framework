using Autofac;
using Microsoft.Extensions.Configuration;

namespace Zuravvski.Infrastructure.Options.Autofac
{
    public static class Extensions
    {
        public static ContainerBuilder RegisterOptions<TOptions>(this ContainerBuilder builder,
                                                                 IConfiguration configuration,
                                                                 string sectionName = null,
                                                                 bool optional = false) where TOptions : class, new()
        {
            var settings = configuration.GetOptions<TOptions>(sectionName, optional);

            if (settings is not null)
            {
                builder.RegisterInstance(settings)
                    .As<TOptions>()
                    .SingleInstance()
                    .PreserveExistingDefaults();
            }

            return builder;
        }
    }
}
