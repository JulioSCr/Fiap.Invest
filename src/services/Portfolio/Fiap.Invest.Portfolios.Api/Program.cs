using Fiap.Invest.Portfolios.Api.Configuration;
using Delivery.WebAPI.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Fiap.Invest.Portfolios.Api;
public static class Program
{
    [ExcludeFromCodeCoverage]
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApiConfiguration(builder.Configuration);
        builder.Services.AddSwaggerConfiguration(new(
                "Portfólio API",
                "Esta API faz parte do projeto Fiap Invest, projeto em grupo de alunos da FIAP",
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        builder.Services.RegisterServices();

        builder.Services.RegisterServices();

        var app = builder.Build();

        app.UseSwaggerConfiguration();

        app.UseApiConfiguration(app.Environment);

        app.Run();
    }
}