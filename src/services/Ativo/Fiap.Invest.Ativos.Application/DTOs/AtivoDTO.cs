using Fiap.Invest.Ativos.Domain.Entities;
using Fiap.Invest.Ativos.Domain.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Ativos.Application.DTOs;
[ExcludeFromCodeCoverage]
public class AtivoDTO
{
    public Guid Id { get; set; }
    public ETipoAtivo Tipo { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;

    public AtivoDTO(Ativo ativo)
    {
        Id = ativo.Id;
        Tipo = ativo.Tipo;
        Nome = ativo.Nome;
        Codigo = ativo.Codigo;
    }
}
