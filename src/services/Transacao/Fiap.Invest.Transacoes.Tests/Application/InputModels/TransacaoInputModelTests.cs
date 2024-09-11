using Fiap.Invest.Transacoes.Application.InputModels;
using Fiap.Invest.Transacoes.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Transacoes.Tests.Application.InputModels;
[ExcludeFromCodeCoverage]
[Collection(nameof(InputModelCollection))]
public class TransacaoInputModelTests
{
    private readonly InputModelTestsFixture _fixture;

    public TransacaoInputModelTests(InputModelTestsFixture fixture)
    {
        _fixture = fixture;
    }

    [Theory(DisplayName = "TransacaoInputModel Quando Válido Deve Passar Na Validação")]
    [MemberData(nameof(InputModelTestsFixture.ObterTransacaoInputModelValidos), MemberType = typeof(InputModelTestsFixture))]
    [Trait("Categoria", "TransacaoInputModel")]
    public void TransacaoInputModel_QuandoValido_DevePassarNaValidacao(TransacaoInputModel model)
    {
        // Arrange & Act
        var validationResults = _fixture.ValidateModel(model);

        // Assert
        Assert.Empty(validationResults);
    }

    [Theory(DisplayName = "TransacaoInputModel Quando Quantidade Menor Que Mínimo Deve Falhar Na Validação")]
    [MemberData(nameof(InputModelTestsFixture.ObterTransacaoInputModelQuantidadeMenorQueMinimo), MemberType = typeof(InputModelTestsFixture))]
    [Trait("Categoria", "TransacaoInputModel")]
    public void PortfolioInputModel_QuandoQuantidadeMenorQueMinimo_DeveFalharNaValidacao(TransacaoInputModel model)
    {
        // Arrange & Act
        var validationResults = _fixture.ValidateModel(model);

        // Assert
        Assert.Single(validationResults);
        Assert.Equal($"A quantidade deve ser maior que {Transacao.QuantidadeMinima}", validationResults[0].ErrorMessage);
    }

    [Theory(DisplayName = "TransacaoInputModel Quando Preço Menor Que Mínimo Deve Falhar Na Validação")]
    [MemberData(nameof(InputModelTestsFixture.ObterTransacaoInputModelPrecoMenorQueMinimo), MemberType = typeof(InputModelTestsFixture))]
    [Trait("Categoria", "TransacaoInputModel")]
    public void PortfolioInputModel_QuandoPrecoMenorQueMinimo_DeveFalharNaValidacao(TransacaoInputModel model)
    {
        // Arrange & Act
        var validationResults = _fixture.ValidateModel(model);

        // Assert
        Assert.Single(validationResults);
        Assert.Equal($"O preço deve ser maior que {Transacao.PrecoMinimo}", validationResults[0].ErrorMessage);
    }
}
