using Delivery.Core.DomainObjects;
using Delivery.WebAPI.Core.Controllers;
using Fiap.Invest.Auth.Application.DTOs;
using Fiap.Invest.Auth.Application.InputModels;
using Fiap.Invest.Auth.Application.Services;
using Fiap.Invest.Core.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Invest.Auth.Api.Controllers;
[Route("api/[Controller]")]
public class AuthController : MainController
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [AllowAnonymous]
    [HttpPost("registrar")]
    [ProducesResponseType(typeof(TokenJwtDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegistrarUsuarioAsync(UsuarioInputModel model)
    {
        try
        {
            return CustomResponse(await _service.RegistrarAsync(model));
        }
        catch (Exception ex) when (ex is FiapInvestApplicationException || ex is DomainException || ex is DataNotFoundException)
        {
            AddErrorToStack($"{ex.GetType().Name}: {ex.Message}");
            return CustomResponse();
        }
    }

    [AllowAnonymous]
    [HttpPost("autenticar")]
    [ProducesResponseType(typeof(TokenJwtDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegistrarUsuarioAsync(AutenticacaoInputModel model)
    {
        try
        {
            return CustomResponse(await _service.AutenticarAsync(model));
        }
        catch (Exception ex) when (ex is FiapInvestApplicationException || ex is DomainException || ex is DataNotFoundException)
        {
            AddErrorToStack($"{ex.GetType().Name}: {ex.Message}");
            return CustomResponse();
        }
    }

    [AllowAnonymous]
    [HttpPut("refresh-token")]
    [ProducesResponseType(typeof(TokenJwtDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] Guid refreshToken)
    {
        try
        {
            return CustomResponse(await _service.ObterRedreshTokenAsync(refreshToken));
        }
        catch (Exception ex) when (ex is FiapInvestApplicationException || ex is DomainException || ex is DataNotFoundException)
        {
            AddErrorToStack($"{ex.GetType().Name}: {ex.Message}");
            return CustomResponse();
        }
    }
}
