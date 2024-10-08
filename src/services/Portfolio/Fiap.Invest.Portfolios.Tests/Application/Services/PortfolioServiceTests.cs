using Delivery.Core.Data;
using Delivery.WebAPI.Core.User;
using Fiap.Invest.Core.Exceptions;
using Fiap.Invest.Portfolios.Application.DTOs;
using Fiap.Invest.Portfolios.Application.InputModels;
using Fiap.Invest.Portfolios.Application.Services;
using Fiap.Invest.Portfolios.Domain.Entities;
using Fiap.Invest.Portfolios.Domain.Interfaces.Repositories;
using Fiap.Invest.Portfolios.Domain.ValueObjects;
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

    [Fact(DisplayName = "CriarPortfolioAsync Quando Portfólio Válido Deve Gravar E Retornar Portfólio Com Dados Inseridos")]
    [Trait("Categoria", "PortfolioService")]
    public async Task CriarPortfolioAsync_QuandoPortfolioValido_DeveGravarERetornarPortfolioComDadosInseridos()
    {
        // Arrange
        var inputData = new PortfolioInputModel
        {
            Nome = "Renda variável",
            Descricao = "Portfólio para renda variável",
        };

        var unitOfWork = _mocker.GetMock<IUnitOfWork>();
        unitOfWork
            .Setup(u => u.Commit())
            .ReturnsAsync(true);

        var portfolioRepository = _mocker.GetMock<IPortfolioRepository>();
        portfolioRepository
            .Setup(repo => repo.ListarPorUsuarioAsync(It.IsAny<Guid>()))
            .ReturnsAsync([]);

        portfolioRepository
            .Setup(repo => repo.UnitOfWork)
            .Returns(unitOfWork.Object);

        _mocker
            .GetMock<IAspNetUser>()
            .Setup(u => u.Name)
            .Returns(Guid.NewGuid().ToString());

        var service = _mocker.CreateInstance<PortfolioService>();

        // Act
        var resultado = await service.CriarPortfolioAsync(inputData);

        // Assert
        Assert.IsType<Portfolio>(resultado);
        Assert.Equal(NomePortfolio.Formatar(inputData.Nome), resultado.Nome.Valor);
        Assert.Equal(inputData.Descricao, resultado.Descricao.Valor);
        Assert.NotEqual(Guid.Empty, resultado.Id);
        portfolioRepository
            .Verify(repo => repo.Add(It.IsAny<Portfolio>()), Times.Once);
    }

    [Fact(DisplayName = "CriarPortfolioAsync Quando Portfólio Válido Deve Gravar E Retornar Portfólio Com Dados Inseridos")]
    [Trait("Categoria", "PortfolioService")]
    public async Task CriarPortfolioAsync_QuandoPersistenciaFalha_DeveRetornarExcecao()
    {
        // Arrange
        var inputData = new PortfolioInputModel
        {
            Nome = "Renda variável",
            Descricao = "Portfólio para renda variável",
        };

        var unitOfWork = _mocker.GetMock<IUnitOfWork>();
        unitOfWork
            .Setup(u => u.Commit())
            .ReturnsAsync(false);

        var portfolioRepository = _mocker.GetMock<IPortfolioRepository>();
        portfolioRepository
            .Setup(repo => repo.ListarPorUsuarioAsync(It.IsAny<Guid>()))
            .ReturnsAsync([]);

        portfolioRepository
            .Setup(repo => repo.UnitOfWork)
            .Returns(unitOfWork.Object);

        _mocker
            .GetMock<IAspNetUser>()
            .Setup(u => u.Name)
            .Returns(Guid.NewGuid().ToString());

        var service = _mocker.CreateInstance<PortfolioService>();

        // Act
        var erro = (async () => { var resultado = await service.CriarPortfolioAsync(inputData); });

        // Assert
        var excecao = await Assert.ThrowsAsync<FiapInvestApplicationException>(erro);
        Assert.Equal("Falha ao persistir transação.", excecao.Message);
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
            .Setup(repo => repo.ListarPorUsuarioAsync(It.IsAny<Guid>()))
            .ReturnsAsync([portfolio]);
        var service = _mocker.CreateInstance<PortfolioService>();

        _mocker
            .GetMock<IAspNetUser>()
            .Setup(u => u.Name)
            .Returns(Guid.NewGuid().ToString());

        // Act
        var erro = (async () => { var resultado = await service.CriarPortfolioAsync(inputData); });

        // Assert
        var excecao = await Assert.ThrowsAsync<FiapInvestApplicationException>(erro);
        Assert.Equal(mensagem, excecao.Message);
    }

    [Fact(DisplayName = "ListarPorUsuarioAsync Quando Requisitado Deve Retornar Lista De PortfolioDTO")]
    [Trait("Categoria", "PortfolioService")]
    public async Task ListarPorUsuarioAsync_QuandoRequisitado_DeveRetornarListaDePortfolioDTO()
    {
        var portfolioRepository = _mocker.GetMock<IPortfolioRepository>();
        portfolioRepository
            .Setup(repo => repo.ListarPorUsuarioAsync(It.IsAny<Guid>()))
            .ReturnsAsync([]);

        _mocker
            .GetMock<IAspNetUser>()
            .Setup(u => u.Name)
            .Returns(Guid.NewGuid().ToString());

        var service = _mocker.CreateInstance<PortfolioService>();

        // Act
        var portfolios = await service.ListarPorUsuarioAsync();

        // Assert
        Assert.IsType<List<PortfolioDTO>>(portfolios);
    }
}