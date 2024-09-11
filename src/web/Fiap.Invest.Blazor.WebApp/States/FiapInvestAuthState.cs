
using Blazored.LocalStorage;
using Fiap.Invest.Blazor.WebApp.DTOs.Auth;
using Fiap.Invest.Blazor.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace Fiap.Invest.Blazor.WebApp.States;
public class FiapInvestAuthState : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    private readonly IAuthService _authService;
    private readonly NavigationManager _navigationManager;
    private ClaimsPrincipal _claims = new ClaimsPrincipal(new ClaimsIdentity());
    private bool _login = false;

    public FiapInvestAuthState(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorageService.GetItemAsStringAsync("__token");

        UsuarioDTO? usuario = null;
        try { usuario = new UsuarioDTO(token); } catch { }
        if (usuario is null)
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        _claims = usuario.ObterClaims();
        return new AuthenticationState(_claims);
    }

    public async Task SetUser(UsuarioDTO usuario)
    {
        _claims = usuario.ObterClaims();
        NotifyAuthenticationStateChanged(await Task.FromResult(GetAuthenticationStateAsync()));
    }

    public async Task ClearUser()
    {
        _claims = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(await Task.FromResult(GetAuthenticationStateAsync()));
    }
}
