using System.Data;
using Npgsql;

namespace Zuravvski.Infrastructure.Persistence.Postgres
{
    public class PostgresConnectionFactory : ISqlConnectionFactory
    {
        private readonly PostgresOptions _postgresSettings;

        public PostgresConnectionFactory(PostgresOptions postgresSettings)
        {
            _postgresSettings = postgresSettings;
        }

        public IDbConnection CreateNewOpenedConnection()
        {
            var connectionString = $"Host={_postgresSettings.Host};" +
                $"Username={_postgresSettings.Username};" +
                $"Password={_postgresSettings.Password};" +
                $"Database={_postgresSettings.DatabaseName}";
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
