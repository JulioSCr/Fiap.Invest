using Delivery.Core.DomainObjects;
using Fiap.Invest.Ativos.Domain.Enums;

namespace Fiap.Invest.Ativos.Domain.Entities;
public class Ativo : Entity, IAggregateRoot
{
    public ETipoAtivo Tipo { get; private set; }
    public string Nome { get; private set; }
    public string Codigo { get; private set; }

    public Ativo(ETipoAtivo tipo, string nome, string codigo)
    {
        Tipo = tipo;
        Nome = nome;
        Codigo = codigo;
    }
}
