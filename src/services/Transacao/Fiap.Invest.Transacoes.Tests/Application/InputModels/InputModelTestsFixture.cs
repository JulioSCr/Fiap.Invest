using Bogus;
using Fiap.Invest.Transacoes.Application.InputModels;
using Fiap.Invest.Transacoes.Domain.Entities;
using Fiap.Invest.Transacoes.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using DataAnnotationsValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Fiap.Invest.Transacoes.Tests.Application.InputModels;

[ExcludeFromCodeCoverage]
[CollectionDefinition(nameof(InputModelCollection))]
public class InputModelCollection
        : ICollectionFixture<InputModelTestsFixture>
{ }

[ExcludeFromCodeCoverage]
public class InputModelTestsFixture
{
    private readonly Faker _faker;

    public InputModelTestsFixture()
    {
        _faker = new Faker("pt_BR");
    }

    public IList<DataAnnotationsValidationResult> ValidateModel(object model)
    {
        var results = new List<DataAnnotationsValidationResult>();
        var context = new ValidationContext(model, serviceProvider: null, items: null);
        Validator.TryValidateObject(model, context, results, validateAllProperties: true);
        return results;
    }

    public static TransacaoInputModel ObterTransacaoInputModelValidoCompra()
    {
        return new TransacaoInputModel
        {
            PortfolioId = Guid.NewGuid(),
            AtivoId = Guid.NewGuid(),
            Tipo = ETipoTransacao.Compra,
            Quantidade = Transacao.QuantidadeMinima,
            Preco = Transacao.PrecoMinimo
        };
    }

    public static TransacaoInputModel ObterTransacaoInputModelValidoVenda()
    {
        return new TransacaoInputModel
        {
            PortfolioId = Guid.NewGuid(),
            AtivoId = Guid.NewGuid(),
            Tipo = ETipoTransacao.Compra,
            Quantidade = Transacao.QuantidadeMinima,
            Preco = Transacao.PrecoMinimo
        };
    }

    public static TransacaoInputModel ObterTransacaoInputModelQuantidadeInvalida()
    {
        return new TransacaoInputModel
        {
            PortfolioId = Guid.NewGuid(),
            AtivoId = Guid.NewGuid(),
            Tipo = ETipoTransacao.Compra,
            Quantidade = Transacao.QuantidadeMinima - 1,
            Preco = Transacao.PrecoMinimo
        };
    }

    public static TransacaoInputModel ObterTransacaoInputModelPrecoInvalido()
    {
        return new TransacaoInputModel
        {
            PortfolioId = Guid.NewGuid(),
            AtivoId = Guid.NewGuid(),
            Tipo = ETipoTransacao.Compra,
            Quantidade = Transacao.QuantidadeMinima,
            Preco = Transacao.PrecoMinimo - 0.1M
        };
    }

    public static IEnumerable<object[]> ObterTransacaoInputModelValidos()
    {
        yield return new object[] { new TransacaoInputModel {
            PortfolioId = Guid.NewGuid(),
            AtivoId = Guid.NewGuid(),
            Tipo = ETipoTransacao.Compra,
            Quantidade = Transacao.QuantidadeMinima,
            Preco = Transacao.PrecoMinimo } };
        yield return new object[] { new TransacaoInputModel {
            PortfolioId = Guid.NewGuid(),
            AtivoId = Guid.NewGuid(),
            Tipo = ETipoTransacao.Venda,
            Quantidade = Transacao.QuantidadeMinima,
            Preco = Transacao.PrecoMinimo } };
    }

    public static IEnumerable<object[]> ObterTransacaoInputModelQuantidadeMenorQueMinimo()
    {
        yield return new object[] { new TransacaoInputModel {
            PortfolioId = Guid.NewGuid(),
            AtivoId = Guid.NewGuid(),
            Tipo = ETipoTransacao.Compra,
            Quantidade = Transacao.QuantidadeMinima - 1,
            Preco = Transacao.PrecoMinimo } };
    }

    public static IEnumerable<object[]> ObterTransacaoInputModelPrecoMenorQueMinimo()
    {
        yield return new object[] { new TransacaoInputModel {
            PortfolioId = Guid.NewGuid(),
            AtivoId = Guid.NewGuid(),
            Tipo = ETipoTransacao.Compra,
            Quantidade = Transacao.QuantidadeMinima,
            Preco = Transacao.PrecoMinimo - 0.1M } };
    }
}