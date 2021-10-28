namespace Zuravvski.Infrastructure.Security.Jwt
{
    public class JsonWebToken
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long ExpirationTime { get; set; }
    }
}
