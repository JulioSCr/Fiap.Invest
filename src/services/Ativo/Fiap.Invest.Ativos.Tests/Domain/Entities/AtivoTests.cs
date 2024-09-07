using Fiap.Invest.Ativos.Domain.Entities;
using Fiap.Invest.Ativos.Domain.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Ativos.Tests.Domain.Entities;
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
