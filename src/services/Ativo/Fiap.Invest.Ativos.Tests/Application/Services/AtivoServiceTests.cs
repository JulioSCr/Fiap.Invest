using Delivery.Core.Data;
using Delivery.Core.DomainObjects;
using Fiap.Invest.Ativos.Application.DTOs;
using Fiap.Invest.Ativos.Application.InputModels;
using Fiap.Invest.Ativos.Application.Services;
using Fiap.Invest.Ativos.Domain.Entities;
using Fiap.Invest.Ativos.Domain.Enums;
using Fiap.Invest.Ativos.Domain.Interfaces.Repositories;
using Moq;
using Moq.AutoMock;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Ativos.Tests.Application.Services;
[ExcludeFromCodeCoverage]
public class AtivoServiceTests
{
    private readonly AutoMocker _mocker;

    public AtivoServiceTests()
    {
        _mocker = new AutoMocker();
    }

    [Fact(DisplayName = "ObterAtivoAsync Quando Ativo Existe Deve Retornar DTO")]
    [Trait("Categoria", "AtivoService")]
    public async Task ObterAtivoAsync_QuandoAtivoExiste_DeveRetornarDTO()
    {
        // Arrange
        var ativo = new Ativo(ETipoAtivo.Titulos, "Teste", "TEST");
        var ativoRepository = _mocker.GetMock<IAtivoRepository>();
        ativoRepository
            .Setup(repo => repo.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(ativo);

        var service = new AtivoService(ativoRepository.Object);

        // Act
        var resultado = await service.ObterAtivoAsync(Guid.NewGuid());

        // Assert
        Assert.IsType<AtivoDTO>(resultado);
    }

    [Fact(DisplayName = "ListarAtivosAsync Quando Chamado Deve Retornar Paginação De AtivoDTO")]
    [Trait("Categoria", "AtivoService")]
    public async Task ListarAtivosAsync_QuandoChamado_DeveRetornarPaginacaoDeAtivoDTO()
    {
        // Arrange
        var ativoPaginado = new PagedResult<Ativo>()
        {
            List = new List<Ativo>(),
            PageIndex = 1,
            TotalResults = 1,
            PageSize = 1,
            Query = "Teste"
        };

        var ativoRepository = _mocker.GetMock<IAtivoRepository>();
        ativoRepository
            .Setup(repo => repo.ListarTodosAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(ativoPaginado);

        var service = new AtivoService(ativoRepository.Object);

        // Act
        var resultado = await service.ListarAtivosAsync(1, 2);

        // Assert
        Assert.IsType<PagedResult<AtivoDTO>>(resultado);
    }

    [Fact(DisplayName = "AdicionarAtivoAsync Quando Chamado Deve Retornar Adicionar Ativo")]
    [Trait("Categoria", "AtivoService")]
    public async Task AdicionarAtivoAsync_QuandoChamado_DeveAdicionarAtivo()
    {
        // Arrange
        var unitOfWork = _mocker.GetMock<IUnitOfWork>();
        unitOfWork
            .Setup(u => u.Commit())
            .ReturnsAsync(true);

        var ativoRepository = _mocker.GetMock<IAtivoRepository>();
        ativoRepository
            .Setup(repo => repo.UnitOfWork)
            .Returns(unitOfWork.Object);

        var service = new AtivoService(ativoRepository.Object);

        // Act
        await service.AdicionarAtivoAsync(new AtivoInputModel());

        // Assert
        ativoRepository.Verify(a => a.Add(It.IsAny<Ativo>()), Times.Once);
        ativoRepository.Verify(a => a.UnitOfWork, Times.Once);
        unitOfWork.Verify(u => u.Commit(), Times.Once);
    }

    [Fact(DisplayName = "AdicionarAtivoAsync Quando Falha Persistir Deve Retornar Exceção")]
    [Trait("Categoria", "AtivoService")]
    public async Task AdicionarAtivoAsync_QuandoFalhaPersistir_DeveRetornarExcecao()
    {
        // Arrange
        var unitOfWork = _mocker.GetMock<IUnitOfWork>();
        unitOfWork
            .Setup(u => u.Commit())
            .ReturnsAsync(false);

        var ativoRepository = _mocker.GetMock<IAtivoRepository>();
        ativoRepository
            .Setup(repo => repo.UnitOfWork)
            .Returns(unitOfWork.Object);

        var service = new AtivoService(ativoRepository.Object);

        // Act
        var erro = async() => await service.AdicionarAtivoAsync(new AtivoInputModel());

        // Assert
        var excecao = await Assert.ThrowsAsync<ApplicationException>(erro);
        Assert.Equal("Falha ao persistir transação.", excecao.Message);
    }
}
