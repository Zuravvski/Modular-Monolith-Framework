using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Zuravvski.Infrastructure.Security.Jwt
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtOptions _jwtSettings;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly SecurityKey _securityKey;
        private readonly SigningCredentials _signingCredentials;
        private readonly JwtHeader _jwtHeader;

        public JwtHandler(JwtOptions jwtSettings)
        {
            _jwtSettings = jwtSettings;
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            _signingCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
            _jwtHeader = new JwtHeader(_signingCredentials);

        }
        public JsonWebToken Create(string email, Guid userId)
        {
            var nowUtc = DateTime.UtcNow;
            var expires = nowUtc.AddMilliseconds(_jwtSettings.ExpirationTimeInMs);
            var centuryBegin = new DateTime(1970, 1, 1).ToUniversalTime();
            var exp = (long)(new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds);
            var iat = (long)(new TimeSpan(nowUtc.Ticks - centuryBegin.Ticks).TotalSeconds);
            var payload = new JwtPayload
            {
                {"iss", _jwtSettings.Issuer},
                {"iat", iat},
                {"exp", exp},
                {"unique_name", email},
                {"id", userId}
            };
            var jwt = new JwtSecurityToken(_jwtHeader, payload);
            var token = _jwtSecurityTokenHandler.WriteToken(jwt);

            return new JsonWebToken
            {
                AccessToken = token,
                ExpirationTime = exp
            };
        }
    }
}
