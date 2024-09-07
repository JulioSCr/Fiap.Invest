using Delivery.Core.DomainObjects;
using Fiap.Invest.Ativos.Application.DTOs;
using Fiap.Invest.Ativos.Application.InputModels;

namespace Fiap.Invest.Ativos.Application.Services;
public interface IAtivoService
{
    Task<AtivoDTO?> ObterAtivoAsync(Guid ativoId);
    Task<PagedResult<AtivoDTO>> ListarAtivosAsync(int pageSize, int pageIndex);
    Task AdicionarAtivoAsync(AtivoInputModel model);
}
