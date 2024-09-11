using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Transacoes.Domain.DTOs;
[ExcludeFromCodeCoverage]
public record PortfolioDTO
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
}
