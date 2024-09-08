using Fiap.Invest.Core.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Invest.Core.Extensions.Attributes;
public class CpfValidatorAttribute : ValidationAttribute
{
    public const string MemberNameGenerico = "Cpf";

    public CpfValidatorAttribute()
    {
        ErrorMessage = "O campo {0} é inválido para o tipo Cpf.";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var mensagemErro = string.Format(
            ErrorMessageString,
            validationContext?.DisplayName ?? MemberNameGenerico);

        var cpf = value?.ToString();

        mensagemErro += $" Motivo: Cpf \"{cpf}\" inválido";

        return Cpf.Validar(cpf)
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
