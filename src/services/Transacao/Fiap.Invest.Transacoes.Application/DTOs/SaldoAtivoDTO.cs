using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Transacoes.Application.DTOs;
[ExcludeFromCodeCoverage]
public class SaldoAtivoDTO
{
    public Guid AtivoId { get; set; }
    public int Quantidade { get; set; }
}
