using Fiap.Invest.Portfolios.Application.Extensions.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Invest.Portfolios.Tests.Application.Extensions.Attributes;
public class NomePortfolioAttributeTestable : NomePortfolioAttribute
{
    public ValidationResult? TestIsValid(object? value, ValidationContext validationContext)
    {
        return IsValid(value, validationContext);
    }
}