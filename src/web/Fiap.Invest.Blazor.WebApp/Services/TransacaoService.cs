using Blazored.LocalStorage;
using Fiap.Invest.Blazor.WebApp.DTOs;
using Fiap.Invest.Blazor.WebApp.DTOs.Transacoes;
using Fiap.Invest.Blazor.WebApp.InputModels.Transacoes;
using Fiap.Invest.Blazor.WebApp.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace Fiap.Invest.Blazor.WebApp.Services;
public class TransacaoService : Service, ITransacaoService
{
    private HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;

    public TransacaoService(HttpClient httpClient,
        ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
    }

    public async Task<KeyValuePair<string?, List<SaldoAtivoDTO>?>> ObterSaldoAsync(Guid portfolioId)
    {
        var autenticado = AddAuthorization(ref _httpClient, await ObterToken(_localStorageService));
        if (!autenticado)
            return new KeyValuePair<string?, List<SaldoAtivoDTO>?>(
                "Erro: Sistema indisponível", null);

        var response = await _httpClient.GetAsync($"/api/transacao/saldo/{portfolioId}");

        if (!response.IsSuccessStatusCode)
            return new KeyValuePair<string?, List<SaldoAtivoDTO>?>(
                "Erro: Sistema indisponível", null);

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            return new KeyValuePair<string?, List<SaldoAtivoDTO>?>(
                "Erro: Você não possui transações neste portfólio", null);

        return new KeyValuePair<string?, List<SaldoAtivoDTO>?>(
                null, await DeserializarObjetoResponse<List<SaldoAtivoDTO>>(response));
    }

    public async Task<string?> FazerTransacaoAsync(TransacaoInputModel request)
    {
        var autenticado = AddAuthorization(ref _httpClient, await ObterToken(_localStorageService));
        if (!autenticado) return "Sistema indisponível";

        var response = await _httpClient.PostAsync("/api/transacao", ObterConteudo(request));

        if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            try
            {
                var erro = await DeserializarObjetoResponse<ErroDTO>(response);
                var sb = new StringBuilder();
                if (erro == null) return "Sistema indisponível";
                foreach (var item in erro.Errors.Messages)
                {
                    sb.AppendLine(item);
                }
                var mensagens = sb.ToString();
                if (string.IsNullOrEmpty(mensagens))
                    return "Sistema indisponível";
                else
                    return mensagens;
            }
            catch (Exception)
            {
                return "Sistema indisponível";
            }
        }

        if (response.IsSuccessStatusCode) return null;

        return "Sistema indisponível";
    }

    ~TransacaoService()
    {
        Dispose();
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}
