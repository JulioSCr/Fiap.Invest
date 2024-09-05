using Bogus;
using Fiap.Invest.Portfolios.Domain.ValueObjects;

namespace Fiap.Invest.Portfolios.Tests.Domain.ValueObjects
{
    public class DescricaoPortfolioTests
    {
        private readonly Faker _faker;

        public DescricaoPortfolioTests()
        {
            _faker = new Faker("pt_BR");
        }

        [Fact(DisplayName = "Construtor Quando Descrição Maior Que Tamanho Máximo Deve Gerar Exceção")]
        [Trait("Categoria", "DescricaoPortfolio")]
        public void Construtor_QuandoDescricaoMaiorQueTamanhoMaximo_DeveGerarExcecao()
        {
            // Arrange
            var nome = _faker.Random.AlphaNumeric(DescricaoPortfolio.TamanhoMaximo + 1);
            var mensagem = $"Descrição deve conter no máximo {DescricaoPortfolio.TamanhoMaximo} caracteres.";

            // Act
            var erro = new Action(() =>
            {
                var descricao = new DescricaoPortfolio(nome);
            });

            // Assert
            var excecao = Assert.Throws<InvalidOperationException>(erro);
            Assert.Equal(mensagem, excecao.Message);
        }

        [Fact(DisplayName = "Construtor Quando Descrição Nula Deve Gerar Com Valor Nulo")]
        [Trait("Categoria", "DescricaoPortfolio")]
        public void Construtor_QuandoDescricaoNula_DeveGerarComValorNulo()
        {
            // Arrange
            string? nome = null;

            // Act
            var descricao = new DescricaoPortfolio(nome);

            // Assert
            Assert.IsType<DescricaoPortfolio>(descricao);
            Assert.Null(descricao.Valor);
        }

        [Fact(DisplayName = "Construtor Quando Descrição Menor Igual Ao Valor Máximo Deve Gerar Objeto")]
        [Trait("Categoria", "DescricaoPortfolio")]
        public void Construtor_QuandoDescricaoMenorIgualAoValorMaximo_DeveGerarObjeto()
        {
            // Arrange
            var nome = _faker.Random.AlphaNumeric(_faker.Random.Int(min: 1, max: DescricaoPortfolio.TamanhoMaximo));

            // Act
            var descricao = new DescricaoPortfolio(nome);

            // Assert
            Assert.IsType<DescricaoPortfolio>(descricao);
            Assert.NotNull(descricao.Valor);
        }

        [Fact(DisplayName = "ObterInconsistencias Quando Descrição Maior Que Tamanho Máximo Deve Gerar Inconsisistência")]
        [Trait("Categoria", "DescricaoPortfolio")]
        public void ObterInconsistencias_QuandoDescricaoMaiorQueTamanhoMaximo_DeveGerarInconsisistencia()
        {
            // Arrange
            var nome = _faker.Random.AlphaNumeric(DescricaoPortfolio.TamanhoMaximo + 1);
            var mensagem = $"Descrição deve conter no máximo {DescricaoPortfolio.TamanhoMaximo} caracteres.";

            // Act
            var erro = DescricaoPortfolio.ObterInconsistencias(nome);

            // Assert
            Assert.Equal(mensagem, erro);
        }

        [Fact(DisplayName = "ObterInconsistencias Quando Descrição Menor Igual Ao Valor Máximo Deve Retornar Nulo")]
        [Trait("Categoria", "DescricaoPortfolio")]
        public void ObterInconsistencias_QuandoDescricaoMenorIgualAoValorMaximo_DeveRetornarNulo()
        {
            // Arrange
            var nome = _faker.Random.AlphaNumeric(_faker.Random.Int(min: 1, max: DescricaoPortfolio.TamanhoMaximo));

            // Act
            var erro = DescricaoPortfolio.ObterInconsistencias(nome);

            // Assert
            Assert.Null(erro);
        }
    }
}
