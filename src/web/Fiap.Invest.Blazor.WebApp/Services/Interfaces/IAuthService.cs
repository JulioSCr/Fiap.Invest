using Fiap.Invest.Blazor.WebApp.DTOs.Auth;
using Fiap.Invest.Blazor.WebApp.InputModels;

namespace Fiap.Invest.Blazor.WebApp.Services.Interfaces;
public interface IAuthService : IDisposable
{
    Task LogoutAsync();
    Task<TokenJwtDTO?> LoginAsync(AutenticacaoInputModel request);
    Task<UsuarioDTO?> ObterUsuarioToken(string token);
    Task<TokenJwtDTO?> CadastrarAsync(UsuarioInputModel request);
}
