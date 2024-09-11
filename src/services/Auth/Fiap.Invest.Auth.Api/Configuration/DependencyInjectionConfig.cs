using Delivery.WebAPI.Core.User;
using Fiap.Invest.Auth.Application.Services;
using Fiap.Invest.Auth.Domain.Interfaces.Repositories;
using Fiap.Invest.Auth.Infrastructure.Context;
using Fiap.Invest.Auth.Infrastructure.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Auth.Api.Configuration;
[ExcludeFromCodeCoverage]
public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        
        services
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>()
            .AddScoped<IAspNetUser, AspNetUser>()
            .AddScoped<AuthContext>();
    }
}