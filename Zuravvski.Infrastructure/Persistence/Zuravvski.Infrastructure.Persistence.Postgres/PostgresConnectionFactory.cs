using System.Data;
using Npgsql;

namespace Zuravvski.Infrastructure.Persistence.Postgres
{
    public class PostgresConnectionFactory : ISqlConnectionFactory
    {
        private readonly PostgresSettings _postgresSettings;

        public PostgresConnectionFactory(PostgresSettings postgresSettings)
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
