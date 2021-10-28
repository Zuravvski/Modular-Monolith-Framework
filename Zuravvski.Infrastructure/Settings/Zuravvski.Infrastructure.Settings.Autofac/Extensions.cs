using Autofac;
using Microsoft.Extensions.Configuration;

namespace Zuravvski.Infrastructure.Settings
{
    public static class Extensions
    {
        public static ContainerBuilder RegisterSettings<TSettings>(this ContainerBuilder builder,
                                                                   IConfiguration configuration,
                                                                   bool optional = false) where TSettings : class, new()
        {
            var settings = configuration.GetSettings<TSettings>(optional);

            if (settings is { })
            {
                builder.RegisterInstance(settings)
                    .As<TSettings>()
                    .SingleInstance()
                    .PreserveExistingDefaults();
            }

            return builder;
        }
    }
}
