using Delivery.WebAPI.Configuration;
using Fiap.Invest.Auth.Api.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[ExcludeFromCodeCoverage]
public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddIdentityConfiguration(builder.Configuration);
        builder.Services.AddApiConfiguration(builder.Configuration);
        builder.Services.AddSwaggerConfiguration(new(
                "Auth API",
                "Esta API faz parte do projeto Fiap Invest, projeto em grupo de alunos da FIAP",
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        builder.Services.RegisterServices();

        var app = builder.Build();

        app.UseSwaggerConfiguration();

        app.UseApiConfiguration(app.Environment);

        app.Run();
    }
}