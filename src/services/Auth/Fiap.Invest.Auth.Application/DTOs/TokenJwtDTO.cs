using Fiap.Invest.Auth.Domain.Entities;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace Fiap.Invest.Auth.Application.DTOs;
[ExcludeFromCodeCoverage]
public record struct TokenJwtDto
{
    public string AccessToken { get; set; }
    public Guid RefreshToken { get; set; }
    public double ExpiraEm { get; set; }
    public TokenDetalheUsuarioDto UsuarioToken { get; set; }

    public TokenJwtDto(string accessToken, FiapInvestIdentityUser usuario, IEnumerable<Claim> claims, RefreshToken refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken.Token;
        ExpiraEm = TimeSpan.FromHours(1).TotalSeconds;
        UsuarioToken = new TokenDetalheUsuarioDto(usuario, claims);
    }
}