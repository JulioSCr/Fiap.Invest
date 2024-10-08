using Delivery.Core.DomainObjects;
using Fiap.Invest.Transacoes.Domain.Enums;

namespace Fiap.Invest.Transacoes.Domain.Entities;
public class Transacao : Entity, IAggregateRoot
{
    public const int QuantidadeMinima = 1;
    public const decimal PrecoMinimo = 0.1M;
    public const double PrecoMinimoDataAnnotation = 0.1D;

    public Guid PortfolioId { get; private set; }
    public Guid AtivoId { get; private set; }
    public ETipoTransacao Tipo { get; private set; }
    public int Quantidade { get; private set; }
    public decimal Preco { get; private set; }
    public DateTime DataTransacao { get; private set; }

    public Transacao(Guid portfolioId, Guid ativoId, ETipoTransacao tipo, int quantidade, decimal preco)
    {
        if (quantidade < QuantidadeMinima)
            throw new DomainException($"Quantidade deve ser pelo menos {QuantidadeMinima}.");

        if (preco < PrecoMinimo)
            throw new DomainException($"Preço deve ser pelo menos {PrecoMinimo}.");

        PortfolioId = portfolioId;
        AtivoId = ativoId;
        Tipo = tipo;
        Quantidade = quantidade;
        Preco = preco;
        DataTransacao = DateTime.Now;
    }
}