using Delivery.WebAPI.Core.Identity;
using Fiap.Invest.Auth.Domain.Entities;
using Fiap.Invest.Auth.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using NetDevPack.Security.Jwt.Core.Jwa;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Auth.Api.Configuration;
[ExcludeFromCodeCoverage]
public static class IdentityConfig
{
    public static void AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddJwksManager(jwtOptions =>
            {
                jwtOptions.Jws = Algorithm.Create(AlgorithmType.ECDsa, JwtType.Jws);
            })
            .PersistKeysToDatabaseStore<AuthContext>();

        services.AddDefaultIdentity<FiapInvestIdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AuthContext>()
            .AddDefaultTokenProviders();

        services.AddJwtAsyncKeyConfiguration(configuration);
    }
}