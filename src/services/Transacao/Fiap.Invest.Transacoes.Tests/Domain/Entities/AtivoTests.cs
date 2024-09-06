using Fiap.Invest.Transacoes.Domain.Entities;
using Fiap.Invest.Transacoes.Domain.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Transacoes.Tests.Domain.Entities;
[ExcludeFromCodeCoverage]
public class AtivoTests
{
    [Fact(DisplayName = "Construtor Quando Fornecido Deve Gerar Instância")]
    [Trait("Categoria", "Ativo")]
    public void Construtor_QuandoFornecido_DeveGerarInstancia()
    {
        // Arrange & Act
        var ativo = new Ativo(ETipoAtivo.Acoes, "Apple Inc", "AAPL");

        // Assert
        Assert.IsType<Ativo>(ativo);
        Assert.NotNull(ativo);
        Assert.NotEqual(Guid.Empty, ativo.Id);
    }
}