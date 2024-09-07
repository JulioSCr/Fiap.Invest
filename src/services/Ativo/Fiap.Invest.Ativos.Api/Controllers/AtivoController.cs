using Delivery.Core.DomainObjects;
using Delivery.WebAPI.Core.Controllers;
using Fiap.Invest.Ativos.Application.DTOs;
using Fiap.Invest.Ativos.Application.InputModels;
using Fiap.Invest.Ativos.Application.Services;
using Fiap.Invest.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Invest.Ativos.Api.Controllers;
[Route("api/[Controller]")]
public class AtivoController : MainController
{
    private readonly IAtivoService _service;

    public AtivoController(IAtivoService service)
    {
        _service = service;
    }

    [HttpGet("{ativoId}")]
    [ProducesResponseType(typeof(AtivoDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterAtivoAsync(Guid ativoId)
    {
        try
        {
            var ativo = await _service.ObterAtivoAsync(ativoId);
            if (ativo == null) return CustomResponse();
            return CustomResponse(ativo);
        }
        catch (Exception ex) when (ex is FiapInvestApplicationException || ex is DomainException || ex is DataNotFoundException)
        {
            AddErrorToStack(ex.ToString());
            return CustomResponse();
        }
    }

    [HttpGet()]
    [ProducesResponseType(typeof(PagedResult<AtivoDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListarAtivosPaginadosAsync([FromQuery] int pageSize, [FromQuery] int pageIndex)
    {
        try
        {
            var ativosPaginados = await _service.ListarAtivosAsync(pageSize, pageIndex);
            if (ativosPaginados.TotalResults == 0) return CustomResponse();
            return CustomResponse(ativosPaginados);
        }
        catch (Exception ex) when (ex is FiapInvestApplicationException || ex is DomainException || ex is DataNotFoundException)
        {
            AddErrorToStack(ex.ToString());
            return CustomResponse();
        }
    }

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CriarAtivoAsync([FromBody] AtivoInputModel model)
    {
        try
        {
            await _service.AdicionarAtivoAsync(model);
            return Created();
        }
        catch (Exception ex) when (ex is FiapInvestApplicationException || ex is DomainException || ex is DataNotFoundException)
        {
            AddErrorToStack(ex.ToString());
            return CustomResponse();
        }
    }
}
