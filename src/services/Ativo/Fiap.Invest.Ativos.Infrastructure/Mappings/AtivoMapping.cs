using Fiap.Invest.Ativos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.Invest.Ativos.Infrastructure.Mappings;
public class AtivoMapping : IEntityTypeConfiguration<Ativo>
{
    public void Configure(EntityTypeBuilder<Ativo> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Tipo)
            .IsRequired();

        builder.Property(c => c.Nome)
            .IsRequired();

        builder.Property(c => c.Codigo)
            .IsRequired();

        builder.ToTable("Ativos");
    }
}
