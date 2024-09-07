using Delivery.Core.DomainObjects;
using Fiap.Invest.Transacoes.Application.Services;
using Fiap.Invest.Transacoes.Domain.DTOs;
using Fiap.Invest.Transacoes.Domain.Entities;
using Fiap.Invest.Transacoes.Domain.Enums;
using Fiap.Invest.Transacoes.Domain.Interfaces.Clients;
using Fiap.Invest.Transacoes.Domain.Interfaces.Repositories;
using Fiap.Invest.Transacoes.Tests.Application.InputModels;
using Moq;
using Moq.AutoMock;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Transacoes.Tests.Application.Services;
[ExcludeFromCodeCoverage]
public class TransacaoServiceTests
{
    private readonly AutoMocker _mocker;

    public TransacaoServiceTests()
    {
        _mocker = new AutoMocker();
    }

    [Fact(DisplayName = "FazerTransacaoAsync Quando Transação Válida Deve Gravar E Retornar Transação")]
    [Trait("Categoria", "TransacaoService")]
    public async Task FazerTransacaoAsync_QuandoTransacaoValida_DeveGravarERetornarTransacao()
    {
        // Arrange
        var model = InputModelTestsFixture.ObterTransacaoInputModelValidoCompra();

        var portfolio = new PortfolioDTO
        {
            Id = model.PortfolioId
        };

        var ativo = new AtivoDTO
        {
            Id = model.AtivoId
        };

        var transacaoRepository = _mocker.GetMock<ITransacaoRepository>();

        var portfolioClient = _mocker.GetMock<IPortfolioClient>();
        portfolioClient
            .Setup(p => p.ListarPortfolioPorUsuario())
            .ReturnsAsync([portfolio]);

        var ativoClient = _mocker.GetMock<IAtivoClient>();
        ativoClient
            .Setup(a => a.ObterAtivoPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(ativo);

        var service = _mocker.CreateInstance<TransacaoService>();

        // Act
        var resultado = await service.FazerTransacaoAsync(model);

        // Assert
        Assert.IsType<Transacao>(resultado);
        transacaoRepository.Verify(t => t.Add(It.IsAny<Transacao>()), Times.Once);
    }

    [Fact(DisplayName = "FazerTransacaoAsync Quando Portfólio Inexistente Deve Retornar Exceção")]
    [Trait("Categoria", "TransacaoService")]
    public async Task FazerTransacaoAsync_QuandoPortfolioInexistente_DeveRetornarExcecao()
    {
        // Arrange
        var transacaoRepository = _mocker.GetMock<ITransacaoRepository>();

        var portfolioClient = _mocker.GetMock<IPortfolioClient>();
        portfolioClient
            .Setup(p => p.ListarPortfolioPorUsuario())
            .ReturnsAsync([]);

        var ativoClient = _mocker.GetMock<IAtivoClient>();
        var service = _mocker.CreateInstance<TransacaoService>();

        // Act
        var erro = async () => await service.FazerTransacaoAsync(InputModelTestsFixture.ObterTransacaoInputModelValidoCompra());

        // Assert
        var excecao = await Assert.ThrowsAsync<ApplicationException>(erro);
        Assert.Equal("Portfólio não encontrado", excecao.Message);
    }

    [Fact(DisplayName = "FazerTransacaoAsync Quando Ativo Inexistente Deve Retornar Exceção")]
    [Trait("Categoria", "TransacaoService")]
    public async Task FazerTransacaoAsync_QuandoAtivoInexistente_DeveRetornarExcecao()
    {
        // Arrange
        var model = InputModelTestsFixture.ObterTransacaoInputModelValidoCompra();

        var portfolio = new PortfolioDTO
        {
            Id = model.PortfolioId
        };

        AtivoDTO? ativo = null;

        var transacaoRepository = _mocker.GetMock<ITransacaoRepository>();

        var portfolioClient = _mocker.GetMock<IPortfolioClient>();
        portfolioClient
            .Setup(p => p.ListarPortfolioPorUsuario())
            .ReturnsAsync([portfolio]);

        var ativoClient = _mocker.GetMock<IAtivoClient>();
        ativoClient
            .Setup(a => a.ObterAtivoPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(ativo);

        var service = _mocker.CreateInstance<TransacaoService>();

        // Act
        var erro = async () => await service.FazerTransacaoAsync(model);

        // Assert
        var excecao = await Assert.ThrowsAsync<ApplicationException>(erro);
        Assert.Equal("Ativo inexistente", excecao.Message);
    }

    [Fact(DisplayName = "FazerTransacaoAsync Quando Venda Supera Quantidade De Ativos No Portfólio Deve Retornar Exceção")]
    [Trait("Categoria", "TransacaoService")]
    public async Task FazerTransacaoAsync_QuandoVendaSuperaQuantidadeDeAtivosNoPortfolio_DeveRetornarExcecao()
    {
        // Arrange
        var model = InputModelTestsFixture.ObterTransacaoInputModelValidoCompra();

        model.Quantidade = 2;
        model.Tipo = ETipoTransacao.Venda;

        var transacao = new Transacao(model.PortfolioId, model.AtivoId, ETipoTransacao.Compra, 1, 20M);

        var portfolio = new PortfolioDTO
        {
            Id = model.PortfolioId
        };

        var ativo = new AtivoDTO
        {
            Id = model.AtivoId
        };

        var transacaoRepository = _mocker.GetMock<ITransacaoRepository>();
        transacaoRepository
            .Setup(t => t.ListarPorPortfolioAsync(It.IsAny<Guid>()))
            .ReturnsAsync([transacao]);

        var portfolioClient = _mocker.GetMock<IPortfolioClient>();
        portfolioClient
            .Setup(p => p.ListarPortfolioPorUsuario())
            .ReturnsAsync([portfolio]);

        var ativoClient = _mocker.GetMock<IAtivoClient>();
        ativoClient
            .Setup(a => a.ObterAtivoPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(ativo);

        var service = _mocker.CreateInstance<TransacaoService>();

        // Act
        var erro = async () => await service.FazerTransacaoAsync(model);

        // Assert
        var excecao = await Assert.ThrowsAsync<ApplicationException>(erro);
        Assert.Equal("Quantidade de ativos no portfólio insuficiente para venda", excecao.Message);
    }

    [Fact(DisplayName = "FazerTransacaoAsync Quando Venda Condiz Com Quantidade De Ativos No Portfólio Deve Retornar Transação")]
    [Trait("Categoria", "TransacaoService")]
    public async Task FazerTransacaoAsync_QuandoVendaCondizComQuantidadeDeAtivosNoPortfolio_DeveRetornarTransacao()
    {
        // Arrange
        var model = InputModelTestsFixture.ObterTransacaoInputModelValidoCompra();

        model.Quantidade = 1;
        model.Tipo = ETipoTransacao.Venda;

        var transacao = new Transacao(model.PortfolioId, model.AtivoId, ETipoTransacao.Compra, 1, 20M);

        var portfolio = new PortfolioDTO
        {
            Id = model.PortfolioId
        };

        var ativo = new AtivoDTO
        {
            Id = model.AtivoId
        };

        var transacaoRepository = _mocker.GetMock<ITransacaoRepository>();
        transacaoRepository
            .Setup(t => t.ListarPorPortfolioAsync(It.IsAny<Guid>()))
            .ReturnsAsync([transacao]);

        var portfolioClient = _mocker.GetMock<IPortfolioClient>();
        portfolioClient
            .Setup(p => p.ListarPortfolioPorUsuario())
            .ReturnsAsync([portfolio]);

        var ativoClient = _mocker.GetMock<IAtivoClient>();
        ativoClient
            .Setup(a => a.ObterAtivoPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(ativo);

        var service = _mocker.CreateInstance<TransacaoService>();

        // Act
        var resultado = await service.FazerTransacaoAsync(model);

        // Assert
        Assert.IsType<Transacao>(resultado);
        transacaoRepository.Verify(t => t.Add(It.IsAny<Transacao>()), Times.Once);
        transacaoRepository.Verify(t => t.ListarPorPortfolioAsync(It.IsAny<Guid>()), Times.AtLeastOnce);
    }

    [Fact(DisplayName = "FazerTransacaoAsync Quando Quantidade Menor Que Mínimo Deve Retornar Exceção")]
    [Trait("Categoria", "TransacaoService")]
    public async Task FazerTransacaoAsync_QuandoQuantidadeMenorQueMinimo_DeveRetornarExcecao()
    {
        // Arrange
        var service = _mocker.CreateInstance<TransacaoService>();

        // Act
        var erro = async () => await service.FazerTransacaoAsync(InputModelTestsFixture.ObterTransacaoInputModelQuantidadeInvalida());

        // Assert
        var excecao = await Assert.ThrowsAsync<DomainException>(erro);
        Assert.Equal($"Quantidade deve ser pelo menos {Transacao.QuantidadeMinima}.", excecao.Message);
    }

    [Fact(DisplayName = "FazerTransacaoAsync Quando Preço Menor Que Mínimo Deve Retornar Exceção")]
    [Trait("Categoria", "TransacaoService")]
    public async Task FazerTransacaoAsync_QuandoPrecoMenorQueMinimo_DeveRetornarExcecao()
    {
        // Arrange
        var service = _mocker.CreateInstance<TransacaoService>();

        // Act
        var erro = async () => await service.FazerTransacaoAsync(InputModelTestsFixture.ObterTransacaoInputModelPrecoInvalido());

        // Assert
        var excecao = await Assert.ThrowsAsync<DomainException>(erro);
        Assert.Equal($"Preço deve ser pelo menos {Transacao.PrecoMinimo}.", excecao.Message);
    }
}
