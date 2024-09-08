using Delivery.Core.DomainObjects;
using System.Text.RegularExpressions;

namespace Fiap.Invest.Core.ValueObjects;
public record Nome
{
    private const int TamanhoMaximo = 60;
    private const int TamanhoMinimoPrimeiro = 2;
    private const int TamanhoMinimoSegundo = 3;

    public string Primeiro { get; private set; }
    public string Sobrenome { get; private set; }
    public string Completo { get; private set; }

    public Nome(string? nomeCompleto)
    {
        if (string.IsNullOrWhiteSpace(nomeCompleto))
            throw new DomainException("Nome não informado");

        Completo = Formatar(nomeCompleto);

        if (string.IsNullOrWhiteSpace(ObterInconsistencias(Completo)))
            throw new DomainException("Nome inválido");

        var partes = Completo.Split(' ');
        Primeiro = partes[0];
        Sobrenome = string.Join(' ', partes[1..]);
    }

    public static string? ObterInconsistencias(string? nomeCompleto)
    {
        if (string.IsNullOrWhiteSpace(nomeCompleto))
            return "Nome em branco";

        nomeCompleto = Formatar(nomeCompleto.Trim());

        if (nomeCompleto.Length > TamanhoMaximo)
            return $"Nome com mais de {TamanhoMaximo} caracteres";

        var partes = nomeCompleto.Trim().Split(' ');

        if (partes.Length < 2)
            return "Nome sem sobrenome";

        var primeiroNome = partes[0];
        var sobrenome = string.Join(' ', partes[1..]);

        var regexNome = new Regex(@"^[A-Za-zÀ-ÖØ-öø-ÿ]+$");
        var regexSobrenome = new Regex(@"^[A-Za-zÀ-ÖØ-öø-ÿ]+(?:\s[A-Za-zÀ-ÖØ-öø-ÿ]+)*$");

        if (primeiroNome.Length < TamanhoMinimoPrimeiro)
        {
            return $"Primeiro nome com menos de {TamanhoMinimoPrimeiro} caracteres";
        }

        if (!regexNome.IsMatch(primeiroNome))
        {
            return "Primeiro nome com caracteres inválidos";
        }

        if (sobrenome.Length < TamanhoMinimoSegundo)
        {
            return $"Sobrenome nome com menos de {TamanhoMinimoSegundo} caracteres";
        }

        if (!regexSobrenome.IsMatch(sobrenome))
        {
            return "Sobrenome com caracteres inválidos";
        }

        return null;
    }

    public static string Formatar(string nomeCompleto)
    {
        nomeCompleto = Regex.Replace(nomeCompleto.Trim(), @"\s+", " ");

        nomeCompleto = Regex.Replace(nomeCompleto, @"\b\w", m => m.Value.ToUpper());

        return nomeCompleto;
    }
}
