using Fiap.Invest.Core.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Invest.Core.Extensions.Attributes;
public class NomeValidatorAttribute : ValidationAttribute
{
    public const string MemberNameGenerico = "Nome";

    public NomeValidatorAttribute()
    {
        ErrorMessage = "O campo {0} é inválido para o tipo Nome.";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var mensagemErro = string.Format(
            ErrorMessageString,
            validationContext?.DisplayName ?? MemberNameGenerico);

        var nome = value?.ToString();

        var inconsistencia = Nome.ObterInconsistencias(nome);

        mensagemErro += $" Motivo: {inconsistencia}";

        return string.IsNullOrWhiteSpace(inconsistencia)
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