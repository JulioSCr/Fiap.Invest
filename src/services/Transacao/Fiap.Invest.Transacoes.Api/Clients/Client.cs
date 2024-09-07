using Delivery.Core.Communication;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Fiap.Invest.Transacoes.Api.Clients;
[ExcludeFromCodeCoverage]
public abstract class Client
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
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
    }

    protected bool TratarErrosResponse(HttpResponseMessage responseMessage)
    {
        if (responseMessage.StatusCode == HttpStatusCode.BadRequest) return false;

        responseMessage.EnsureSuccessStatusCode();
        return true;
    }

    protected ResponseResult RetornoOk()
    {
        return new ResponseResult();
    }
}

