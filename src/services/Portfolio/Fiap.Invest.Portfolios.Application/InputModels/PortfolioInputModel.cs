using Fiap.Invest.Portfolios.Application.Extensions.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Invest.Portfolios.Application.InputModels;

public class PortfolioInputModel
{
    [Required(ErrorMessage = "UsuarioId é obrigatório")]
    public Guid UsuarioId { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório")]
    [NomePortfolio()]
    public string? Nome { get; set; }

    [DescricaoPortfolio]
    public string? Descricao { get; set; }
}