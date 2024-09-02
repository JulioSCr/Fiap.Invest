namespace Fiap.Invest.Portfolios.Application.InputModels;

public class PortfolioInputModel
{
    public Guid UsuarioId { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
}