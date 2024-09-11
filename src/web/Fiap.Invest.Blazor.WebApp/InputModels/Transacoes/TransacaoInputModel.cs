using Fiap.Invest.Blazor.WebApp.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fiap.Invest.Blazor.WebApp.InputModels.Transacoes;
public class TransacaoInputModel
{
    [JsonIgnore]
    public const int QuantidadeMinima = 1;
    [JsonIgnore]
    public const decimal PrecoMinimo = 0.1M;
    [JsonIgnore]
    public const double PrecoMinimoDataAnnotation = 0.1D;

    [Required(ErrorMessage = "Portfólio é obrigatório")]
    [GuidNotEmpty(ErrorMessage = "Informe um portfólio")]
    public Guid PortfolioId { get; set; }

    [Required(ErrorMessage = "Ativo é obrigatório")]
    [GuidNotEmpty(ErrorMessage = "Informe um ativo")]
    public Guid AtivoId { get; set; }

    [Required(ErrorMessage = "Tipo é obrigatório")]
    public ETipoTransacao Tipo { get; set; }

    [Required(ErrorMessage = "Quantidade é obrigatório")]
    [Range(QuantidadeMinima, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que {1}")]
    public int Quantidade { get; set; }

    [Required(ErrorMessage = "Preco é obrigatório")]
    [Range(PrecoMinimoDataAnnotation, double.MaxValue, ErrorMessage = "O preço deve ser maior que {1}")]
    public decimal Preco { get; set; }
}
public enum ETipoTransacao
{
    Compra,
    Venda
}