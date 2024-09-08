using Fiap.Invest.Auth.Application.DTOs;
using Fiap.Invest.Auth.Application.InputModels;

namespace Fiap.Invest.Auth.Application.Services;
public interface IAuthService
{
    Task<TokenJwtDto> RegistrarAsync(UsuarioInputModel usuario);
    Task<TokenJwtDto> AutenticarAsync(AutenticacaoInputModel model);
    Task<RefreshTokenDTO?> ObterRedreshTokenAsync(Guid refreshToken);
}
