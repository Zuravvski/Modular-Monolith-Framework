using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zuravvski.Infrastructure.Options;

namespace Zuravvski.Infrastructure.Persistence.Postgres.Autofac
{
    public static class Extensions
    {
        public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISqlConnectionFactory, PostgresConnectionFactory>();
            services.AddScoped<ISqlQueryRunner, DefaultSqlQueryRunner>();
            services.AddSingleton(configuration.GetOptions<PostgresOptions>());
            return services;
        }
    }
}
