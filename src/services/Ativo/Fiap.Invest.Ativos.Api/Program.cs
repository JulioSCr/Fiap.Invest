using Delivery.WebAPI.Configuration;
using Fiap.Invest.Ativos.Api.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Net.Security;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore;

[ExcludeFromCodeCoverage]
public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.WebHost.ConfigureKestrel(options => { });

        builder.Services.AddApiConfiguration(builder.Configuration);
        builder.Services.AddSwaggerConfiguration(new(
                "Ativo API",
                "Esta API faz parte do projeto Fiap Invest, projeto em grupo de alunos da FIAP",
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        builder.Services.RegisterServices();

        var app = builder.Build();

        app.UseSwaggerConfiguration();

        app.UseApiConfiguration(app.Environment);

        app.Run();
    }
}
