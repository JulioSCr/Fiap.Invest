using Delivery.Core.Data;
using Delivery.Core.Messages;
using FluentValidation.Results;
using Fiap.Invest.Transacoes.Domain.Entities;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Invest.Transacoes.Infrastructure.Context;
[ExcludeFromCodeCoverage]
public sealed class TransacaoContext : DbContext, IUnitOfWork
{
    public TransacaoContext(DbContextOptions<TransacaoContext> options) : base(options) { }

    public DbSet<Transacao> Transacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();

        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TransacaoContext).Assembly);
    }

    public async Task<bool> Commit()
    {
        return await base.SaveChangesAsync() > 0;
    }
}
