using Bogus;
using Fiap.Invest.Portfolios.Domain.ValueObjects;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Portfolios.Tests.Domain.ValueObjects
{
    [ExcludeFromCodeCoverage]
    public class NomePortfolioTests
    {
        private readonly Faker _facker;

        public NomePortfolioTests()
        {
            _facker = new Faker("pt_BR");
        }

        [Fact(DisplayName = "Construtor Quando Nome Nulo Deve Gerar Exceção")]
        [Trait("Categoria", "NomePortfolio")]
        public void Construtor_QuandoNomeNulo_DeveGerarExcecao()
        {
            // Arrange
            var mensagem = "Deve ser fornecido um nome para o portfólio.";

            // Act
            var erro = (() =>
            {
                var nome = new NomePortfolio(null);
            });

            // Assert
            var excecao = Assert.Throws<InvalidOperationException>(erro);
            Assert.Equal(mensagem, excecao.Message);
        }

        [Fact(DisplayName = "Construtor Quando Nome Maior Que Tamanho Máximo Deve Gerar Exceção")]
        [Trait("Categoria", "NomePortfolio")]
        public void Construtor_QuandoNomeMaiorQueTamanhoMaximo_DeveGerarExcecao()
        {
            // Arrange
            var nomeFalso = _facker.Random.AlphaNumeric(NomePortfolio.TamanhoMaximo + 1);
            var mensagem = $"Nome do portfólio deve conter no máximo {NomePortfolio.TamanhoMaximo} caracteres.";

            // Act
            var erro = (() =>
            {
                var nome = new NomePortfolio(nomeFalso);
            });

            // Assert
            var excecao = Assert.Throws<InvalidOperationException>(erro);
            Assert.Equal(mensagem, excecao.Message);
        }

        [Fact(DisplayName = "Construtor Quando Nome Menor Que Tamanho Mínimo Deve Gerar Exceção")]
        [Trait("Categoria", "NomePortfolio")]
        public void Construtor_QuandoNomeMenorQueTamanhoMinimo_DeveGerarExcecao()
        {
            // Arrange
            var nomeFalso = _facker.Random.AlphaNumeric(NomePortfolio.TamanhoMinimo - 1);
            var mensagem = $"Nome do portfólio deve conter no mínimo {NomePortfolio.TamanhoMinimo} caracteres.";

            // Act
            var erro = (() =>
            {
                var nome = new NomePortfolio(nomeFalso);
            });

            // Assert
            var excecao = Assert.Throws<InvalidOperationException>(erro);
            Assert.Equal(mensagem, excecao.Message);
        }

        [Fact(DisplayName = "Construtor Quando Nome Maior Igual Que Tamanho Mínimo E Menor Igual Ao Tamanho Máximo Deve Retornar Nulo")]
        [Trait("Categoria", "NomePortfolio")]
        public void Construtor_QuandoNomeMaiorIgualQueTamanhoMinimoEMenorIgualTamanhoMaximo_DeveRetornarNulo()
        {
            // Arrange
            var nomeFalso = _facker.Random.AlphaNumeric(_facker.Random.Int(min: NomePortfolio.TamanhoMinimo, max: NomePortfolio.TamanhoMaximo));

            // Act
            var nome = new NomePortfolio(nomeFalso);

            // Assert
            Assert.IsType<NomePortfolio>(nome);
            Assert.NotNull(nome.Valor);
        }

        [Fact(DisplayName = "ObterInconsistencias Quando Nome Nulo Deve Gerar Inconsistência")]
        [Trait("Categoria", "NomePortfolio")]
        public void ObterInconsistencias_QuandoNomeNulo_DeveGerarInconsitencia()
        {
            // Arrange
            var mensagem = "Deve ser fornecido um nome para o portfólio.";

            // Act
            var erro = NomePortfolio.ObterInconsistencias(null);

            // Assert
            Assert.Equal(mensagem, erro);
        }

        [Fact(DisplayName = "ObterInconsistencias Quando Nome Maior Que Tamanho Máximo Deve Gerar Inconsistência")]
        [Trait("Categoria", "NomePortfolio")]
        public void ObterInconsistencias_QuandoNomeMaiorQueTamanhoMaximo_DeveGerarInconsitencia()
        {
            // Arrange
            var nomeFalso = _facker.Random.AlphaNumeric(DescricaoPortfolio.TamanhoMaximo + 1);
            var mensagem = $"Nome do portfólio deve conter no máximo {NomePortfolio.TamanhoMaximo} caracteres.";

            // Act
            var erro = NomePortfolio.ObterInconsistencias(nomeFalso);

            // Assert
            Assert.Equal(mensagem, erro);
        }

        [Fact(DisplayName = "ObterInconsistencias Quando Nome Menor Que Tamanho Mínimo Deve Gerar Inconsistência")]
        [Trait("Categoria", "NomePortfolio")]
        public void ObterInconsistencias_QuandoNomeMenorQueTamanhoMinimo_DeveGerarInconsitencia()
        {
            // Arrange
            var nomeFalso = _facker.Random.AlphaNumeric(NomePortfolio.TamanhoMinimo - 1);
            var mensagem = $"Nome do portfólio deve conter no mínimo {NomePortfolio.TamanhoMinimo} caracteres.";

            // Act
            var erro = NomePortfolio.ObterInconsistencias(nomeFalso);

            // Assert
            Assert.Equal(mensagem, erro);
        }

        [Fact(DisplayName = "ObterInconsistencias Quando Nome Maior Igual Que Tamanho Mínimo E Menor Igual Ao Tamanho Máximo Deve Retornar Nulo")]
        [Trait("Categoria", "NomePortfolio")]
        public void ObterInconsistencias_QuandoNomeMaiorIgualQueTamanhoMinimoEMenorIgualTamanhoMaximo_DeveRetornarNulo()
        {
            // Arrange
            var nomeFalso = _facker.Random.AlphaNumeric(_facker.Random.Int(min: NomePortfolio.TamanhoMinimo, max: NomePortfolio.TamanhoMaximo));

            // Act
            var erro = NomePortfolio.ObterInconsistencias(nomeFalso);

            // Assert
            Assert.Null(erro);
        }

        [Theory(DisplayName = "Formatar Quando Nome Sem Formatação Deve Retornar Formatado")]
        [InlineData("portfólio renda variável", "Portfólio Renda Variável")]
        [InlineData("      portfólio     renda        variável     ", "Portfólio Renda Variável")]
        [Trait("Categoria", "NomePortfolio")]
        public void Formatar_QuandoNomeSemFormatacao_DeveRetornarFormatado(string nome, string nomeFormatadoEsperado)
        {
            // Arrange & Act
            var nomeFormatado = NomePortfolio.Formatar(nome);

            // Assert
            Assert.Equal(nomeFormatadoEsperado, nomeFormatado);
        }
    }
}
