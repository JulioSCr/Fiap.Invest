using Bogus;
using Fiap.Invest.Portfolios.Domain.ValueObjects;
using Fiap.Invest.Portfolios.Application.Extensions.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Portfolios.Tests.Application.Extensions.Attributes;

[ExcludeFromCodeCoverage]
public class NomePortfolioAttributeTests
{
    private readonly NomePortfolioAttributeTestable _attribute;
    private readonly Faker _faker;

    public NomePortfolioAttributeTests()
    {
        _attribute = new NomePortfolioAttributeTestable();
        _faker = new Faker("pt_BR");
    }

    [Fact(DisplayName = "IsValid Quando Nome Válido Deve Retornar Verdadeiro")]
    [Trait("Categoria", "NomePortfolioAttribute")]
    public void IsValid_QuandoNomeValido_DeveRetornarVerdadeiro()
    {
        // Arrange
        var nome = "Renda Variável";

        // Act
        var resultado = _attribute.IsValid(nome);

        // Assert
        Assert.True(resultado);
    }

    [Fact(DisplayName = "IsValid Quando Nome Em Branco Deve Retornar Falso")]
    [Trait("Categoria", "NomePortfolioAttribute")]
    public void IsValid_QuandoNomeEmBranco_DeveRetornarFalso()
    {
        // Arrange
        var nome = "                 ";
        var nomeCampo = "Nome";
        var mensagem = $"O campo {nomeCampo} é inválido para o tipo NomePortfolio. Motivo: Deve ser fornecido um nome para o portfólio.";
        var context = new ValidationContext(new { }) { MemberName = nomeCampo };

        // Act
        var resultado = _attribute.TestIsValid(nome, context);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(mensagem, resultado?.ErrorMessage);
    }

    [Fact(DisplayName = "IsValid Quando Nome Nulo Deve Retornar Falso")]
    [Trait("Categoria", "NomePortfolioAttribute")]
    public void IsValid_QuandoNomeNulo_DeveRetornarFalso()
    {
        // Arrange
        string? nome = null;
        var nomeCampo = "Nome";
        var mensagem = $"O campo {nomeCampo} é inválido para o tipo NomePortfolio. Motivo: Deve ser fornecido um nome para o portfólio.";
        var context = new ValidationContext(new { }) { MemberName = nomeCampo };

        // Act
        var resultado = _attribute.TestIsValid(nome, context);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(mensagem, resultado?.ErrorMessage);
    }

    [Fact(DisplayName = "IsValid Quando Nome Maior Que Máximo Deve Retornar Falso")]
    [Trait("Categoria", "NomePortfolioAttribute")]
    public void IsValid_QuandoNomeMaiorQueMaximo_DeveRetornarFalso()
    {
        // Arrange
        var nome = _faker.Random.AlphaNumeric(NomePortfolio.TamanhoMaximo + 1);
        var nomeCampo = "Nome";
        var mensagem = $"O campo {nomeCampo} é inválido para o tipo NomePortfolio. Motivo: Nome do portfólio deve conter no máximo {NomePortfolio.TamanhoMaximo} caracteres.";
        var context = new ValidationContext(new { }) { MemberName = nomeCampo };

        // Act
        var resultado = _attribute.TestIsValid(nome, context);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(mensagem, resultado?.ErrorMessage);
    }

    [Fact(DisplayName = "IsValid Quando Nome Menor Que Mínimo Deve Retornar Falso")]
    [Trait("Categoria", "NomePortfolioAttribute")]
    public void IsValid_QuandoNomeMenorQueMinimo_DeveRetornarFalso()
    {
        // Arrange
        var nome = _faker.Random.AlphaNumeric(NomePortfolio.TamanhoMinimo - 1);
        var nomeCampo = "Nome";
        var mensagem = $"O campo {nomeCampo} é inválido para o tipo NomePortfolio. Motivo: Nome do portfólio deve conter no mínimo {NomePortfolio.TamanhoMinimo} caracteres.";
        var context = new ValidationContext(new { }) { MemberName = nomeCampo };

        // Act
        var resultado = _attribute.TestIsValid(nome, context);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(mensagem, resultado?.ErrorMessage);
    }
}