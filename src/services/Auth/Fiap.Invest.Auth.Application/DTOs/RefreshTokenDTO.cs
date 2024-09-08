using Fiap.Invest.Auth.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Auth.Application.DTOs;
[ExcludeFromCodeCoverage]
public record struct RefreshTokenDTO
{
    public Guid Token { get; set; }
    public string? Cpf { get; set; }
    public DateTime DataExpiracao { get; set; }

    public RefreshTokenDTO(RefreshToken refreshToken)
    {
        Token = refreshToken.Token;
        Cpf = refreshToken.Cpf;
        DataExpiracao = refreshToken.DataExpiracao;
    }
}
