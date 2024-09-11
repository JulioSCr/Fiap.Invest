using Delivery.Core.Data;
using Fiap.Invest.Auth.Domain.Entities;

namespace Fiap.Invest.Auth.Domain.Interfaces.Repositories;
public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    IUnitOfWork UnitOfWork { get; }
    Task<RefreshToken?> GetRefreshTokenByTokenAsync(Guid token);
    Task AddAsync(RefreshToken refreshToken);
    Task<List<RefreshToken>> ListarPorCpfAsync(string cpf);
    void DeletarRange(IEnumerable<RefreshToken> refreshTokens);
}
