namespace Zuravvski.Infrastructure.Security.Jwt
{
    public class RefreshToken
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public bool Revoked { get; set; }
    }
}
