using Delivery.WebAPI.Core.Controllers;
using Fiap.Invest.Portfolios.Application.DTOs;
using Fiap.Invest.Portfolios.Application.InputModels;
using Fiap.Invest.Portfolios.Application.Services;
using Fiap.Invest.Portfolios.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CriarPortfolioAsync([FromBody] PortfolioInputModel model)
    {
        await _service.CriarPortfolioAsync(model);
        return CustomResponse();
    }

    [HttpGet("{usuarioId}")]
    [ProducesResponseType(typeof(List<PortfolioDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListarPortfolioPorUsuario(Guid usuarioId)
        => CustomResponse(await _service.ListarPorUsuarioAsync(usuarioId));
}