using Microsoft.AspNetCore.Identity;

namespace Fiap.Invest.Auth.Domain.Entities;
public class FiapInvestIdentityUser : IdentityUser
{
    public string Nome { get; private set; }

    public FiapInvestIdentityUser() { }

    public FiapInvestIdentityUser(string nome, string cpf)
    {
        Nome = Fiap.Invest.Core.ValueObjects.Nome.Formatar(nome);
        UserName = cpf;
    }
}
