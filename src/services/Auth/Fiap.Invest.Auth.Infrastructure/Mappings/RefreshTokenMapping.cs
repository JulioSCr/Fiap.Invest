using Fiap.Invest.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Auth.Infrastructure.Mappings;
[ExcludeFromCodeCoverage]
public class RefreshTokenMapping : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Token).IsRequired();
        builder.Property(r => r.Cpf).IsRequired();
        builder.Property(r => r.DataExpiracao).IsRequired();
        builder.ToTable("RefreshTokens");
    }
}
