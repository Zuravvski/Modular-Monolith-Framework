using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Zuravvski.Infrastructure.Security.Jwt
{
    internal sealed class JwtHandler : IJwtHandler
    {
        private static readonly IDictionary<string, IEnumerable<string>> EmptyClaims = 
            new Dictionary<string, IEnumerable<string>>();

        private static readonly ISet<string> DefaultClaims = new HashSet<string>
        {
            JwtRegisteredClaimNames.Sub,
            JwtRegisteredClaimNames.UniqueName,
            JwtRegisteredClaimNames.Jti,
            JwtRegisteredClaimNames.Iat,
            ClaimTypes.Role,
        };

        private readonly JwtOptions _jwtOptions;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();
        private readonly SigningCredentials _signingCredentials;

        public JwtHandler(JwtOptions jwtOptions, TokenValidationParameters tokenValidationParameters)
        {
            _jwtOptions = jwtOptions ?? throw new ArgumentNullException(nameof(jwtOptions));
            _tokenValidationParameters = tokenValidationParameters ?? throw new ArgumentNullException(nameof(tokenValidationParameters));
            _signingCredentials = new SigningCredentials(
                tokenValidationParameters.IssuerSigningKey, 
                SecurityAlgorithms.HmacSha256
            );

        }

        public JsonWebToken Create(string userId,
                                   string role = null,
                                   string audience = null,
                                   IDictionary<string, IEnumerable<string>> claims = null)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (_jwtOptions.ExpiresIn is null)
            {
                throw new ArgumentNullException(nameof(_jwtOptions.ExpiresIn));
            }

            var now = DateTime.UtcNow;

            var jwtClaims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Iss, _jwtOptions.Issuer),
                new(JwtRegisteredClaimNames.Sub, userId),
                new(JwtRegisteredClaimNames.UniqueName, userId),
                new(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (!string.IsNullOrWhiteSpace(role))
            {
                jwtClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            if (!string.IsNullOrWhiteSpace(audience))
            {
                jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Aud, audience));
            }

            if (claims?.Any() is true)
            {
                var customClaims = new List<Claim>();
                foreach (var (claim, values) in claims)
                {
                    customClaims.AddRange(values.Select(value => new Claim(claim, value)));
                }

                jwtClaims.AddRange(customClaims);
            }

            var expiresIn = now.Add(_jwtOptions.ExpiresIn.Value);

            var jwt = new JwtSecurityToken(
                _jwtOptions.Issuer,
                expires: expiresIn,
                notBefore: now,
                claims: jwtClaims,
                signingCredentials: _signingCredentials
            );
            var token = _jwtSecurityTokenHandler.WriteToken(jwt);

            return new JsonWebToken
            {
                AccessToken = token,
                RefreshToken = string.Empty,
                ExpirationTime = expiresIn.ToTimestamp(),
                Subject = userId,
                Role = role ?? string.Empty,
                Claims = claims ?? EmptyClaims
            };
        }

        public JsonWebTokenPayload GetPayload(string accessToken)
        {
            _jwtSecurityTokenHandler.ValidateToken(accessToken, _tokenValidationParameters, out var validatedToken);

            if (validatedToken is not JwtSecurityToken token)
            {
                return null;
            }

            return new JsonWebTokenPayload
            {
                Subject = token.Subject,
                Role = token.Claims.SingleOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value,
                ExpirationTime = token.ValidTo.ToTimestamp(),
                Claims = token.Claims.Where(claim => !DefaultClaims.Contains(claim.Type))
                    .GroupBy(claim => claim.Type)
                    .ToDictionary(k => k.Key, v => v.Select(claim => claim.Value))
            };
        }
    }
}
