using System.Threading.Tasks;

namespace Zuravvski.Infrastructure.Cqrs.Abstractions.Queries
{
    public interface IQueryBus
    {
        public Task<TResult> Query<TResult>(IQuery<TResult> query);
    }
}
