using Fiap.Invest.Portfolios.Application.DTOs;
using Fiap.Invest.Portfolios.Application.InputModels;
using Fiap.Invest.Portfolios.Domain.Entities;

namespace Fiap.Invest.Portfolios.Application.Services;
public interface IPortfolioService
{
    Task<Portfolio> CriarPortfolioAsync(PortfolioInputModel portfolioInputModel);
    Task<List<PortfolioDTO>> ListarPorUsuarioAsync();
}