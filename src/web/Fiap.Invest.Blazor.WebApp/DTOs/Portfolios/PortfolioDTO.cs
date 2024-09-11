namespace Fiap.Invest.Blazor.WebApp.DTOs.Portfolios;
public record PortfolioDTO
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public string Nome { get; set; }
    public string? Descricao { get; set; }
}