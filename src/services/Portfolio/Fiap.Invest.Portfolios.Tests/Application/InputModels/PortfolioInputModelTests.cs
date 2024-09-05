using Bogus;
using Fiap.Invest.Portfolios.Application.InputModels;
using Fiap.Invest.Portfolios.Domain.ValueObjects;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Portfolios.Tests.Application.InputModels;

[ExcludeFromCodeCoverage]
[Collection(nameof(InputModelCollection))]
public class PortfolioInputModelTests
{
    private readonly InputModelTestsFixture _fixture;
    private readonly Faker _faker;

    public PortfolioInputModelTests(InputModelTestsFixture fixture)
    {
        _fixture = fixture;
        _faker = new Faker("pt_BR");
    }

    [Theory(DisplayName = "PortfolioInputModel Quando Válido Deve Passar Na Validação")]
    [MemberData(nameof(InputModelTestsFixture.ObterPortfolioInputModelValidos), MemberType = typeof(InputModelTestsFixture))]
    [Trait("Categoria", "PortfolioInputModel")]
    public void PortfolioInputModel_QuandoValido_DevePassarNaValidacao(PortfolioInputModel model)
    {
        // Arrange & Act
        var validationResults = _fixture.ValidateModel(model);

        // Assert
        Assert.Empty(validationResults);
    }

    [Theory(DisplayName = "PortfolioInputModel Quando Nome Nulo Ou Em Branco Deve Falhar Na Validação")]
    [MemberData(nameof(InputModelTestsFixture.ObterPortfolioInputModelNomeNuloOuEmBranco), MemberType = typeof(InputModelTestsFixture))]
    [Trait("Categoria", "PortfolioInputModel")]
    public void PortfolioInputModel_QuandoNomeNuloOuEmBranco_DeveFalharNaValidacao(PortfolioInputModel model)
    {
        // Arrange & Act
        var validationResults = _fixture.ValidateModel(model);

        // Assert
        Assert.Single(validationResults);
        Assert.Equal("Nome é obrigatório", validationResults[0].ErrorMessage);
    }

    [Fact(DisplayName = "PortfolioInputModel Quando Nome Maior Que Máximo Deve Falhar Na Validação")]
    [Trait("Categoria", "PortfolioInputModel")]
    public void PortfolioInputModel_QuandoNomeMaiorQueMaximo_DeveFalharNaValidacao()
    {
        // Arrange
        var mensagem = $"O campo {nameof(PortfolioInputModel.Nome)} é inválido para o tipo NomePortfolio. Motivo: Nome do portfólio deve conter no máximo {NomePortfolio.TamanhoMaximo} caracteres.";
        var model = new PortfolioInputModel
        {
            UsuarioId = Guid.NewGuid(),
            Nome = _faker.Random.AlphaNumeric(NomePortfolio.TamanhoMaximo + 1)
        };

        // Act
        var validationResults = _fixture.ValidateModel(model);

        // Assert
        Assert.Single(validationResults);
        Assert.Equal(mensagem, validationResults[0].ErrorMessage);
    }

    [Fact(DisplayName = "PortfolioInputModel Quando Nome Menor Que Mínimo Deve Falhar Na Validação")]
    [Trait("Categoria", "PortfolioInputModel")]
    public void PortfolioInputModel_QuandoNomeMenorQueMinimo_DeveFalharNaValidacao()
    {
        // Arrange
        var mensagem = $"O campo {nameof(PortfolioInputModel.Nome)} é inválido para o tipo NomePortfolio. Motivo: Nome do portfólio deve conter no mínimo {NomePortfolio.TamanhoMinimo} caracteres.";
        var model = new PortfolioInputModel
        {
            UsuarioId = Guid.NewGuid(),
            Nome = _faker.Random.AlphaNumeric(NomePortfolio.TamanhoMinimo - 1)
        };

        // Act
        var validationResults = _fixture.ValidateModel(model);

        // Assert
        Assert.Single(validationResults);
        Assert.Equal(mensagem, validationResults[0].ErrorMessage);
    }

    [Fact(DisplayName = "PortfolioInputModel Quando Descricao Maior Que Máximo Deve Falhar Na Validação")]
    [Trait("Categoria", "PortfolioInputModel")]
    public void PortfolioInputModel_QuandoDescricaoMaiorQueMaximo_DeveFalharNaValidacao()
    {
        // Arrange
        var mensagem = $"O campo {nameof(PortfolioInputModel.Descricao)} é inválido para o tipo DescricaoPortfolio. Motivo: Descrição deve conter no máximo {DescricaoPortfolio.TamanhoMaximo} caracteres.";
        var model = new PortfolioInputModel
        {
            UsuarioId = Guid.NewGuid(),
            Nome = "Renda variável",
            Descricao = _faker.Random.AlphaNumeric(DescricaoPortfolio.TamanhoMaximo + 1)
        };

        // Act
        var validationResults = _fixture.ValidateModel(model);

        // Assert
        Assert.Single(validationResults);
        Assert.Equal(mensagem, validationResults[0].ErrorMessage);
    }
}