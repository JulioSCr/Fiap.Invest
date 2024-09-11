using Fiap.Invest.Portfolios.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fiap.Invest.Portfolios.Infrastructure.ValueConversion;
public class DescricaoPortfolioConverter : ValueConverter<DescricaoPortfolio, string?>
{
    public DescricaoPortfolioConverter()
        : base(
            descricao => descricao.Valor ?? string.Empty,
            str => new DescricaoPortfolio(str)
        ) 
    { }
}