namespace Fiap.Invest.Core.ValueObjects;
public record struct DescricaoPortfolio
{
    public const int TamanhoMaximo = 500;

    public string? Valor { get; private set; }

    public DescricaoPortfolio(string? descricaoPortfolio)
    {
        var inconsistencias = ObterInconsistencias(descricaoPortfolio);

        if (!string.IsNullOrWhiteSpace(inconsistencias))
            throw new Exception(inconsistencias);

        Valor = descricaoPortfolio?.Trim();
    }

    public static string? ObterInconsistencias(string? descricaoPortfolio)
    {
        if (descricaoPortfolio?.Length > TamanhoMaximo)
            return $"Descrição deve conter no máximo {TamanhoMaximo} caracteres.";

        return null;
    }
}
