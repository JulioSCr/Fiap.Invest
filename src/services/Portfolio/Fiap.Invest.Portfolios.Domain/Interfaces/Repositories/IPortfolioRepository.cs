using Delivery.Core.Data;
using Fiap.Invest.Portfolios.Domain.Entities;

namespace Fiap.Invest.Portfolios.Domain.Interfaces.Repositories;

public interface IPortfolioRepository : IRepository<Portfolio>
{
    IUnitOfWork UnitOfWork { get; }
    public Task<List<Portfolio>> ListarPorUsuarioAsync(Guid usuarioId);
}