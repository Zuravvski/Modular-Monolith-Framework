using Autofac;
using Microsoft.Extensions.Configuration;
using Zuravvski.Infrastructure.Options.Autofac;

namespace Zuravvski.Infrastructure.Persistence.Postgres.Autofac
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
