using Fiap.Invest.Transacoes.Application.DTOs;
using Fiap.Invest.Transacoes.Application.InputModels;
using Fiap.Invest.Transacoes.Domain.Entities;

namespace Fiap.Invest.Transacoes.Application.Services;
public interface ITransacaoService
{
    Task<Transacao> FazerTransacaoAsync(TransacaoInputModel model);
    Task<List<SaldoAtivoDTO>> ObterSaldoAtivoPorPortfolioAsync(Guid portfolioId);
}
