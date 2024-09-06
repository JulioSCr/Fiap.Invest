using Delivery.Core.Data;
using Fiap.Invest.Portfolios.Domain.Entities;
using Fiap.Invest.Portfolios.Domain.Interfaces.Repositories;
using Fiap.Invest.Portfolios.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Invest.Portfolios.Infrastructure.Repositories;
public sealed class PortfolioRepository : IPortfolioRepository
{
    private readonly PortfolioContext _context;

    public PortfolioRepository(PortfolioContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<List<Portfolio>> GetByUsuarioAsync(Guid usuarioId)
    {
        return await _context.Portfolios.AsNoTracking()
            .Where(p => p.UsuarioId == usuarioId).ToListAsync();
    }

    public async Task<Portfolio> GetById(Guid id)
    {
        var portfolio = await _context.Portfolios.FirstOrDefaultAsync(x => x.Id == id);
        if (portfolio is null)
            throw new InvalidOperationException($"Nenhum portfólio encontrado com Id \"{id}\".");
        return portfolio;
    }

    public async Task Add(Portfolio portfolio)
    {
        await _context.Portfolios.AddAsync(portfolio);
    }

    public void Update(Portfolio portfolio)
    {
        _context.Portfolios.Update(portfolio);
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