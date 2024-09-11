using System.Diagnostics.CodeAnalysis;
using Fiap.Invest.Portfolios.Domain.Entities;
using Fiap.Invest.Portfolios.Domain.ValueObjects;

namespace Fiap.Invest.Portfolios.Tests.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class PortfolioTests
    {
        [Fact(DisplayName = "Construtor Quando Fornecido Deve Gerar Instância")]
        [Trait("Categoria", "Portfolio")]
        public void Construtor_QuandoFornecido_DeveGerarInstancia()
        {
            // Arrange
            var nome = new NomePortfolio("Teste");
            var descricao = new DescricaoPortfolio();

            // Act
            var portfolio = new Portfolio(Guid.NewGuid(), nome, descricao);

            // Assert
            Assert.IsType<Portfolio>(portfolio);
            Assert.NotNull(portfolio);
        }
    }
}
