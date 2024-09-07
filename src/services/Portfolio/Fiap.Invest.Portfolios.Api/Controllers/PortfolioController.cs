using Delivery.Core.DomainObjects;
using Delivery.WebAPI.Core.Controllers;
using Fiap.Invest.Core.Exceptions;
using Fiap.Invest.Portfolios.Application.DTOs;
using Fiap.Invest.Portfolios.Application.InputModels;
using Fiap.Invest.Portfolios.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Invest.Portfolios.Api.Controllers;
[Route("api/[Controller]")]
public sealed class PortfolioController : MainController
{
    private readonly IPortfolioService _service;

    public PortfolioController(IPortfolioService service)
    {
        _service = service;
    }

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CriarPortfolioAsync([FromBody] PortfolioInputModel model)
    {
        try
        {
            await _service.CriarPortfolioAsync(model);
            return Created();
        }
        catch (Exception ex) when (ex is FiapInvestApplicationException || ex is DomainException || ex is DataNotFoundException)
        {
            AddErrorToStack(ex.ToString());
            return CustomResponse();
        }
    }

    [HttpGet("{usuarioId}")]
    [ProducesResponseType(typeof(List<PortfolioDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListarPortfolioPorUsuario(Guid usuarioId)
    {
        try
        {
            return CustomResponse(await _service.ListarPorUsuarioAsync(usuarioId));
        }
        catch (Exception ex) when (ex is FiapInvestApplicationException || ex is DomainException || ex is DataNotFoundException)
        {
            AddErrorToStack(ex.ToString());
            return CustomResponse();
        }
    }
}