using Delivery.Core.DomainObjects;
using Delivery.WebAPI.Core.Controllers;
using Fiap.Invest.Core.Exceptions;
using Fiap.Invest.Portfolios.Application.DTOs;
using Fiap.Invest.Portfolios.Application.InputModels;
using Fiap.Invest.Portfolios.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Invest.Portfolios.Api.Controllers;
[Authorize]
[Route("api/[Controller]")]
public sealed class PortfolioController : MainController
{
    private readonly IPortfolioService _service;

    public PortfolioController(IPortfolioService service)
    {
        _service = service;
    }

    [HttpGet("Usuario")]
    [ProducesResponseType(typeof(List<PortfolioDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListarPortfolioPorUsuario()
    {
        try
        {
            return CustomResponse(await _service.ListarPorUsuarioAsync());
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

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CriarPortfolioAsync([FromBody] PortfolioInputModel model)
    {
        try
        {
            var portfolio = await _service.CriarPortfolioAsync(model);
            return Created(portfolio.Id.ToString(), portfolio.Id);
        }
        catch (Exception ex) when (ex is FiapInvestApplicationException || ex is DomainException || ex is DataNotFoundException)
        {
            AddErrorToStack($"{ex.GetType().Name}: {ex.Message}");
            return CustomResponse();
        }
    }
}