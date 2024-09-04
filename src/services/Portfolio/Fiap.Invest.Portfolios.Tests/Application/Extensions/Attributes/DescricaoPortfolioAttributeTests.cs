using Bogus;
using Fiap.Invest.Portfolios.Domain.ValueObjects;
using Fiap.Invest.Portfolios.Application.Extensions.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Portfolios.Tests.Application.Extensions.Attributes;

[ExcludeFromCodeCoverage]
public class DescricaoPortfolioAttributeTests
{
    private readonly DescricaoPortfolioAttributeTestable _attribute;
    private readonly Faker _faker;

    public DescricaoPortfolioAttributeTests()
    {
        _attribute = new DescricaoPortfolioAttributeTestable();
        _faker = new Faker("pt_BR");
    }

    [Fact(DisplayName = "IsValid Quando Descrição Válida Deve Retornar Verdadeiro")]
    [Trait("Categoria", "DescricaoPortfolioAttribute")]
    public void IsValid_QuandoDescricaoValida_DeveRetornarVerdadeiro()
    {
        // Arrange
        var descricao = "Portfólio de Renda Variável";

        // Act
        var resultado = _attribute.IsValid(descricao);

        // Assert
        Assert.True(resultado);
    }

    [Fact(DisplayName = "IsValid Quando Descrição Em Branco Deve Retornar Verdadeiro")]
    [Trait("Categoria", "DescricaoPortfolioAttribute")]
    public void IsValid_QuandoDescricaoEmBranco_DeveRetornarVerdadeiro()
    {
        // Arrange
        var descricao = "                 ";

        // Act
        var resultado = _attribute.IsValid(descricao);

        // Assert
        Assert.True(resultado);
    }

    [Fact(DisplayName = "IsValid Quando Descrição Nula Deve Retornar Verdadeiro")]
    [Trait("Categoria", "DescricaoPortfolioAttribute")]
    public void IsValid_QuandoDescricaoNula_DeveRetornarVerdadeiro()
    {
        // Arrange
        string? descricao = null;

        // Act
        var resultado = _attribute.IsValid(descricao);

        // Assert
        Assert.True(resultado);
    }

    [Fact(DisplayName = "IsValid Quando Descrição Maior Que Máximo Deve Retornar Falso")]
    [Trait("Categoria", "DescricaoPortfolioAttribute")]
    public void IsValid_QuandoDescricaoMaiorQueMaximo_DeveRetornarFalso()
    {
        // Arrange
        var descricao = _faker.Random.AlphaNumeric(DescricaoPortfolio.TamanhoMaximo + 1);
        var nomeCampo = "Descricao";
        var mensagem = $"O campo {nomeCampo} é inválido para o tipo DescricaoPortfolio. Motivo: Descrição deve conter no máximo {DescricaoPortfolio.TamanhoMaximo} caracteres.";
        var context = new ValidationContext(new { }) { MemberName = nomeCampo };

        // Act
        var resultado = _attribute.TestIsValid(descricao, context);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(mensagem, resultado?.ErrorMessage);
    }
}