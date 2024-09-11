using Fiap.Invest.Transacoes.Domain.DTOs;

namespace Fiap.Invest.Transacoes.Domain.Interfaces.Clients;
public interface IAtivoClient
{
    Task<AtivoDTO?> ObterAtivoPorIdAsync(Guid ativoId);
}
