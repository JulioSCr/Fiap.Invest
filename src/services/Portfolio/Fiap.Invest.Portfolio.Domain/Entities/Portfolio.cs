using Fiap.Invest.Portfolios.Domain.ValueObjects;

namespace Fiap.Invest.Portfolios.Domain.Entities
{
    public record Portfolio
    {
        public Guid Id { get; private set; }
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
