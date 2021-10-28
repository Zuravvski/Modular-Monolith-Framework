using System;
using System.Threading.Tasks;
using Autofac;

namespace Zuravvski.Infrastructure.Cqrs.Queries
{
    internal sealed class InMemoryQueryBus : IQueryBus
    {
        private readonly IComponentContext _context;

        public InMemoryQueryBus(IComponentContext context)
        {
            _context = context;
        }

        public async Task<TResult> Query<TResult>(IQuery<TResult> query)
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(query), "Command cannot be null");
            }

            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = _context.Resolve(handlerType);
            return await handler.Handle((dynamic)query);
        }
    }
}
