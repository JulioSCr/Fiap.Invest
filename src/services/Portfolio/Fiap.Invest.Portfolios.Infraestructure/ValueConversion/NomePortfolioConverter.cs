using Fiap.Invest.Portfolios.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fiap.Invest.Portfolios.Infraestructure.ValueConversion;
public class NomePortfolioConverter : ValueConverter<NomePortfolio, string>
{
    public NomePortfolioConverter()
        : base(
            nome => nome.Valor,
            str => new NomePortfolio(str)
        ) 
    { }
}