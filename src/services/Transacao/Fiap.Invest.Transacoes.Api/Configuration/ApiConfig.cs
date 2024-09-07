using Delivery.Core.DatabaseFlavor;
using Fiap.Invest.Transacoes.Api.Extensions;
using Fiap.Invest.Transacoes.Infrastructure.Context;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Transacoes.Api.Configuration;
[ExcludeFromCodeCoverage]
public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureProviderForContext<TransacaoContext>(ProviderConfiguration.DetectDatabase(configuration));

        services.AddControllers();

        services.Configure<AppClientsSettings>(configuration);
        services.AddSingleton<IAppClientsSettings>(sp => sp.GetService<IOptions<AppClientsSettings>>()?.Value ?? throw new ArgumentException($"{nameof(AppClientsSettings)} pendente configuração"));

        services.AddCors(options =>
        {
            options.AddPolicy("Total",
                builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
        });

        services.AddHealthChecks();

        services.AddEndpointsApiExplorer();
    }

    public static void UseApiConfiguration(this WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHttpsRedirection();
        }

        app.UseRouting();

        app.UseCors("Total");

        app.MapControllers();

        app.MapHealthChecks("/healthz");
    }
}
