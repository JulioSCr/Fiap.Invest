using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace Fiap.Invest.Auth.Application.DTOs;
[ExcludeFromCodeCoverage]
public record struct TokenClaimUsuarioDto
{
    public string Valor { get; set; }
    public string Tipo { get; set; }

    public TokenClaimUsuarioDto(Claim claim)
    {
        Valor = claim.Value;
        Tipo = claim.Type;
    }
}
