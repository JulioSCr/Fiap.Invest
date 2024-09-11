using Delivery.Core.DomainObjects;
using Fiap.Invest.Core.Exceptions;
using Fiap.Invest.Transacoes.Api.Controllers;
using Fiap.Invest.Transacoes.Application.DTOs;
using Fiap.Invest.Transacoes.Application.InputModels;
using Fiap.Invest.Transacoes.Application.Services;
using Fiap.Invest.Transacoes.Domain.Entities;
using Fiap.Invest.Transacoes.Domain.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Transacoes.Tests.Api.Controllers;
[ExcludeFromCodeCoverage]
public class TransacaoControllerTests
{
    private readonly AutoMocker _mocker;

    public TransacaoControllerTests()
    {
        _mocker = new AutoMocker();
    }

    #region FazerTransacaoAsync
    [Fact(DisplayName = "FazerTransacaoAsync Quando Requisição Válida Deve Retornar NoContent")]
    [Trait("Categoria", "TransacaoController")]
    public async Task FazerTransacaoAsync_QuandoRequisicaoValida_DeveRetornarNoContent()
    {
        // Arrange
        var transacao = new Transacao(Guid.NewGuid(), Guid.NewGuid(), ETipoTransacao.Compra, 1, 10M);
        var inputData = new TransacaoInputModel();

        var transacaoService = _mocker.GetMock<ITransacaoService>();
        transacaoService
            .Setup(service => service.FazerTransacaoAsync(It.IsAny<TransacaoInputModel>()))
            .ReturnsAsync(transacao);
        var controller = new TransacaoController(transacaoService.Object);

        // Act
        var resultado = await controller.FazerTransacaoAsync(inputData);

        // Assert
        Assert.IsType<CreatedResult>(resultado);
    }

    [Fact(DisplayName = "FazerTransacaoAsync Quando Requisição Falha Domain Deve Retornar BadRequest")]
    [Trait("Categoria", "TransacaoController")]
    public async Task FazerTransacaoAsync_QuandoRequisicaoFalhaDomain_DeveRetornarBadRequest()
    {
        // Arrange
        var transacao = new Transacao(Guid.NewGuid(), Guid.NewGuid(), ETipoTransacao.Compra, 1, 10M);
        var inputData = new TransacaoInputModel();

        var transacaoService = _mocker.GetMock<ITransacaoService>();
        transacaoService
            .Setup(service => service.FazerTransacaoAsync(It.IsAny<TransacaoInputModel>()))
            .Throws<DomainException>();
        var controller = new TransacaoController(transacaoService.Object);

        // Act
        var resultado = await controller.FazerTransacaoAsync(inputData);

        // Assert
        var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(resultado);
        Assert.Equal(400, badRequestObjectResult.StatusCode);
    }

    [Fact(DisplayName = "FazerTransacaoAsync Quando Requisição Falha Application Deve Retornar BadRequest")]
    [Trait("Categoria", "TransacaoController")]
    public async Task FazerTransacaoAsync_QuandoRequisicaoFalhaApplication_DeveRetornarBadRequest()
    {
        // Arrange
        var transacao = new Transacao(Guid.NewGuid(), Guid.NewGuid(), ETipoTransacao.Compra, 1, 10M);
        var inputData = new TransacaoInputModel();

        var transacaoService = _mocker.GetMock<ITransacaoService>();
        transacaoService
            .Setup(service => service.FazerTransacaoAsync(It.IsAny<TransacaoInputModel>()))
            .Throws<FiapInvestApplicationException>();
        var controller = new TransacaoController(transacaoService.Object);

        // Act
        var resultado = await controller.FazerTransacaoAsync(inputData);

        // Assert
        var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(resultado);
        Assert.Equal(400, badRequestObjectResult.StatusCode);
    }
    #endregion

