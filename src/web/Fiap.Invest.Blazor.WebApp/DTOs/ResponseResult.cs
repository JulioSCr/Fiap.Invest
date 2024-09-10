using System.Text.Json.Serialization;

namespace Fiap.Invest.Blazor.WebApp.DTOs;
public class ResponseResult
{
    [JsonPropertyName("Title")]
    public string Titulo { get; set; } = string.Empty;

    public int Status { get; set; }

    [JsonPropertyName("Errors")]
    public ResponseErrorMessages Erros { get; set; }

    public ResponseResult()
    {
        Erros = new ResponseErrorMessages();
    }
}
