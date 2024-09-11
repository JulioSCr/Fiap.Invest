using Fiap.Invest.Auth.Application.DTOs;
using Fiap.Invest.Auth.Application.InputModels;

namespace Fiap.Invest.Auth.Application.Services;
public interface IAuthService
{
    Task<TokenJwtDTO> RegistrarAsync(UsuarioInputModel usuario);
    Task<TokenJwtDTO> AutenticarAsync(AutenticacaoInputModel model);
    Task<RefreshTokenDTO?> ObterRedreshTokenAsync(Guid refreshToken);
    Task<UsuarioDTO> DecryptotokenAsync(Guid refreshToken);
}
