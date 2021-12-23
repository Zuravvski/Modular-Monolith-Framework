using System.Data;

namespace Zuravvski.Infrastructure.Persistence
{
    public interface ISqlConnectionFactory
    {
        public IDbConnection CreateNewOpenedConnection();
    }
}
