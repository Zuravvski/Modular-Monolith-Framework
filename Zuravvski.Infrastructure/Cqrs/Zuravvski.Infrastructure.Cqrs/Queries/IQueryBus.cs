using System.Threading.Tasks;

namespace Zuravvski.Infrastructure.Cqrs.Queries
{
    public interface IQueryBus
    {
        public Task<TResult> Query<TResult>(IQuery<TResult> query);
    }
}
