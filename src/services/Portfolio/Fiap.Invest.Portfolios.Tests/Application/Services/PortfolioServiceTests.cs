using Fiap.Invest.Portfolios.Application.InputModels;
using Fiap.Invest.Portfolios.Application.Services;
using Fiap.Invest.Portfolios.Domain.Entities;
using Fiap.Invest.Portfolios.Domain.ValueObjects;
using Fiap.Invest.Portfolios.Domain.Interfaces.Repositories;
using Moq;
using Moq.AutoMock;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Portfolios.Tests.Application.Services;

[ExcludeFromCodeCoverage]
public class PortfolioServiceTests
{
    private readonly AutoMocker _mocker;

    public PortfolioServiceTests()
    {
        _mocker = new AutoMocker();
    }

    [Fact(DisplayName = "CriarPortfolioAsync Quando Portfólio Válido Deve Gravar Retornar Portfólio Com Dados Inseridos")]
    [Trait("Categoria", "PortfolioService")]
    public async Task CriarPortfolioAsync_QuandoPortfolioValido_DeveGravarERetornarPortfolioComDadosInseridos()
    {
        // Arrange
        var inputData = new PortfolioInputModel
        {
            UsuarioId = Guid.NewGuid(),
            Nome = "Renda variável",
            Descricao = "Portfólio para renda variável",
        };

        var portfolioRepository = _mocker.GetMock<IPortfolioRepository>();
        portfolioRepository
            .Setup(repo => repo.ObterPorUsuarioAsync(It.IsAny<Guid>()))
            .ReturnsAsync([]);
        var service = new PortfolioService(portfolioRepository.Object);

        // Act
        var resultado = await service.CriarPortfolioAsync(inputData);

        // Assert
        Assert.IsType<Portfolio>(resultado);
        Assert.Equal(inputData.UsuarioId, resultado.UsuarioId);
        Assert.Equal(NomePortfolio.Formatar(inputData.Nome), resultado.Nome.Valor);
        Assert.Equal(inputData.Descricao, resultado.Descricao.Valor);
        Assert.NotEqual(Guid.Empty, resultado.Id);
        portfolioRepository
            .Verify(repo => repo.Add(It.IsAny<Portfolio>()), Times.Once);
    }

    [Fact(DisplayName = "CriarPortfolioAsync Quando Portfólio Com Mesmo Nome Para Mesmo Usuario Deve Retornar Exceção")]
    [Trait("Categoria", "PortfolioService")]
    public async Task CriarPortfolioAsync_QuandoPortfolioComMesmoNomeParaMesmoUsuario_DeveRetornarExcecao()
    {
        // Arrange
        var nome = new NomePortfolio("Renda variável");
        var descricao = new DescricaoPortfolio();
        var portfolio = new Portfolio(Guid.NewGuid(), nome, descricao);

        var mensagem = $"Portfólio de nome \"{portfolio.Nome}\" já existe.";

        var inputData = new PortfolioInputModel
        {
            Nome = nome.Valor
        };
        var portfolioRepository = _mocker.GetMock<IPortfolioRepository>();
        portfolioRepository
            .Setup(repo => repo.ObterPorUsuarioAsync(It.IsAny<Guid>()))
            .ReturnsAsync([portfolio]);
        var service = new PortfolioService(portfolioRepository.Object);

        // Act
        var erro = (async () => { var resultado = await service.CriarPortfolioAsync(inputData); });

        // Assert
        var excecao = await Assert.ThrowsAsync<InvalidOperationException>(erro);
        Assert.Equal(mensagem, excecao.Message);
    }
}