using Fiap.Invest.Portfolios.Domain.Entities;

namespace Fiap.Invest.Portfolios.Domain.Interfaces.Repositories;

public interface IPortfolioRepository
{
    public Task<List<Portfolio>> ObterPorUsuarioAsync(Guid UsuarioId);
    public bool Gravar(Portfolio portfolio);
}