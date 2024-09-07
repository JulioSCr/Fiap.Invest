using Delivery.Core.DomainObjects;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Fiap.Invest.Portfolios.Domain.ValueObjects
{
    public record struct NomePortfolio
    {
        public const int TamanhoMaximo = 20;
        public const int TamanhoMinimo = 5;
        public string Valor { get; private set; }

        public NomePortfolio(string? nomePortfolio)
        {
            var inconsistencias = ObterInconsistencias(nomePortfolio);

            if (!string.IsNullOrWhiteSpace(inconsistencias))
                throw new DomainException(inconsistencias);

            Valor = Formatar(nomePortfolio!);
        }

        public static string? ObterInconsistencias(string? nomePortfolio)
        {
            if (string.IsNullOrWhiteSpace(nomePortfolio?.Trim()))
                return "Deve ser fornecido um nome para o portfólio.";

            if (nomePortfolio.Length > TamanhoMaximo)
                return $"Nome do portfólio deve conter no máximo {TamanhoMaximo} caracteres.";

            if (nomePortfolio.Length < TamanhoMinimo)
                return $"Nome do portfólio deve conter no mínimo {TamanhoMinimo} caracteres.";

            return null;
        }

        public static string Formatar(string nomePortfolio)
        {
            var textInfo = new CultureInfo("pt-BR", false).TextInfo;
            return textInfo.ToTitleCase(Regex.Replace(nomePortfolio.Trim(), @"\s+", " "));
        }
    }
}
