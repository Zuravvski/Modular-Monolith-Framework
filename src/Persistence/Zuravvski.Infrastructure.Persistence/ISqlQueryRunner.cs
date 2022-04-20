using System;
using System.Data;
using System.Threading.Tasks;

namespace Zuravvski.Infrastructure.Persistence
{
    public interface ISqlQueryRunner
    {
        public void Run(Action<IDbConnection> action);
        public TResult Run<TResult>(Func<IDbConnection, TResult> action);
        public Task<TResult> RunAsync<TResult>(Func<IDbConnection, Task<TResult>> action);
        public Task RunInTransaction(Func<IDbConnection, Task> action);
        public Task<TResult> RunInTransaction<TResult>(Func<IDbConnection, Task<TResult>> action);
    }
}
