namespace Zuravvski.Infrastructure.Security.Jwt
{
    public class JwtSettings
    {
        public string SecretKey { get; init; }
        public long ExpirationTimeInMs { get; init; }
        public string Issuer { get; init; }
    }
}
