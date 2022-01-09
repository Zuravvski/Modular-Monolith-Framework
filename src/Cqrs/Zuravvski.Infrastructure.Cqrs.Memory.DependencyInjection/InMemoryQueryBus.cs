using Microsoft.Extensions.DependencyInjection;
using Zuravvski.Infrastructure.Cqrs.Abstractions.Queries;

namespace Zuravvski.Infrastructure.Cqrs.Memory.DependencyInjection
{
    internal sealed class InMemoryQueryBus : IQueryBus
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public InMemoryQueryBus(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<TResult> Query<TResult>(IQuery<TResult> query)
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(IQuery<TResult>), "Query cannot be null");
            }

            using var scope = _serviceScopeFactory.CreateScope();
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            var handler = scope.ServiceProvider.GetRequiredService(handlerType);
            return await(Task<TResult>)handlerType
               .GetMethod(nameof(IQueryHandler<IQuery<TResult>, TResult>.Handle))
               ?.Invoke(handler, new[] { query });
        }
    }
}
