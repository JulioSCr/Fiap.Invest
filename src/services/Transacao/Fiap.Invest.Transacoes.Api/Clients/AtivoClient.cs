using Fiap.Invest.Transacoes.Api.Extensions;
using Fiap.Invest.Transacoes.Domain.DTOs;
using Fiap.Invest.Transacoes.Domain.Interfaces.Clients;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Transacoes.Api.Clients;
[ExcludeFromCodeCoverage]
public class AtivoClient : Client, IAtivoClient
{
    private readonly HttpClient _httpClient;

    public AtivoClient(HttpClient httpClient, IAppClientsSettings appSettings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(appSettings.AtivoUrl);
    }

    public async Task<AtivoDTO?> ObterAtivoPorIdAsync(Guid ativoId)
    {
        var response = await _httpClient.GetAsync($"{ativoId}");

        return await DeserializarObjetoResponse<AtivoDTO>(response);
    }
}
