using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Runtime.CompilerServices;
using System.Text;
using Zuravvski.Infrastructure.Options;

[assembly: InternalsVisibleTo("Zuravvski.Infrastructure.Security.Jwt.Autofac")]
namespace Zuravvski.Infrastructure.Security.Jwt
{
    public static class Extensions
    {
        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetOptions<JwtOptions>();
            var tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
                ValidateAudience = options.ValidateAudience,
                ValidateIssuer = options.ValidateIssuer,
                ValidateLifetime = options.ValidateLifetime,
                ValidateIssuerSigningKey = options.ValidateIssuerSigningKey,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
                        ValidIssuer = options.Issuer,
                        ValidateIssuer = options.ValidateIssuer,
                        ValidateAudience = options.ValidateAudience,
                        ValidateLifetime = options.ValidateLifetime
                    };
                });

            services.AddSingleton(options);
            services.AddSingleton<IJwtHandler, JwtHandler>();
            services.AddSingleton(tokenValidationParameters);
            services.AddScoped<IUserContext, HttpUserContext>();
            return services;
        }

        internal static long ToTimestamp(this DateTime date)
            => new DateTimeOffset(date).ToUnixTimeMilliseconds();
    }
}
