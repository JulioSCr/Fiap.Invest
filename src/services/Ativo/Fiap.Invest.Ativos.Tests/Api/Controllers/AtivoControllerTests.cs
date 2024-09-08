using Delivery.Core.DomainObjects;
using Fiap.Invest.Ativos.Api.Controllers;
using Fiap.Invest.Ativos.Application.DTOs;
using Fiap.Invest.Ativos.Application.InputModels;
using Fiap.Invest.Ativos.Application.Services;
using Fiap.Invest.Ativos.Domain.Entities;
using Fiap.Invest.Ativos.Domain.Enums;
using Fiap.Invest.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Ativos.Tests.Api.Controllers;
[ExcludeFromCodeCoverage]
public class AtivoControllerTests
{
    private readonly AutoMocker _mocker;

    public AtivoControllerTests()
    {
        _mocker = new AutoMocker();
    }

    #region ObterAtivoAsync
    [Fact(DisplayName = "ObterAtivoAsync Quando Requisição Válida Deve Retornar Ok Com AtivoDTO")]
    [Trait("Categoria", "AtivoController")]
    public async Task ObterAtivoAsync_QuandoRequisicaoValida_DeveRetornarOkComAtivoDTO()
    {
        // Arrange
        var ativo = new AtivoDTO(new Ativo(ETipoAtivo.Acoes, "Teste", "TEST"));

        var ativoService = _mocker.GetMock<IAtivoService>();
        ativoService
            .Setup(service => service.ObterAtivoAsync(It.IsAny<Guid>()))
            .ReturnsAsync(ativo);
        var controller = new AtivoController(ativoService.Object);

        // Act
        var resultado = await controller.ObterAtivoAsync(Guid.NewGuid());

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(resultado);
        Assert.IsType<AtivoDTO>(okObjectResult.Value);
        Assert.Equal(200, okObjectResult.StatusCode);
    }

    [Fact(DisplayName = "ObterAtivoAsync Quando Requisição Falha Domínio Deve Retornar BadRequest")]
    [Trait("Categoria", "AtivoController")]
    public async Task ObterAtivoAsync_QuandoRequisicaoFalhaDominio_DeveRetornarBadRequest()
    {
        // Arrange
        var ativoService = _mocker.GetMock<IAtivoService>();
        ativoService
            .Setup(service => service.ObterAtivoAsync(It.IsAny<Guid>()))
            .Throws<DomainException>();

        var controller = new AtivoController(ativoService.Object);

        // Act
        var resultado = await controller.ObterAtivoAsync(Guid.NewGuid());

        // Assert
        var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(resultado);
        Assert.Equal(400, badRequestObjectResult.StatusCode);
    }

    [Fact(DisplayName = "ObterAtivoAsync Quando Requisição Falha Application Deve Retornar BadRequest")]
    [Trait("Categoria", "AtivoController")]
    public async Task ObterAtivoAsync_QuandoRequisicaoFalhaApplication_DeveRetornarBadRequest()
    {
        // Arrange
        var ativoService = _mocker.GetMock<IAtivoService>();
        ativoService
            .Setup(service => service.ObterAtivoAsync(It.IsAny<Guid>()))
            .Throws<FiapInvestApplicationException>();

        var controller = new AtivoController(ativoService.Object);

        // Act
        var resultado = await controller.ObterAtivoAsync(Guid.NewGuid());

        // Assert
        var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(resultado);
        Assert.Equal(400, badRequestObjectResult.StatusCode);
    }

    [Fact(DisplayName = "ObterAtivoAsync Quando Requisição Falha DataNotFound Deve Retornar NoContent")]
    [Trait("Categoria", "AtivoController")]
    public async Task ObterAtivoAsync_QuandoRequisicaoFalhaDataNotFound_DeveRetornarNoContent()
    {
        // Arrange
        var ativoService = _mocker.GetMock<IAtivoService>();
        ativoService
            .Setup(service => service.ObterAtivoAsync(It.IsAny<Guid>()))
            .Throws<DataNotFoundException>();

        var controller = new AtivoController(ativoService.Object);

        // Act
        var resultado = await controller.ObterAtivoAsync(Guid.NewGuid());

        // Assert
        var noContentResult = Assert.IsType<NoContentResult>(resultado);
        Assert.Equal(204, noContentResult.StatusCode);
    }

