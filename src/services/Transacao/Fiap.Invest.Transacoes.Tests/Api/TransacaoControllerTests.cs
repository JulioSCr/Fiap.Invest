using Delivery.Core.DomainObjects;
using Fiap.Invest.Core.Exceptions;
using Fiap.Invest.Transacoes.Api.Controllers;
using Fiap.Invest.Transacoes.Application.InputModels;
using Fiap.Invest.Transacoes.Application.Services;
using Fiap.Invest.Transacoes.Domain.Entities;
using Fiap.Invest.Transacoes.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Transacoes.Tests.Api;
[ExcludeFromCodeCoverage]
public class TransacaoControllerTests
{
    private readonly AutoMocker _mocker;

    public TransacaoControllerTests()
    {
        _mocker = new AutoMocker();
    }

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
        var noContentResult = Assert.IsType<NoContentResult>(resultado);
        Assert.Equal(204, noContentResult.StatusCode);
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
}

