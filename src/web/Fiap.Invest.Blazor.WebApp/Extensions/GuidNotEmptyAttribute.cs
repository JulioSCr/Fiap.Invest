using Fiap.Invest.Core.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Invest.Blazor.WebApp.Extensions;
public class GuidNotEmptyAttribute : ValidationAttribute
{
    public const string MemberNameGenerico = "Campo";

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var mensagemErro = string.Format(
            ErrorMessageString,
            validationContext?.DisplayName ?? MemberNameGenerico);

        var invalido = (value == null || (Guid)value == Guid.Empty);


        return !invalido
            ? ValidationResult.Success
            : new ValidationResult(
                mensagemErro,
                new[] {
                    validationContext?.MemberName ?? MemberNameGenerico
                });
    }

    public override string FormatErrorMessage(string nome)
    {
        return $"O campo \"{nome}\" deve ser informado.";
    }
}