    [Fact(DisplayName = "ObterAtivoAsync Quando Dado Não Existe Deve Retornar NoContent")]
    [Trait("Categoria", "AtivoController")]
    public async Task ObterAtivoAsync_QuandoDadoNaoExiste_DeveRetornarNoContent()
    {
        // Arrange
        AtivoDTO? ativo = null;

        var ativoService = _mocker.GetMock<IAtivoService>();
        ativoService
            .Setup(service => service.ObterAtivoAsync(It.IsAny<Guid>()))
            .ReturnsAsync(ativo);

        var controller = new AtivoController(ativoService.Object);

        // Act
        var resultado = await controller.ObterAtivoAsync(Guid.NewGuid());

        // Assert
        var noContentResult = Assert.IsType<NoContentResult>(resultado);
        Assert.Equal(204, noContentResult.StatusCode);
    }
    #endregion

    #region ListarAtivosPaginadosAsync
    [Fact(DisplayName = "ListarAtivosPaginadosAsync Quando Requisição Válida Deve Retornar Ok Com PagedResult De AtivoDTO")]
    [Trait("Categoria", "AtivoController")]
    public async Task ListarAtivosPaginadosAsync_QuandoRequisicaoValida_DeveRetornarOkComPagedResultDeAtivoDTO()
    {
        // Arrange
        var ativo = new AtivoDTO(new Ativo(ETipoAtivo.Acoes, "Teste", "TEST"));
        var ativoPaginado = new PagedResult<AtivoDTO>
        {
            List = [ativo],
            TotalResults = 1,
            PageIndex = 1,
            PageSize = 1
        };

        var pageSize = 1;
        var pageIndex = 1;

        var ativoService = _mocker.GetMock<IAtivoService>();
        ativoService
            .Setup(service => service.ListarAtivosAsync(pageSize, pageIndex))
            .ReturnsAsync(ativoPaginado);
        var controller = new AtivoController(ativoService.Object);

        // Act
        var resultado = await controller.ListarAtivosPaginadosAsync(pageSize, pageIndex);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(resultado);
        Assert.IsType<PagedResult<AtivoDTO>>(okObjectResult.Value);
        Assert.Equal(200, okObjectResult.StatusCode);
    }

    [Fact(DisplayName = "ListarAtivosPaginadosAsync Quando Requisição Falha Domínio Deve Retornar BadRequest")]
    [Trait("Categoria", "AtivoController")]
    public async Task ListarAtivosPaginadosAsync_QuandoRequisicaoFalhaDominio_DeveRetornarBadRequest()
    {
        // Arrange
        var ativoService = _mocker.GetMock<IAtivoService>();
        ativoService
            .Setup(service => service.ListarAtivosAsync(It.IsAny<int>(), It.IsAny<int>()))
            .Throws<DomainException>();

        var controller = new AtivoController(ativoService.Object);

        // Act
        var resultado = await controller.ListarAtivosPaginadosAsync(1, 1);

        // Assert
        var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(resultado);
        Assert.Equal(400, badRequestObjectResult.StatusCode);
    }

    [Fact(DisplayName = "ListarAtivosPaginadosAsync Quando Requisição Falha Application Deve Retornar BadRequest")]
    [Trait("Categoria", "AtivoController")]
    public async Task ListarAtivosPaginadosAsync_QuandoRequisicaoFalhaApplication_DeveRetornarBadRequest()
    {
        // Arrange
        var ativoService = _mocker.GetMock<IAtivoService>();
        ativoService
            .Setup(service => service.ListarAtivosAsync(It.IsAny<int>(), It.IsAny<int>()))
            .Throws<FiapInvestApplicationException>();

        var controller = new AtivoController(ativoService.Object);

        // Act
        var resultado = await controller.ListarAtivosPaginadosAsync(1, 1);

        // Assert
        var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(resultado);
        Assert.Equal(400, badRequestObjectResult.StatusCode);
    }

    [Fact(DisplayName = "ListarAtivosPaginadosAsync Quando Requisição Falha DataNotFound Deve Retornar NoContent")]
    [Trait("Categoria", "AtivoController")]
    public async Task ListarAtivosPaginadosAsync_QuandoRequisicaoFalhaDataNotFound_DeveRetornarNoContent()
    {
        // Arrange
        var ativoService = _mocker.GetMock<IAtivoService>();
        ativoService
            .Setup(service => service.ListarAtivosAsync(It.IsAny<int>(), It.IsAny<int>()))
            .Throws<DataNotFoundException>();

        var controller = new AtivoController(ativoService.Object);

        // Act
        var resultado = await controller.ListarAtivosPaginadosAsync(1, 1);

        // Assert
        var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(resultado);
        Assert.Equal(400, badRequestObjectResult.StatusCode);
    }

