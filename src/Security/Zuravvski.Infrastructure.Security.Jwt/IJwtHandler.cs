using System;

namespace Zuravvski.Infrastructure.Security.Jwt
{
    public interface IJwtHandler
    {
        JsonWebToken Create(string email, Guid id);
    }
}
