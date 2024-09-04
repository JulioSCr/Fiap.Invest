using Fiap.Invest.Portfolios.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fiap.Invest.Portfolios.Infraestructure.ValueConversion;
public class DescricaoPortfolioConverter : ValueConverter<DescricaoPortfolio, string?>
{
    public DescricaoPortfolioConverter()
        : base(
            descricao => descricao.Valor,
            str => new DescricaoPortfolio(str)
        ) 
    { }
}