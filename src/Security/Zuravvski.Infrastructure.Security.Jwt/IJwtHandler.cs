using System.Collections.Generic;

namespace Zuravvski.Infrastructure.Security.Jwt
{
    public interface IJwtHandler
    {
        JsonWebToken Create(string userId, 
                            string? role = null, 
                            string? audience = null, 
                            IDictionary<string, IEnumerable<string>>? claims = null);

        JsonWebTokenPayload GetPayload(string accessToken);
    }
}
