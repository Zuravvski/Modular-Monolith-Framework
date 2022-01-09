using System.Collections.Generic;

namespace Zuravvski.Infrastructure.Security.Jwt
{
    public class JsonWebToken
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long ExpirationTime { get; set; }
        public string Subject { get; set; }
        public string Role { get; set; }
        public IDictionary<string, IEnumerable<string>> Claims { get; set; }
    }
}
