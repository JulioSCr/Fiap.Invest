using Fiap.Invest.Core.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Core.Extensions.Attributes;
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class NomePortfolioAttribute : ValidationAttribute
{
    public const string MemberNameGenerico = "NomePortfolio";

    public NomePortfolioAttribute()
    {
        ErrorMessage = "O campo {0} é inválido para o tipo NomePortfolio.";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var mensagemErro = string.Format(
            ErrorMessageString,
            validationContext?.DisplayName ?? MemberNameGenerico);

        var nome = value?.ToString();

        var inconsistencias = NomePortfolio.ObterInconsistencias(nome);

        mensagemErro += $" Motivo: {inconsistencias}";

        return string.IsNullOrWhiteSpace(inconsistencias)
            ? ValidationResult.Success
            : new ValidationResult(
                mensagemErro,
                new[] {
                    validationContext?.MemberName ?? MemberNameGenerico
                });
    }

    public override string FormatErrorMessage(string nome)
    {
        return $"O campo \"{nome}\" é inválido.";
    }
}