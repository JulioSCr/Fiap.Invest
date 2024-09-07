using Fiap.Invest.Transacoes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.Invest.Transacoes.Infrastructure.Mappings;
public class TransacaoMapping : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.PortfolioId)
            .IsRequired();

        builder.Property(c => c.AtivoId)
            .IsRequired();

        builder.Property(c => c.Tipo)
            .IsRequired();

        builder.Property(c => c.Quantidade)
            .IsRequired();

        builder.Property(c => c.Preco)
            .IsRequired();

        builder.Property(c => c.DataTransacao)
            .IsRequired();

        builder.ToTable("Transacoes");
    }
}