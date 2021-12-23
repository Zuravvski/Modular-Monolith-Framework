using Autofac;
using Zuravvski.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

namespace Zuravvski.Infrastructure.Persistence.Postgres
{
    public static class Extensions
    {
        public static ContainerBuilder UsePostgres(this ContainerBuilder builder, IConfiguration configuration)
        {
            builder.RegisterType<PostgresConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .InstancePerLifetimeScope();

            builder.RegisterOptions<PostgresOptions>(configuration);

            builder.RegisterType<DefaultSqlQueryRunner>()
                .As<ISqlQueryRunner>()
                .InstancePerLifetimeScope()
                .PreserveExistingDefaults();

            return builder;
        }
    }
}
