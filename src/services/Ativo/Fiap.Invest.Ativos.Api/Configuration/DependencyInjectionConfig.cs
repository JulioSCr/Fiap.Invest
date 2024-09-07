using Fiap.Invest.Ativos.Application.Services;
using Fiap.Invest.Ativos.Domain.Interfaces.Repositories;
using Fiap.Invest.Ativos.Infrastructure.Context;
using Fiap.Invest.Ativos.Infrastructure.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Ativos.Api.Configuration;
[ExcludeFromCodeCoverage]
public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services
            .AddScoped<IAtivoService, AtivoService>()
            .AddScoped<IAtivoRepository, AtivoRepository>()
            .AddScoped<AtivoContext>();
    }
}
