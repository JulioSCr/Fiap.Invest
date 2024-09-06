using Fiap.Invest.Transacoes.Domain.Entities;
using Fiap.Invest.Transacoes.Domain.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Transacoes.Tests.Domain.Entities;
[ExcludeFromCodeCoverage]
public class TransacaoTests
{
    [Fact(DisplayName = "Construtor Quando Fornecido Deve Gerar Instância")]
    [Trait("Categoria", "Transacao")]
    public void Construtor_QuandoFornecido_DeveGerarInstancia()
    {
        // Arrange & Act
        var transacao = new Transacao(Guid.NewGuid(), Guid.NewGuid(), ETipoTransacao.Compra, Transacao.QuantidadeMinima, Transacao.PrecoMinimo);

        // Assert
        Assert.IsType<Transacao>(transacao);
        Assert.NotNull(transacao);
        Assert.NotEqual(Guid.Empty, transacao.Id);
        Assert.NotEqual(default(DateTime), transacao.DataTransacao);
    }

    [Fact(DisplayName = "Construtor Quando Quantidade Menor Que Mínimo Deve Gerar Exceção")]
    [Trait("Categoria", "Transacao")]
    public void Construtor_QuandoQuantidadeMenorQueMinimo_DeveGerarExcecao()
    {
        // Arrange
        var mensagem = "Quantidade deve ser maior que zero.";

        // Act
        var acaoExcecao = () =>
        {
            var transacao = new Transacao(Guid.NewGuid(), Guid.NewGuid(), ETipoTransacao.Compra, Transacao.QuantidadeMinima - 1, Transacao.PrecoMinimo);
        };

        // Assert
        var excecao = Assert.Throws<InvalidOperationException>(acaoExcecao);
        Assert.Equal(mensagem, excecao.Message);
    }

    [Fact(DisplayName = "Construtor Quando Preço Menor Que Mínimo Deve Gerar Exceção")]
    [Trait("Categoria", "Transacao")]
    public void Construtor_QuandoPrecoMenorQueMinimo_DeveGerarExcecao()
    {
        // Arrange
        var mensagem = "Preço deve ser maior que zero.";

        // Act
        var acaoExcecao = () =>
        {
            var transacao = new Transacao(Guid.NewGuid(), Guid.NewGuid(), ETipoTransacao.Compra, Transacao.QuantidadeMinima, Transacao.PrecoMinimo - 1);
        };

        // Assert
        var excecao = Assert.Throws<InvalidOperationException>(acaoExcecao);
        Assert.Equal(mensagem, excecao.Message);
    }
}