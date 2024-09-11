using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Auth.Application.DTOs;
[ExcludeFromCodeCoverage]
public class UsuarioDTO
{
    public Guid Id { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
}
