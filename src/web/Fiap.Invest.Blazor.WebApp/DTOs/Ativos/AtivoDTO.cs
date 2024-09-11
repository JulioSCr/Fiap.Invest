using System.Text.Json.Serialization;

namespace Fiap.Invest.Blazor.WebApp.DTOs.Ativos;
public class AtivoDTO
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("tipo")]
    public int Tipo { get; set; }

    [JsonPropertyName("nome")]
    public string Nome { get; set; } = string.Empty;

    [JsonPropertyName("codigo")]
    public string Codigo { get; set; } = string.Empty;
}