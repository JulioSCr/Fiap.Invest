using Fiap.Invest.Blazor.WebApp.DTOs.Transacoes;
using Fiap.Invest.Blazor.WebApp.InputModels.Transacoes;

namespace Fiap.Invest.Blazor.WebApp.Services.Interfaces;
public interface ITransacaoService : IDisposable
{
    Task<KeyValuePair<string?, List<SaldoAtivoDTO>?>> ObterSaldoAsync(Guid portfolioId);
    Task<string?> FazerTransacaoAsync(TransacaoInputModel request);
}
