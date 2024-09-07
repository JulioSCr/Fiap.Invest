using Fiap.Invest.Transacoes.Api.Clients;
using Fiap.Invest.Transacoes.Application.Services;
using Fiap.Invest.Transacoes.Domain.Interfaces.Clients;
using Fiap.Invest.Transacoes.Domain.Interfaces.Repositories;
using Fiap.Invest.Transacoes.Infrastructure.Context;
using Fiap.Invest.Transacoes.Infrastructure.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Transacoes.Api.Configuration;
[ExcludeFromCodeCoverage]
public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddHttpClient<IAtivoClient, AtivoClient>();
        services.AddHttpClient<IPortfolioClient, PortfolioClient>();

        services
            .AddScoped<ITransacaoService, TransacaoService>()
            .AddScoped<ITransacaoRepository, TransacaoRepository>()
            .AddScoped<TransacaoContext>();
    }
}
