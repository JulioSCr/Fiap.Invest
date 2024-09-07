using Delivery.Core.Data;
using Delivery.Core.Messages;
using Fiap.Invest.Ativos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Ativos.Infrastructure.Context;
[ExcludeFromCodeCoverage]
public sealed class AtivoContext : DbContext, IUnitOfWork
{
    public AtivoContext(DbContextOptions<AtivoContext> options) : base(options) { }

    public DbSet<Ativo> Ativos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();

        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AtivoContext).Assembly);
    }

    public async Task<bool> Commit()
    {
        return await base.SaveChangesAsync() > 0;
    }
}
