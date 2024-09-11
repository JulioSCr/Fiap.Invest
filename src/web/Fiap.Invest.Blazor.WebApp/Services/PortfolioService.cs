using Blazored.LocalStorage;
using Fiap.Invest.Blazor.WebApp.DTOs.Portfolios;
using Fiap.Invest.Blazor.WebApp.InputModels.Portfolios;
using Fiap.Invest.Blazor.WebApp.Services.Interfaces;

namespace Fiap.Invest.Blazor.WebApp.Services;
public class PortfolioService : Service, IPortfolioService
{
    private HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;

    public PortfolioService(HttpClient httpClient,
        ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
    }
    public async Task<List<PortfolioDTO>?> ListarAsync()
    {
        var autenticado = AddAuthorization(ref _httpClient, await ObterToken(_localStorageService));
        if (!autenticado) return null;

        var response = await _httpClient.GetAsync("/api/portfolio/usuario");

        if (!response.IsSuccessStatusCode) return null;

        if (response.StatusCode != System.Net.HttpStatusCode.OK) return null;

        return await DeserializarObjetoResponse<List<PortfolioDTO>>(response);
    }

    public async Task<bool> CadastrarAsync(PortfolioInputModel request)
    {
        var autenticado = AddAuthorization(ref _httpClient, await ObterToken(_localStorageService));
        if (!autenticado) return false;

        var response = await _httpClient.PostAsync("/api/portfolio", ObterConteudo(request));

        return response.IsSuccessStatusCode;
    }

    ~PortfolioService()
    {
        Dispose();
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}