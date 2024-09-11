using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;
using Blazored.LocalStorage;
using System.Net.Http;

namespace Fiap.Invest.Blazor.WebApp.Services;
public abstract class Service
{
    protected StringContent ObterConteudo(object dado)
    {
        return new StringContent(
            JsonSerializer.Serialize(dado),
            Encoding.UTF8,
            "application/json");
    }

    protected async Task<T?> DeserializarObjetoResponse<T>(HttpResponseMessage responseMessage)
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
    }

    protected bool TratarErrosResponse(HttpResponseMessage response)
    {
        switch ((int)response.StatusCode)
        {
            case 401:
            case 403:
            case 404:
            case 500:
                throw new HttpRequestException($"Falha ao fazer requisição: {response.RequestMessage?.RequestUri}", new Exception(response.Content?.ToString()), statusCode: response.StatusCode);
            case 400:
                return false;
        }

        response.EnsureSuccessStatusCode();
        return true;
    }

    protected async Task<string?> ObterToken(ILocalStorageService localStorageService)
    {
        return await localStorageService.GetItemAsStringAsync("__jwt");
    }

    protected bool AddAuthorization(ref HttpClient httpClient, string? token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return false;

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return true;
    }
}