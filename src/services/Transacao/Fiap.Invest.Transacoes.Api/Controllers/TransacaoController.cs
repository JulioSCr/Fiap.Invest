using Delivery.Core.DomainObjects;
using Delivery.WebAPI.Core.Controllers;
using Fiap.Invest.Core.Exceptions;
using Fiap.Invest.Transacoes.Application.DTOs;
using Fiap.Invest.Transacoes.Application.InputModels;
using Fiap.Invest.Transacoes.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Invest.Transacoes.Api.Controllers;
[Route("api/[Controller]")]
public class TransacaoController : MainController
{
    private readonly ITransacaoService _service;

    public TransacaoController(ITransacaoService service)
    {
        _service = service;
    }

    [HttpGet("saldo/{portfolioId}")]
    [ProducesResponseType(typeof(SaldoAtivoDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterSaldoAsync(Guid portfolioId)
    {
        try
        {
            var saldos = await _service.ObterSaldoAtivoPorPortfolioAsync(portfolioId);
            if (saldos.Count == 0) return CustomResponse();
            return CustomResponse(saldos);
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
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> FazerTransacaoAsync([FromBody] TransacaoInputModel model)
    {
        try
        {
            await _service.FazerTransacaoAsync(model);
            return Created();
        }
        catch (Exception ex) when (ex is FiapInvestApplicationException || ex is DomainException || ex is DataNotFoundException)
        {
            AddErrorToStack(ex.ToString());
            return CustomResponse();
        }
    }
}
