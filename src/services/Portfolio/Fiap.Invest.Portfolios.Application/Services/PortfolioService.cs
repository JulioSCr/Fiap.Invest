using Delivery.WebAPI.Core.User;
using Fiap.Invest.Core.Exceptions;
using Fiap.Invest.Portfolios.Application.DTOs;
using Fiap.Invest.Portfolios.Application.InputModels;
using Fiap.Invest.Portfolios.Domain.Entities;
using Fiap.Invest.Portfolios.Domain.Interfaces.Repositories;
using Fiap.Invest.Portfolios.Domain.ValueObjects;

namespace Fiap.Invest.Portfolios.Application.Services;

public sealed class PortfolioService : IPortfolioService
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IAspNetUser _user;

    public PortfolioService(IPortfolioRepository portfolioRepository, IAspNetUser aspNetUser)
    {
        _portfolioRepository = portfolioRepository;
        _user = aspNetUser;
    }

    public async Task<Portfolio> CriarPortfolioAsync(PortfolioInputModel portfolioInputModel)
    {
        var nome = new NomePortfolio(portfolioInputModel.Nome);
        var descricao = new DescricaoPortfolio(portfolioInputModel.Descricao);
        var portfolio = new Portfolio(Guid.Parse(_user.Name), nome, descricao);

        var portfolios = await _portfolioRepository.ListarPorUsuarioAsync(portfolio.UsuarioId);
        if (portfolios.Any(p => p.Nome == portfolio.Nome))
            throw new FiapInvestApplicationException($"Portfólio de nome \"{portfolio.Nome}\" já existe.");

        await _portfolioRepository.Add(portfolio);

        if (!await _portfolioRepository.UnitOfWork.Commit())
            throw new FiapInvestApplicationException("Falha ao persistir transação.");

        return portfolio;
    }

    public async Task<List<PortfolioDTO>> ListarPorUsuarioAsync()
    {
        var portfolios = await _portfolioRepository.ListarPorUsuarioAsync(Guid.Parse(_user.Name));
        return portfolios
            .Select(p => new PortfolioDTO(p))
            .ToList();
    }
}