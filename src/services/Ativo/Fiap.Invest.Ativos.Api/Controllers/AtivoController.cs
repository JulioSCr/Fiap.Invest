using Delivery.Core.DomainObjects;
using Delivery.WebAPI.Core.Controllers;
using Delivery.WebAPI.Core.Identity;
using Fiap.Invest.Ativos.Application.DTOs;
using Fiap.Invest.Ativos.Application.InputModels;
using Fiap.Invest.Ativos.Application.Services;
using Fiap.Invest.Core.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Invest.Ativos.Api.Controllers;
[Authorize]
[Route("api/[Controller]")]
public class AtivoController : MainController
{
    private readonly IAtivoService _service;

    public AtivoController(IAtivoService service)
    {
        _service = service;
    }

    [AllowAnonymous]
    [HttpGet("{ativoId}")]
    [ProducesResponseType(typeof(AtivoDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterAtivoAsync(Guid ativoId)
    {
        try
        {
            var ativo = await _service.ObterAtivoAsync(ativoId);
            if (ativo == null) return CustomResponse();
            return CustomResponse(ativo);
        }
        catch (Exception ex) when (ex is FiapInvestApplicationException || ex is DomainException)
        {
            AddErrorToStack($"{ex.GetType().Name}: {ex.Message}");
            return CustomResponse();
        }
        catch (DataNotFoundException)
        {
            return CustomResponse();
        }
    }

    [AllowAnonymous]
    [HttpGet()]
    [ProducesResponseType(typeof(PagedResult<AtivoDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
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
            AddErrorToStack($"{ex.GetType().Name}: {ex.Message}");
            return CustomResponse();
        }
    }

    [ClaimsAuthorize("Usuario", "Admin")]
    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CriarAtivoAsync([FromBody] AtivoInputModel model)
    {
        try
        {
            var ativo = await _service.AdicionarAtivoAsync(model);
            return Created(ativo.Id.ToString(), ativo.Id);
        }
        catch (Exception ex) when (ex is FiapInvestApplicationException || ex is DomainException || ex is DataNotFoundException)
        {
            AddErrorToStack($"{ex.GetType().Name}: {ex.Message}");
            return CustomResponse();
        }
    }
}
