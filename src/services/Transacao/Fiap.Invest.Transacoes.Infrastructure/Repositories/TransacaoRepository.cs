using Delivery.Core.Data;
using Delivery.Core.Exceptions;
using Fiap.Invest.Transacoes.Domain.Entities;
using Fiap.Invest.Transacoes.Domain.Interfaces.Repositories;
using Fiap.Invest.Transacoes.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Invest.Transacoes.Infrastructure.Repositories;
public class TransacaoRepository : ITransacaoRepository
{
    private readonly TransacaoContext _context;

    public TransacaoRepository(TransacaoContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Transacao> GetById(Guid id)
    {
        var transacao = await _context.Transacoes.FirstOrDefaultAsync(x => x.Id == id);
        if (transacao is null)
            throw new DatabaseNotFoundException($"Nenhuma transação encontrada com Id \"{id}\".");
        return transacao;
    }

    public async Task<List<Transacao>> ListarPorPortfolioAsync(Guid portfolioId)
    {
        return await _context.Transacoes.AsNoTracking()
            .Where(p => p.PortfolioId == portfolioId).ToListAsync();
    }

    public async Task Add(Transacao transacao)
    {
        await _context.Transacoes.AddAsync(transacao);
    }

    public void Update(Transacao transacao)
    {
        _context.Transacoes.Update(transacao);
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
