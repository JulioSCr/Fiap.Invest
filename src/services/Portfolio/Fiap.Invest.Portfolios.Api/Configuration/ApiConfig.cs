using Delivery.Core.DatabaseFlavor;
using Fiap.Invest.Portfolios.Infrastructure.Context;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Portfolios.Api.Configuration;
[ExcludeFromCodeCoverage]
public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureProviderForContext<PortfolioContext>(ProviderConfiguration.DetectDatabase(configuration));

        services.AddControllers();

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