using Blazored.LocalStorage;
using Fiap.Invest.Blazor.WebApp.DTOs;
using Fiap.Invest.Blazor.WebApp.DTOs.Ativos;
using Fiap.Invest.Blazor.WebApp.Services.Interfaces;

namespace Fiap.Invest.Blazor.WebApp.Services;
public class AtivoService : Service, IAtivoService
{
    private HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;

    public AtivoService(HttpClient httpClient, ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
    }

    public async Task<PagedResultDTO<AtivoDTO>?> ListarAsync(int tamanhoPagina, int paginaAtual)
    {
        var response = await _httpClient.GetAsync($"/api/ativo?pageSize={tamanhoPagina}&pageIndex={paginaAtual}");

        if (!response.IsSuccessStatusCode) return null;

        if (response.StatusCode != System.Net.HttpStatusCode.OK) return null;

        return await DeserializarObjetoResponse<PagedResultDTO<AtivoDTO>>(response);
    }

    public async Task<AtivoDTO?> ObterAsync(Guid ativoId)
    {
        var autenticado = AddAuthorization(ref _httpClient, await ObterToken(_localStorageService));
        if (!autenticado) return null;

        var response = await _httpClient.GetAsync($"/api/ativo/{ativoId}");

        if (!response.IsSuccessStatusCode) return null;

        if (response.StatusCode != System.Net.HttpStatusCode.OK) return null;

        return await DeserializarObjetoResponse<AtivoDTO>(response);
    }

    ~AtivoService()
    {
        Dispose();
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}
