using Delivery.Core.DatabaseFlavor;
using Delivery.WebAPI.Core.Identity;
using Fiap.Invest.Ativos.Infrastructure.Context;
using System.Diagnostics.CodeAnalysis;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Fiap.Invest.Ativos.Api.Configuration;
[ExcludeFromCodeCoverage]
public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddJwtAsyncKeyConfiguration(configuration);

        services.ConfigureProviderForContext<AtivoContext>(ProviderConfiguration.DetectDatabase(configuration));

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

        app.UseAuthConfiguration();

        app.UseCors("Total");

        app.MapControllers();

        app.UseJwksDiscovery();

        app.MapHealthChecks("/healthz");
    }

    public static bool ValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
    {
        return true;
    }
}
