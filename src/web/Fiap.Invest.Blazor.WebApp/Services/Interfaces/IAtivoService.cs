using Fiap.Invest.Blazor.WebApp.DTOs;
using Fiap.Invest.Blazor.WebApp.DTOs.Ativos;

namespace Fiap.Invest.Blazor.WebApp.Services.Interfaces;
public interface IAtivoService : IDisposable
{
    Task<PagedResultDTO<AtivoDTO>?> ListarAsync(int tamanhoPagina, int paginaAtual);
    Task<AtivoDTO?> ObterAsync(Guid ativoId);
}
