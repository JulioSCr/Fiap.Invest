using Fiap.Invest.Blazor.WebApp.DTOs.Portfolios;
using Fiap.Invest.Blazor.WebApp.InputModels.Portfolios;

namespace Fiap.Invest.Blazor.WebApp.Services.Interfaces;
public interface IPortfolioService : IDisposable
{
    Task<List<PortfolioDTO>?> ListarAsync();
    Task<bool> CadastrarAsync(PortfolioInputModel request);
}
