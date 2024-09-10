using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Blazor.WebApp.DTOs.Auth;
[ExcludeFromCodeCoverage]
public record struct TokenDetalheUsuarioDto
{
    public string Cpf { get; set; }
    public string Nome { get; set; }
    public IEnumerable<TokenClaimUsuarioDto> Claims { get; set; }
};
