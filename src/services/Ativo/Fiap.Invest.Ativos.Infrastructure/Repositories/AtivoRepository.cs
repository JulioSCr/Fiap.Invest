using Delivery.Core.Data;
using Delivery.Core.DomainObjects;
using Fiap.Invest.Ativos.Domain.Entities;
using Fiap.Invest.Ativos.Domain.Interfaces.Repositories;
using Fiap.Invest.Ativos.Infrastructure.Context;
using Fiap.Invest.Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Invest.Ativos.Infrastructure.Repositories;
public class AtivoRepository : IAtivoRepository
{
    private readonly AtivoContext _context;

    public AtivoRepository(AtivoContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Ativo> GetById(Guid id)
    {
        var ativo = await _context.Ativos.FirstOrDefaultAsync(x => x.Id == id);
        if (ativo is null)
            throw new DataNotFoundException($"Nenhum ativo encontrado com Id \"{id}\".");
        return ativo;
    }

    public async Task<PagedResult<Ativo>> ListarTodosAsync(int pageSize, int pageIndex)
    {
        var ativoQuery = _context.Ativos.AsQueryable();

        var catalog = await ativoQuery.AsNoTrackingWithIdentityResolution()
                                        .OrderBy(x => x.Codigo)
                                        .Skip(pageSize * (pageIndex - 1))
                                        .Take(pageSize).ToListAsync();

        var total = await ativoQuery.AsNoTrackingWithIdentityResolution()
                                      .CountAsync();


        return new PagedResult<Ativo>()
        {
            List = catalog,
            TotalResults = total,
            PageIndex = pageIndex,
            PageSize = pageSize,
            Query = null
        };
    }

    public async Task Add(Ativo ativo)
    {
        await _context.Ativos.AddAsync(ativo);
    }

    public void Update(Ativo ativo)
    {
        _context.Ativos.Update(ativo);
    }

    public Task Delete(Guid Id)
    {
        throw new NotImplementedException("Pendente implementação");
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}
