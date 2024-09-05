using Fiap.Invest.Portfolios.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Portfolios.Application.DTOs;
[ExcludeFromCodeCoverage]
public record PortfolioDTO
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public string Nome { get; set; }
    public string? Descricao { get; set; }

    public PortfolioDTO(Portfolio portfolio)
    {
        Id = portfolio.Id;
        UsuarioId = portfolio.UsuarioId;
        Nome = portfolio.Nome.Valor;
        Descricao = portfolio.Descricao.Valor;
    }
}