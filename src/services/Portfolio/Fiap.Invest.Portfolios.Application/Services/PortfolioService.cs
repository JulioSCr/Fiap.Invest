using Fiap.Invest.Portfolios.Application.DTOs;
using Fiap.Invest.Portfolios.Application.InputModels;
using Fiap.Invest.Portfolios.Domain.Entities;
using Fiap.Invest.Portfolios.Domain.ValueObjects;
using Fiap.Invest.Portfolios.Domain.Interfaces.Repositories;

namespace Fiap.Invest.Portfolios.Application.Services;

public sealed class PortfolioService : IPortfolioService
{
    private readonly IPortfolioRepository _portfolioRepository;

    public PortfolioService(IPortfolioRepository portfolioRepository)
    {
        _portfolioRepository = portfolioRepository;
    }

    public async Task<Portfolio> CriarPortfolioAsync(PortfolioInputModel portfolioInputModel)
    {
        var nome = new NomePortfolio(portfolioInputModel.Nome);
        var descricao = new DescricaoPortfolio(portfolioInputModel.Descricao);
        var portfolio = new Portfolio(portfolioInputModel.UsuarioId, nome, descricao);

        var portfolios = await _portfolioRepository.ListarPorUsuarioAsync(portfolio.UsuarioId);
        if (portfolios.Any(p => p.Nome == portfolio.Nome))
            throw new ApplicationException($"Portfólio de nome \"{portfolio.Nome}\" já existe.");

        await _portfolioRepository.Add(portfolio);
        return portfolio;
    }

    public async Task<List<PortfolioDTO>> ListarPorUsuarioAsync(Guid usuarioId)
    {
        var portfolios = await _portfolioRepository.ListarPorUsuarioAsync(usuarioId);
        return portfolios
            .Select(p => new PortfolioDTO(p))
            .ToList();
    }
}