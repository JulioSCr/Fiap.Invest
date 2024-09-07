using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Transacoes.Api.Extensions;
[ExcludeFromCodeCoverage]
public class AppClientsSettings : IAppClientsSettings
{
    public string AtivoUrl { get; set; } = string.Empty;
    public string PortfolioUrl { get; set; } = string.Empty;
}
