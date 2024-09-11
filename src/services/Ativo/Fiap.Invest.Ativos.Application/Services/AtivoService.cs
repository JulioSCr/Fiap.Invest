using Delivery.Core.DomainObjects;
using Fiap.Invest.Ativos.Application.DTOs;
using Fiap.Invest.Ativos.Application.InputModels;
using Fiap.Invest.Ativos.Domain.Entities;
using Fiap.Invest.Ativos.Domain.Interfaces.Repositories;
using Fiap.Invest.Core.Exceptions;

namespace Fiap.Invest.Ativos.Application.Services;
public class AtivoService : IAtivoService
{
    private readonly IAtivoRepository _ativoRepository;

    public AtivoService(IAtivoRepository ativoRepository)
    {
        _ativoRepository = ativoRepository;
    }

    public async Task<AtivoDTO?> ObterAtivoAsync(Guid ativoId)
    {
        var ativo = await _ativoRepository.GetById(ativoId);
        return new AtivoDTO(ativo);
    }

    public async Task<PagedResult<AtivoDTO>> ListarAtivosAsync(int pageSize, int pageIndex)
    {
        var ativosPaginados = await _ativoRepository.ListarTodosAsync(pageSize, pageIndex);
        return new PagedResult<AtivoDTO>
        {
            List = ativosPaginados.List.Select(ativo => new AtivoDTO(ativo)),
            TotalResults = ativosPaginados.TotalResults,
            PageIndex = ativosPaginados.PageIndex,
            PageSize = ativosPaginados.PageSize,
            Query = ativosPaginados.Query
        };
    }

    public async Task<Ativo> AdicionarAtivoAsync(AtivoInputModel model)
    {
        var ativo = new Ativo(model.Tipo, model.Nome, model.Codigo);

        await _ativoRepository.Add(ativo);

        if (!await _ativoRepository.UnitOfWork.Commit())
            throw new FiapInvestApplicationException("Falha ao persistir transação.");

        return ativo;
    }
}
