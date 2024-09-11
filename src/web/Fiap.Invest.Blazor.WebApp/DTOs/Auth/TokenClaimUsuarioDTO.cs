using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Blazor.WebApp.DTOs.Auth;
[ExcludeFromCodeCoverage]
public record struct TokenClaimUsuarioDto
{
    public string Valor { get; set; }
    public string Tipo { get; set; }
}
