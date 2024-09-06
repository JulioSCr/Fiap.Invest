using Delivery.Core.DomainObjects;
using Fiap.Invest.Transacoes.Domain.Enums;

namespace Fiap.Invest.Transacoes.Domain.Entities;
public class Ativo : Entity
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