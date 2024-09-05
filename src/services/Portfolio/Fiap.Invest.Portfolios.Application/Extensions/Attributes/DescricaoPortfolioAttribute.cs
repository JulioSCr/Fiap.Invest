using Fiap.Invest.Portfolios.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Portfolios.Application.Extensions.Attributes;
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class DescricaoPortfolioAttribute : ValidationAttribute
{
    public const string MemberNameGenerico = "DescricaoPortfolio";
    public DescricaoPortfolioAttribute()
    {
        ErrorMessage = "O campo {0} é inválido para o tipo DescricaoPortfolio.";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var mensagemErro = string.Format(
            ErrorMessageString,
            validationContext?.DisplayName ?? MemberNameGenerico);

        var descricao = value?.ToString();

        var inconsistencias = DescricaoPortfolio.ObterInconsistencias(descricao);

        mensagemErro += $" Motivo: {inconsistencias}";

        return string.IsNullOrWhiteSpace(inconsistencias)
            ? ValidationResult.Success
            : new ValidationResult(
                mensagemErro,
                new[] {
                    validationContext?.MemberName ?? MemberNameGenerico
                });
    }
}