    #region ObterSaldoAsync
    [Fact(DisplayName = "ObterSaldoAsync Quando Requisição Válida Deve Retornar Ok Com SaldoAtivoDTO")]
    [Trait("Categoria", "TransacaoController")]
    public async Task ObterSaldoAsync_QuandoRequisicaoValida_DeveRetornarOkComSaldoAtivoDTO()
    {
        // Arrange
        var saldo = new SaldoAtivoDTO();

        var transacaoService = _mocker.GetMock<ITransacaoService>();
        transacaoService
            .Setup(t => t.ObterSaldoAtivoPorPortfolioAsync(It.IsAny<Guid>()))
            .ReturnsAsync([saldo]);

        var controller = _mocker.CreateInstance<TransacaoController>();

        // Act
        var resultado = await controller.ObterSaldoAsync(Guid.NewGuid());

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(resultado);
        Assert.IsType<List<SaldoAtivoDTO>>(okObjectResult.Value);
        Assert.Equal(200, okObjectResult.StatusCode);
    }

    [Fact(DisplayName = "ObterSaldoAsync Quando Retorno Vazio Deve Retornar NoContent")]
    [Trait("Categoria", "TransacaoController")]
    public async Task ObterSaldoAsync_QuandoRetornoVazio_DeveRetornarNoContent()
    {
        // Arrange
        var transacaoService = _mocker.GetMock<ITransacaoService>();
        transacaoService
            .Setup(t => t.ObterSaldoAtivoPorPortfolioAsync(It.IsAny<Guid>()))
            .ReturnsAsync([]);

        var controller = _mocker.CreateInstance<TransacaoController>();

        // Act
        var resultado = await controller.ObterSaldoAsync(Guid.NewGuid());

        // Assert
        var noContentResult = Assert.IsType<NoContentResult>(resultado);
        Assert.Equal(204, noContentResult.StatusCode);
    }

    [Fact(DisplayName = "ObterSaldoAsync Quando Exceção Domínio Deve Retornar BadRequest")]
    [Trait("Categoria", "TransacaoController")]
    public async Task ObterSaldoAsync_QuandoExcecaoDominio_DeveRetornarBadRequest()
    {
        // Arrange
        var transacaoService = _mocker.GetMock<ITransacaoService>();
        transacaoService
            .Setup(t => t.ObterSaldoAtivoPorPortfolioAsync(It.IsAny<Guid>()))
            .Throws<DomainException>();

        var controller = _mocker.CreateInstance<TransacaoController>();

        // Act
        var resultado = await controller.ObterSaldoAsync(Guid.NewGuid());

        // Assert
        var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(resultado);
        Assert.Equal(400, badRequestObjectResult.StatusCode);
    }

    [Fact(DisplayName = "ObterSaldoAsync Quando Exceção Application Deve Retornar BadRequest")]
    [Trait("Categoria", "TransacaoController")]
    public async Task ObterSaldoAsync_QuandoExcecaoApplication_DeveRetornarBadRequest()
    {
        // Arrange
        var transacaoService = _mocker.GetMock<ITransacaoService>();
        transacaoService
            .Setup(t => t.ObterSaldoAtivoPorPortfolioAsync(It.IsAny<Guid>()))
            .Throws<FiapInvestApplicationException>();

        var controller = _mocker.CreateInstance<TransacaoController>();

        // Act
        var resultado = await controller.ObterSaldoAsync(Guid.NewGuid());

        // Assert
        var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(resultado);
        Assert.Equal(400, badRequestObjectResult.StatusCode);
    }

    [Fact(DisplayName = "ObterSaldoAsync Quando Exceção DataNotFound Deve Retornar BadRequest")]
    [Trait("Categoria", "TransacaoController")]
    public async Task ObterSaldoAsync_QuandoExcecaoDataNotFound_DeveRetornarBadRequest()
    {
        // Arrange
        var transacaoService = _mocker.GetMock<ITransacaoService>();
        transacaoService
            .Setup(t => t.ObterSaldoAtivoPorPortfolioAsync(It.IsAny<Guid>()))
            .Throws<DataNotFoundException>();

        var controller = _mocker.CreateInstance<TransacaoController>();

        // Act
        var resultado = await controller.ObterSaldoAsync(Guid.NewGuid());

        // Assert
        var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(resultado);
        Assert.Equal(400, badRequestObjectResult.StatusCode);
    }
    #endregion
}

