using Fiap.Invest.Auth.Domain.Entities;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace Fiap.Invest.Auth.Application.DTOs;
[ExcludeFromCodeCoverage]
public record struct TokenDetalheUsuarioDTO
{
    public string Cpf { get; set; }
    public string Nome { get; set; }
    public IEnumerable<TokenClaimUsuarioDTO> Claims { get; set; }

    public TokenDetalheUsuarioDTO(FiapInvestIdentityUser usuario, IEnumerable<Claim> claims)
    {
        Cpf = usuario.UserName!;
        Nome = usuario.Nome;
        Claims = claims.Select(claim => new TokenClaimUsuarioDTO(claim));
    }
};
