using Delivery.Core.Data;
using Delivery.Core.Messages;
using Fiap.Invest.Auth.Domain.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Security.Jwt.Core.Model;
using NetDevPack.Security.Jwt.Store.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Auth.Infrastructure.Context;
[ExcludeFromCodeCoverage]
public sealed class AuthContext : IdentityDbContext, ISecurityKeyContext, IUnitOfWork
{
    public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }
    public DbSet<KeyMaterial> SecurityKeys { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<FiapInvestIdentityUser> FiapInvestIdentityUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Ignore<ValidationResult>();
        builder.Ignore<Event>();

        builder.ApplyConfigurationsFromAssembly(typeof(AuthContext).Assembly);
        base.OnModelCreating(builder);
    }

    public async Task<bool> Commit()
    {
        return await base.SaveChangesAsync() > 0;
    }
}
