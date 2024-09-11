using Fiap.Invest.Core.Extensions.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Invest.Blazor.WebApp.InputModels;
public class UsuarioInputModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [CpfValidator(ErrorMessage = "O campo {0} está em formato inválido")]
    public string Cpf { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [NomeValidator()]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 8)]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string Senha { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Display(Name = "Confirmar Senha")]
    [Compare("Senha", ErrorMessage = "As senhas não conferem")]
    public string? SenhaConfirmacao { get; set; } = string.Empty;
}
