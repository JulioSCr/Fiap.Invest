using Blazored.LocalStorage;
using Fiap.Invest.Blazor.WebApp.Services.Interfaces;
using Fiap.Invest.Blazor.WebApp.DTOs;
using Fiap.Invest.Blazor.WebApp.DTOs.Auth;
using System.Net;
using Fiap.Invest.Blazor.WebApp.InputModels;

namespace Fiap.Invest.Blazor.WebApp.Services;
public class AuthService : Service, IAuthService
{
    private HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;

    public AuthService(HttpClient httpClient,
        ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
    }

    public async Task LogoutAsync()
    {
        await _localStorageService.ClearAsync();
    }

    public async Task<UsuarioDTO?> ObterUsuarioToken(string token)
    {
        var response = await _httpClient.PostAsync("/api/auth/decryptotoken", ObterConteudo(token));

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return new UsuarioDTO(response);
        }

        if (!TratarErrosResponse(response))
        {
            return new UsuarioDTO(await DeserializarObjetoResponse<ResponseResult>(response));
        }

        return await DeserializarObjetoResponse<UsuarioDTO>(response);
    }

    public async Task<TokenJwtDTO?> LoginAsync(AutenticacaoInputModel request)
    {
        var response = await _httpClient.PostAsync("/api/Auth/autenticar", ObterConteudo(request));
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var res = await DeserializarObjetoResponse<TokenJwtDTO>(response);
            if (res != null)
                return res;
        }

        return null;
    }

    public async Task<TokenJwtDTO?> CadastrarAsync(UsuarioInputModel request)
    {
        var response = await _httpClient.PostAsync("/api/Auth/registrar", ObterConteudo(request));
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var res = await DeserializarObjetoResponse<TokenJwtDTO>(response);
            if (res != null)
                return res;
        }

        return null;
    }

    ~AuthService()
    {
        Dispose();
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}