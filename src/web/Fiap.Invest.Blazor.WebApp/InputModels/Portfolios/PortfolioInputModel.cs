using Fiap.Invest.Core.Extensions.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Invest.Blazor.WebApp.InputModels.Portfolios;
public class PortfolioInputModel
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [NomePortfolio()]
    public string? Nome { get; set; }

    [DescricaoPortfolio]
    public string? Descricao { get; set; }
}
