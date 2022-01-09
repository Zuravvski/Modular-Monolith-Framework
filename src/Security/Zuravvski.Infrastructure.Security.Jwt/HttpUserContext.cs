using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System;
using System.Linq;

namespace Zuravvski.Infrastructure.Security.Jwt
{
    internal sealed class HttpUserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtHandler _jwtHandler;

        public Guid UserId => GetUserId(_httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization]);

        public HttpUserContext(IHttpContextAccessor httpContextAccessor, IJwtHandler jwtHandler)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _jwtHandler = jwtHandler ?? throw new ArgumentNullException(nameof(jwtHandler));
        }

        private Guid GetUserId(StringValues authorizationHeader)
        {
            if (authorizationHeader == StringValues.Empty)
            {
                return Guid.Empty;
            }

            var accessToken = authorizationHeader.Single().Split(" ").Last();
            var payload = _jwtHandler.GetPayload(accessToken);
            return Guid.Parse(payload.Subject);
        }
    }
}
