using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Blazor.WebApp.DTOs.Auth;
[ExcludeFromCodeCoverage]
public record TokenJwtDTO
{
    public string AccessToken { get; set; }
    public Guid RefreshToken { get; set; }
    public double ExpiraEm { get; set; }
    public TokenDetalheUsuarioDto UsuarioToken { get; set; }
}