    [Fact(DisplayName = "ListarAtivosPaginadosAsync Quando Dado Não Existe Deve Retornar NoContent")]
    [Trait("Categoria", "AtivoController")]
    public async Task ListarAtivosPaginadosAsync_QuandoDadoNaoExiste_DeveRetornarNoContent()
    {
        // Arrange
        var ativoPaginado = new PagedResult<AtivoDTO>
        {
            List = [],
            TotalResults = 0,
            PageIndex = 1,
            PageSize = 1
        };

        var ativoService = _mocker.GetMock<IAtivoService>();
        ativoService
            .Setup(service => service.ListarAtivosAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(ativoPaginado);

        var controller = new AtivoController(ativoService.Object);

        // Act
        var resultado = await controller.ListarAtivosPaginadosAsync(1, 1);

        // Assert
        var noContentResult = Assert.IsType<NoContentResult>(resultado);
        Assert.Equal(204, noContentResult.StatusCode);
    }
    #endregion

    #region CriarAtivoAsync
    [Fact(DisplayName = "CriarAtivoAsync Quando Requisição Válida Deve Retornar Created")]
    [Trait("Categoria", "AtivoController")]
    public async Task CriarAtivoAsync_QuandoRequisicaoValida_DeveRetornarCreated()
    {
        // Arrange
        var model = new AtivoInputModel();

        var ativo = new Ativo(ETipoAtivo.Titulos, "Teste", "TEST");

        _mocker
            .GetMock<IAtivoService>()
            .Setup(s => s.AdicionarAtivoAsync(It.IsAny<AtivoInputModel>()))
            .ReturnsAsync(ativo);
        var controller = _mocker.CreateInstance<AtivoController>();

        // Act
        var resultado = await controller.CriarAtivoAsync(model);

        // Assert
        Assert.IsType<CreatedResult>(resultado);
    }

    [Fact(DisplayName = "CriarAtivoAsync Quando Requisição Falha Domínio Deve Retornar BadRequest")]
    [Trait("Categoria", "AtivoController")]
    public async Task CriarAtivoAsync_QuandoRequisicaoFalhaDominio_DeveRetornarBadRequest()
    {
        // Arrange
        var model = new AtivoInputModel();

        var ativoService = _mocker.GetMock<IAtivoService>();
        ativoService
            .Setup(service => service.AdicionarAtivoAsync(It.IsAny<AtivoInputModel>()))
            .Throws<DomainException>();

        var controller = new AtivoController(ativoService.Object);

        // Act
        var resultado = await controller.CriarAtivoAsync(model);

        // Assert
        var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(resultado);
        Assert.Equal(400, badRequestObjectResult.StatusCode);
    }

    [Fact(DisplayName = "CriarAtivoAsync Quando Requisição Falha Application Deve Retornar BadRequest")]
    [Trait("Categoria", "AtivoController")]
    public async Task CriarAtivoAsync_QuandoRequisicaoFalhaApplication_DeveRetornarBadRequest()
    {
        // Arrange
        var model = new AtivoInputModel();

        var ativoService = _mocker.GetMock<IAtivoService>();
        ativoService
            .Setup(service => service.AdicionarAtivoAsync(It.IsAny<AtivoInputModel>()))
            .Throws<FiapInvestApplicationException>();

        var controller = new AtivoController(ativoService.Object);

        // Act
        var resultado = await controller.CriarAtivoAsync(model);

        // Assert
        var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(resultado);
        Assert.Equal(400, badRequestObjectResult.StatusCode);
    }

    [Fact(DisplayName = "CriarAtivoAsync Quando Requisição Falha DataNotFound Deve Retornar NoContent")]
    [Trait("Categoria", "AtivoController")]
    public async Task CriarAtivoAsync_QuandoRequisicaoFalhaDataNotFound_DeveRetornarNoContent()
    {
        // Arrange
        var model = new AtivoInputModel();

        var ativoService = _mocker.GetMock<IAtivoService>();
        ativoService
            .Setup(service => service.AdicionarAtivoAsync(It.IsAny<AtivoInputModel>()))
            .Throws<DataNotFoundException>();

        var controller = new AtivoController(ativoService.Object);

        // Act
        var resultado = await controller.CriarAtivoAsync(model);

        // Assert
        var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(resultado);
        Assert.Equal(400, badRequestObjectResult.StatusCode);
    }
    #endregion
}
