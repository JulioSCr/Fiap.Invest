using Fiap.Invest.Portfolios.Application.Services;
using Fiap.Invest.Portfolios.Domain.Interfaces.Repositories;
using Fiap.Invest.Portfolios.Infraestructure.Context;
using Fiap.Invest.Portfolios.Infraestructure.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Portfolios.Api.Configuration;
[ExcludeFromCodeCoverage]
public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services
            .AddScoped<IPortfolioService, PortfolioService>()
            .AddScoped<IPortfolioRepository, PortfolioRepository>()
            .AddScoped<PortfolioContext>();
    }
}