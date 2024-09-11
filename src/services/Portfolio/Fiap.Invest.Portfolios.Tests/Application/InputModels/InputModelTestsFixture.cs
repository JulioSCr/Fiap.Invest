using Bogus;
using Fiap.Invest.Portfolios.Application.InputModels;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using DataAnnotationsValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Fiap.Invest.Portfolios.Tests.Application.InputModels;

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

    public static IEnumerable<object[]> ObterPortfolioInputModelValidos()
    {
        yield return new object[] { new PortfolioInputModel {
            Nome = "Renda variável" } };
        yield return new object[] { new PortfolioInputModel {
            Nome = "Renda variável",
            Descricao = "Portfólio de renda variável" } };
    }

    public static IEnumerable<object[]> ObterPortfolioInputModelNomeNuloOuEmBranco()
    {
        yield return new object[] { new PortfolioInputModel { } };
        yield return new object[] { new PortfolioInputModel { Nome = "                 " } };
    }
}