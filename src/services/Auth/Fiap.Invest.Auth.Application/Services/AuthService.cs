using Fiap.Invest.Auth.Application.DTOs;
using Fiap.Invest.Auth.Application.Extensions;
using Fiap.Invest.Auth.Application.InputModels;
using Fiap.Invest.Auth.Domain.Entities;
using Fiap.Invest.Auth.Domain.Interfaces.Repositories;
using Fiap.Invest.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NetDevPack.Security.Jwt.Core.Interfaces;
using System.Security.Claims;

namespace Fiap.Invest.Auth.Application.Services;
public class AuthService : IAuthService
{
    private readonly SignInManager<FiapInvestIdentityUser> _signInManager;
    private readonly UserManager<FiapInvestIdentityUser> _userManager;
    private readonly IJwtService _jwksService;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IAppTokenSettings _appTokenSettings;

    public AuthService(
        SignInManager<FiapInvestIdentityUser> signInManager,
        UserManager<FiapInvestIdentityUser> userManager,
        IJwtService jwksService,
        IHttpContextAccessor contextAccessor,
        IRefreshTokenRepository refreshTokenRepository,
        IAppTokenSettings appTokenSettings)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwksService = jwksService;
        _contextAccessor = contextAccessor;
        _refreshTokenRepository = refreshTokenRepository;
        _appTokenSettings = appTokenSettings;
    }

    public async Task<TokenJwtDto> RegistrarAsync(UsuarioInputModel usuario)
    {
        var usuarioBusca = await _userManager.FindByNameAsync(usuario.Cpf);
        if (usuarioBusca != null)
            throw new FiapInvestApplicationException("Usuário já cadastrado");

        var usuarioIdentity = new FiapInvestIdentityUser(usuario.Nome, usuario.Cpf);

        var resultado = await _userManager.CreateAsync(usuarioIdentity, usuario.Senha);

        var claims = new List<Claim>
        {
            new Claim("Usuario", "Comum")
        };
        var claimsResult = await _userManager.AddClaimsAsync(usuarioIdentity, claims);
        if (!claimsResult.Succeeded)
            throw new FiapInvestApplicationException("Falha ao adicionar permissões ao usuário");

        return await CriarTokenJwtAsync(usuario.Cpf);
    }

    public async Task<TokenJwtDto> CriarTokenJwtAsync(string cpf)
    {
        var usuario = await _userManager.FindByNameAsync(cpf);
        if (usuario == null)
            throw new FiapInvestApplicationException("Usuário não cadastrado");

        var claims = await _userManager.GetClaimsAsync(usuario);

        var identityClaims = AuthExtensions.ObterClaims(
            usuario.Id,
            claims,
            await _userManager.GetRolesAsync(usuario));

        var accessToken = identityClaims.GerarToken(await _jwksService.GetCurrentSigningCredentials(), _contextAccessor);

        var refreshToken = new RefreshToken
        {
            Cpf = cpf,
            DataExpiracao = DateTime.UtcNow.AddHours(_appTokenSettings.HorasExpiracaoRefreshToken)
        };

        var refreshTokens =  await _refreshTokenRepository.ListarPorCpfAsync(cpf);
        if (refreshTokens.Any())
            _refreshTokenRepository.DeletarRange(refreshTokens);
        await _refreshTokenRepository.AddAsync(refreshToken);

        await _refreshTokenRepository.UnitOfWork.Commit();

        return new TokenJwtDto(accessToken, usuario, claims, refreshToken);
    }

    public async Task<RefreshTokenDTO?> ObterRedreshTokenAsync(Guid refreshToken)
    {
        var token = await _refreshTokenRepository.GetRefreshTokenByTokenAsync(refreshToken);

        if (token == null)
            throw new FiapInvestApplicationException($"Código 01 - Token inválido.");

        if (token.DataExpiracao.ToLocalTime() < DateTime.Now)
            throw new FiapInvestApplicationException($"Código 02 - Token inválido");

        return new RefreshTokenDTO(token);
    }

    public async Task<TokenJwtDto> AutenticarAsync(AutenticacaoInputModel model)
    {
        var resultado = await _signInManager.PasswordSignInAsync(
            model.Cpf,
            model.Senha,
            false, true);

        if (resultado.Succeeded)
            return await CriarTokenJwtAsync(model.Cpf);

        if (resultado.IsLockedOut)
            throw new FiapInvestApplicationException("Código 01 - Usuário inválido");

        throw new FiapInvestApplicationException("Código 02 - Usuário inválido");
    }
}
