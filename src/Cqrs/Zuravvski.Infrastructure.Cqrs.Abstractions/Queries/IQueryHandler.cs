using System.Threading.Tasks;

namespace Zuravvski.Infrastructure.Cqrs.Abstractions.Queries
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : class, IQuery<TResult>
    {
        public Task<TResult> Handle(TQuery query);
    }
}
