using System;
using System.Data;
using System.Threading.Tasks;

namespace Zuravvski.Infrastructure.Persistence
{
    public class DefaultSqlQueryRunner : ISqlQueryRunner
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public DefaultSqlQueryRunner(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void Run(Action<IDbConnection> action)
        {
            using var connection = _connectionFactory.CreateNewOpenedConnection();

            try
            {
                action(connection);
            }
            finally
            {
                connection.Close();
            }
        }

        public TResult Run<TResult>(Func<IDbConnection, TResult> action)
        {
            using var connection = _connectionFactory.CreateNewOpenedConnection();

            try
            {
                return action(connection);
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<TResult> RunAsync<TResult>(Func<IDbConnection, Task<TResult>> action)
        {
            using var connection = _connectionFactory.CreateNewOpenedConnection();

            try
            {
                return await action(connection);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
