using Delivery.Core.DomainObjects;
using Fiap.Invest.Ativos.Application.DTOs;
using Fiap.Invest.Ativos.Application.InputModels;
using Fiap.Invest.Ativos.Domain.Entities;

namespace Fiap.Invest.Ativos.Application.Services;
public interface IAtivoService
{
    Task<AtivoDTO?> ObterAtivoAsync(Guid ativoId);
    Task<PagedResult<AtivoDTO>> ListarAtivosAsync(int pageSize, int pageIndex);
    Task<Ativo> AdicionarAtivoAsync(AtivoInputModel model);
}
