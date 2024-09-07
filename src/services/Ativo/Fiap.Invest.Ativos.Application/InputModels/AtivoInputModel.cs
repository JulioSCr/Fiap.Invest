using Fiap.Invest.Ativos.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Ativos.Application.InputModels;
[ExcludeFromCodeCoverage]
public class AtivoInputModel
{
    [Required]
    public ETipoAtivo Tipo { get; set; }

    [Required]
    public string Nome { get; set; } = string.Empty;

    [Required]
    public string Codigo { get; set; } = string.Empty;
}
