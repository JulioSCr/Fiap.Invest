using Fiap.Invest.Transacoes.Domain.Entities;
using Fiap.Invest.Transacoes.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Invest.Transacoes.Application.InputModels;
public class TransacaoInputModel
{
    [Required(ErrorMessage = "PortfolioId é obrigatório")]
    public Guid PortfolioId { get; set; }

    [Required(ErrorMessage = "AtivoId é obrigatório")]
    public Guid AtivoId { get; set; }

    [Required(ErrorMessage = "Tipo é obrigatório")]
    public ETipoTransacao Tipo { get; set; }

    [Required(ErrorMessage = "Quantidade é obrigatório")]
    [Range(Transacao.QuantidadeMinima, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que {1}")]
    public int Quantidade { get; set; }

    [Required(ErrorMessage = "Preco é obrigatório")]
    [Range(Transacao.PrecoMinimoDataAnnotation, double.MaxValue, ErrorMessage = "O preço deve ser maior que {1}")]
    public decimal Preco { get; set; }
}
