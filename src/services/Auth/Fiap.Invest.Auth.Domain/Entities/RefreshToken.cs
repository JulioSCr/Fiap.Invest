using Delivery.Core.DomainObjects;

namespace Fiap.Invest.Auth.Domain.Entities;
public class RefreshToken : Entity, IAggregateRoot
{
    public Guid Token { get; init; } = Guid.NewGuid();
    public string Cpf { get; init; } = string.Empty;
    public DateTime DataExpiracao { get; init; }
}
