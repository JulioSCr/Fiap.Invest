﻿using Delivery.Core.DomainObjects;
using Delivery.WebAPI.Core.Controllers;
using Fiap.Invest.Core.Exceptions;
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

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> FazerTransacaoAsync([FromBody] TransacaoInputModel model)
    {
        try
        {
            await _service.FazerTransacaoAsync(model);
            return CustomResponse();
        }
        catch (Exception ex) when (ex is FiapInvestApplicationException || ex is DomainException)
        {
            AddErrorToStack(ex.ToString());
            return CustomResponse();
        }
    }
}