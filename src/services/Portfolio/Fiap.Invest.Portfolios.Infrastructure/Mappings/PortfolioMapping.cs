using Fiap.Invest.Portfolios.Domain.Entities;
using Fiap.Invest.Portfolios.Infrastructure.ValueConversion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.Invest.Portfolios.Infrastructure.Mappings;
public class PortfolioMapping : IEntityTypeConfiguration<Portfolio>
{
    public void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.UsuarioId)
            .IsRequired();

        builder.Property(c => c.Nome)
            .IsRequired()
            .HasConversion(new NomePortfolioConverter())
            .HasColumnType("varchar(20)");

        builder.Property(c => c.Descricao)
            .HasConversion(new DescricaoPortfolioConverter())
            .HasColumnType("varchar(500)");

        builder.ToTable("Portfolios");
    }
}