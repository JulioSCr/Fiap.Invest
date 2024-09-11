global using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using Fiap.Invest.Blazor.WebApp;
using Fiap.Invest.Blazor.WebApp.Services;
using Fiap.Invest.Blazor.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddHttpClient<IAuthService, AuthService>("Auth Client", client =>
{
    client.BaseAddress = new Uri("https://localhost:8081/");
});
builder.Services.AddHttpClient<IPortfolioService, PortfolioService>("Portfolio Client", client =>
{
    client.BaseAddress = new Uri("http://localhost:5091/");
});
builder.Services.AddHttpClient<IAtivoService, AtivoService>("Ativo Client", client =>
{
    client.BaseAddress = new Uri("http://localhost:5090/");
});
builder.Services.AddHttpClient<ITransacaoService, TransacaoService>("Transacao Client", client =>
{
    client.BaseAddress = new Uri("http://localhost:5092/");
});


await builder.Build().RunAsync();
