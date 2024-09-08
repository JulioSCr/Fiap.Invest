using Delivery.Core.DatabaseFlavor;
using Delivery.WebAPI.Core.Identity;
using Fiap.Invest.Auth.Application.Extensions;
using Fiap.Invest.Auth.Infrastructure.Context;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Auth.Api.Configuration;
[ExcludeFromCodeCoverage]
public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureProviderForContext<AuthContext>(ProviderConfiguration.DetectDatabase(configuration));

        services.AddControllers();

        services.Configure<AppTokenSettings>(options => configuration.GetSection(nameof(AppTokenSettings)).Bind(options));
        services.AddSingleton<IAppTokenSettings>(sp => sp.GetService<IOptions<AppTokenSettings>>()?.Value ?? throw new ArgumentException($"{nameof(AppTokenSettings)} pendente configuração"));

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
}
