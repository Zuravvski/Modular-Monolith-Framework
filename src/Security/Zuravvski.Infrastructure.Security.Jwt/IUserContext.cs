using System;

namespace Zuravvski.Infrastructure.Security.Jwt
{
    public interface IUserContext
    {
        public Guid UserId { get; }
    }
}
