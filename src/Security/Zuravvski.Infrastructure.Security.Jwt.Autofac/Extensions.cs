using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using Zuravvski.Infrastructure.Options;

namespace Zuravvski.Infrastructure.Security.Jwt.Autofac
{
    public static class Extensions
    {
        public static ContainerBuilder AddJwt(this ContainerBuilder builder,
                                              IConfiguration configuration,
                                              IHttpContextAccessor httpContextAccessor = null)
        {
            var options = configuration.GetOptions<JwtOptions>();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = options.ValidateAudience,
                ValidateIssuer = options.ValidateIssuer,
                ValidateLifetime = options.ValidateLifetime,
                ValidateIssuerSigningKey = options.ValidateIssuerSigningKey,
                ClockSkew = TimeSpan.Zero
            };

            builder.RegisterInstance(options)
                .SingleInstance()
                .PreserveExistingDefaults();

            builder.RegisterType<JwtHandler>()
                .As<IJwtHandler>()
                .SingleInstance()
                .PreserveExistingDefaults();

            builder.RegisterInstance(tokenValidationParameters).SingleInstance();

            if (httpContextAccessor is not null)
            {
                builder.Register(ctx =>
                {
                    var jwtHandler = ctx.Resolve<IJwtHandler>();
                    return new HttpUserContext(httpContextAccessor, jwtHandler);
                }).As<IUserContext>().InstancePerLifetimeScope().PreserveExistingDefaults();
            }
            return builder;
        }
    }
}
