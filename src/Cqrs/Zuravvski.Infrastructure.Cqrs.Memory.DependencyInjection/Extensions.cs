using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Zuravvski.Infrastructure.Cqrs.Abstractions.Commands;
using Zuravvski.Infrastructure.Cqrs.Abstractions.Queries;

namespace Zuravvski.Infrastructure.Cqrs.Memory.DependencyInjection
{
    public static class Extensions
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services, Assembly assembly = null)
        {
            var finalAssembly = assembly ?? Assembly.GetCallingAssembly();

            return services
                .AddCqrsCommands(finalAssembly)
                .AddCqrsQueries(finalAssembly);
        }
        public static IServiceCollection AddCqrsCommands(this IServiceCollection services, Assembly assembly = null)
        {
            var finalAssembly = assembly ?? Assembly.GetCallingAssembly();

            services.Scan(s => s
                // Non-returning command handlers
                .FromAssemblies(assembly)
                .AddClasses(types => types.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                // Returning command handlers
                .AddClasses(types => types.AssignableTo(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );


            services.AddSingleton<ICommandBus, InMemoryCommandBus>();
            return services;
        }

        public static IServiceCollection AddCqrsQueries(this IServiceCollection services, Assembly assembly = null)
        {
            var finalAssembly = assembly ?? Assembly.GetCallingAssembly();

            services.Scan(s => s
                .FromAssemblies(assembly)
                .AddClasses(types => types.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
            );

            services.AddSingleton<IQueryBus, InMemoryQueryBus>();
            return services;
        }
    }
}

