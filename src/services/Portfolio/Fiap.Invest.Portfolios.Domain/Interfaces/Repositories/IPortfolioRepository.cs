using Delivery.Core.Data;
using Fiap.Invest.Portfolios.Domain.Entities;

namespace Fiap.Invest.Portfolios.Domain.Interfaces.Repositories;

public interface IPortfolioRepository : IRepository<Portfolio>
{
    public Task<List<Portfolio>> ObterPorUsuarioAsync(Guid UsuarioId);
}