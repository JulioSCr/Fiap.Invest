using Delivery.Core.Data;
using Fiap.Invest.Transacoes.Domain.Entities;

namespace Fiap.Invest.Transacoes.Domain.Interfaces.Repositories;
public interface ITransacaoRepository : IRepository<Transacao>
{
    IUnitOfWork UnitOfWork { get; }
    Task<List<Transacao>> ListarPorPortfolioAsync(Guid portfolioId);
}
