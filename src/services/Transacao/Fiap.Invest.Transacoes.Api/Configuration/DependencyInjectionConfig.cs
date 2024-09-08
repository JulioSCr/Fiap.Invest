using Delivery.WebAPI.Core.User;
using Fiap.Invest.Core.Extensions.Clients;
using Fiap.Invest.Transacoes.Application.Services;
using Fiap.Invest.Transacoes.Domain.Interfaces.Clients;
using Fiap.Invest.Transacoes.Domain.Interfaces.Repositories;
using Fiap.Invest.Transacoes.Infrastructure.Clients;
using Fiap.Invest.Transacoes.Infrastructure.Context;
using Fiap.Invest.Transacoes.Infrastructure.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Transacoes.Api.Configuration;
[ExcludeFromCodeCoverage]
public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

        services
            .AddHttpClient<IAtivoClient, AtivoClient>()
            .ConfigurePrimaryHttpMessageHandler(o => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            })
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
        services
            .AddHttpClient<IPortfolioClient, PortfolioClient>()
            .ConfigurePrimaryHttpMessageHandler(o => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            })
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        services
            .AddScoped<ITransacaoService, TransacaoService>()
            .AddScoped<ITransacaoRepository, TransacaoRepository>()
            .AddScoped<IAspNetUser, AspNetUser>()
            .AddScoped<TransacaoContext>();
    }
}
