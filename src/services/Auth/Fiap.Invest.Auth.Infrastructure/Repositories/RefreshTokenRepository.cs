using Delivery.Core.Data;
using Fiap.Invest.Auth.Domain.Entities;
using Fiap.Invest.Auth.Domain.Interfaces.Repositories;
using Fiap.Invest.Auth.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Auth.Infrastructure.Repositories;
[ExcludeFromCodeCoverage]
public sealed class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AuthContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public RefreshTokenRepository(AuthContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetRefreshTokenByTokenAsync(Guid token)
    {
        return await _context.RefreshTokens.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Token == token);
    }

    public async Task AddAsync(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
    }

    public async Task<List<RefreshToken>> ListarPorCpfAsync(string cpf)
    {
        var refreshTokens = await _context.RefreshTokens.Where(u => u.Cpf == cpf).ToListAsync();
        return refreshTokens ?? new List<RefreshToken>();
    }

    public void DeletarRange(IEnumerable<RefreshToken> refreshTokens)
    {
        _context.RefreshTokens.RemoveRange(refreshTokens);
    }

    public void Dispose()
    {
        _context?.Dispose();
    }

    public Task<RefreshToken> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task Add(RefreshToken Item)
    {
        throw new NotImplementedException();
    }

    public void Update(RefreshToken Item)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid Id)
    {
        throw new NotImplementedException();
    }
}
