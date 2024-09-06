using Fiap.Invest.Transacoes.Domain.Entities;

namespace Fiap.Invest.Transacoes.Infrastructure.Mappings;
public class AtivoMapping : IEntityTypeConfiguration<Ativo>
{
    public void Configure(EntityTypeBuilder<Ativo> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Tipo)
            .IsRequired();

        builder.Property(c => c.Nome)
            .IsRequired()
            .HasColumnType("varchar(20)");

        builder.Property(c => c.Codigo)
            .IsRequired()
            .HasConversion(new DescricaoPortfolioConverter());

        builder.ToTable("Portfolios");
    }
}