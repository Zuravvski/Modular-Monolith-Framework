using System;

namespace Zuravvski.Infrastructure.Security.Jwt
{
    public class JwtOptions
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string IssuerSigningKey { get; set; }
        public TimeSpan? ExpiresIn { get; set; }
        public bool ValidateIssuerSigningKey { get; set; } = false;
        public bool ValidateAudience { get; set; } = true;
        public bool ValidateIssuer { get; set; } = true;
        public bool ValidateLifetime { get; set; } = true;
        public bool IncludeErrorDetails { get; set; } = true;
    }
}
