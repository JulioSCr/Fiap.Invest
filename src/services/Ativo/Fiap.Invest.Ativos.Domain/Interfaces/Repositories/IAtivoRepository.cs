using Delivery.Core.Data;
using Delivery.Core.DomainObjects;
using Fiap.Invest.Ativos.Domain.Entities;

namespace Fiap.Invest.Ativos.Domain.Interfaces.Repositories;
public interface IAtivoRepository : IRepository<Ativo>
{
    IUnitOfWork UnitOfWork { get; }
    Task<PagedResult<Ativo>> ListarTodosAsync(int pageSize, int pageIndex);
}
