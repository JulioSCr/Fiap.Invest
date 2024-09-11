using System.Text.Json.Serialization;

namespace Fiap.Invest.Blazor.WebApp.DTOs;
public class PagedResultDTO<T> where T : class
{
    [JsonPropertyName("list")]
    public IEnumerable<T> List { get; set; }

    [JsonPropertyName("totalResults")]
    public int TotalResults { get; set; }

    [JsonPropertyName("pageIndex")]
    public int PageIndex { get; set; }

    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }

    [JsonPropertyName("query")]
    public string? Query { get; set; }
}