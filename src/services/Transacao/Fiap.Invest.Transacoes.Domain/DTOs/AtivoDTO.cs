using Fiap.Invest.Transacoes.Domain.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Transacoes.Domain.DTOs;
[ExcludeFromCodeCoverage]
public class AtivoDTO
{
    public Guid Id { get; set; }
    public ETipoAtivo Tipo { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;
}
