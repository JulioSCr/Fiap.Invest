using Delivery.Core.DomainObjects;
using Fiap.Invest.Portfolios.Domain.ValueObjects;

namespace Fiap.Invest.Portfolios.Domain.Entities
{
    public class Portfolio : Entity, IAggregateRoot
    {
        public Guid UsuarioId { get; private set; }
        public NomePortfolio Nome { get; private set; }
        public DescricaoPortfolio Descricao { get; private set; }

        public Portfolio(Guid usuarioId, NomePortfolio nome, DescricaoPortfolio descricao)
        {
            Id = Guid.NewGuid();
            UsuarioId = usuarioId;
            Nome = nome;
            Descricao = descricao;
        }
    }
}
