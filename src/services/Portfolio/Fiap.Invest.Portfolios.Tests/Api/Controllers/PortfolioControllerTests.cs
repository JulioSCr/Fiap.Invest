using Fiap.Invest.Portfolios.Api.Controllers;
using Fiap.Invest.Portfolios.Application.DTOs;
using Fiap.Invest.Portfolios.Application.InputModels;
using Fiap.Invest.Portfolios.Application.Services;
using Fiap.Invest.Portfolios.Domain.Entities;
using Fiap.Invest.Portfolios.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Portfolios.Tests.Api.Controllers;
[ExcludeFromCodeCoverage]
public class PortfolioControllerTests
{
    private readonly AutoMocker _mocker;

    public PortfolioControllerTests()
    {
        _mocker = new AutoMocker();
    }

    [Fact(DisplayName = "CriarPortfolioAsync Quando Requisição Válida Deve Retornar NoContent")]
    [Trait("Categoria", "PortfolioController")]
    public async Task CriarPortfolioAsync_QuandoRequisicaoValida_DeveRetornarNoContent()
    {
        // Arrange
        var portfolio = new Portfolio(Guid.NewGuid(), new NomePortfolio("Teste"), new DescricaoPortfolio());
        var inputData = new PortfolioInputModel
        {
            UsuarioId = Guid.NewGuid(),
            Nome = "Renda variável",
            Descricao = "Portfólio para renda variável",
        };

        var portfolioService = _mocker.GetMock<IPortfolioService>();
        portfolioService
            .Setup(service => service.CriarPortfolioAsync(It.IsAny<PortfolioInputModel>()))
            .ReturnsAsync(portfolio);
        var controller = new PortfolioController(portfolioService.Object);

        // Act
        var resultado = await controller.CriarPortfolioAsync(inputData);

        // Assert
        var noContentResult = Assert.IsType<NoContentResult>(resultado);
        Assert.Equal(204, noContentResult.StatusCode);
    }

    [Fact(DisplayName = "ListarPortfolioPorUsuario Quando Requisição Válida Deve Retornar Ok")]
    [Trait("Categoria", "PortfolioController")]
    public async Task ListarPortfolioPorUsuario_QuandoRequisicaoValida_DeveRetornarOk()
    {
        // Arrange
        var portfolioService = _mocker.GetMock<IPortfolioService>();
        portfolioService
            .Setup(service => service.ListarPorUsuarioAsync(It.IsAny<Guid>()))
            .ReturnsAsync([]);
        var controller = new PortfolioController(portfolioService.Object);

        // Act
        var resultado = await controller.ListarPortfolioPorUsuario(Guid.NewGuid());

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(resultado);
        Assert.Equal(200, okObjectResult.StatusCode);
        Assert.IsType<List<PortfolioDTO>>(okObjectResult.Value);
    }
}