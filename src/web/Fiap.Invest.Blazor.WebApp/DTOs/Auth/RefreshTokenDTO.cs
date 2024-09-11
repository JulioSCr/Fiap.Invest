using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Blazor.WebApp.DTOs.Auth;
[ExcludeFromCodeCoverage]
public record struct RefreshTokenDTO
{
    public Guid Token { get; set; }
    public string? Cpf { get; set; }
    public DateTime DataExpiracao { get; set; }
}
