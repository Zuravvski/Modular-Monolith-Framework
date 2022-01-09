using System.Collections.Generic;

namespace Zuravvski.Infrastructure.Security.Jwt
{
    public class JsonWebTokenPayload
    {
        public long ExpirationTime { get; init; }
        public string Subject { get; init; }
        public string Role { get; init; }
        public IDictionary<string, IEnumerable<string>> Claims { get; init; }
    }
}
