using Fiap.Invest.Transacoes.Application.DTOs;
using Fiap.Invest.Transacoes.Application.InputModels;
using Fiap.Invest.Transacoes.Domain.Entities;
using Fiap.Invest.Transacoes.Domain.Enums;
using Fiap.Invest.Transacoes.Domain.Interfaces.Clients;
using Fiap.Invest.Transacoes.Domain.Interfaces.Repositories;
using System.Linq;

namespace Fiap.Invest.Transacoes.Application.Services;
public class TransacaoService : ITransacaoService
{
    private readonly ITransacaoRepository _transacaoRepository;
    private readonly IPortfolioClient _portfolioClient;
    private readonly IAtivoClient _ativoClient;

    public TransacaoService(ITransacaoRepository transacaoRepository, IPortfolioClient portfolioClient, IAtivoClient ativoClient)
    {
        _transacaoRepository = transacaoRepository;
        _portfolioClient = portfolioClient;
        _ativoClient = ativoClient;
    }

    public async Task<Transacao> FazerTransacaoAsync(TransacaoInputModel model)
    {
        var transacao = new Transacao(model.PortfolioId, model.AtivoId, model.Tipo, model.Quantidade, model.Preco);

        var portfolios = await _portfolioClient.ListarPortfolioPorUsuario();

        if (portfolios.TrueForAll(p => p.Id != model.PortfolioId))
            throw new ApplicationException("Portfólio não encontrado");

        var ativo = await _ativoClient.ObterAtivoPorIdAsync(model.AtivoId);

        if (ativo is null)
            throw new ApplicationException("Ativo inexistente");

        if (model.Tipo == ETipoTransacao.Venda)
        {
            var saldos = await ObterSaldoAtivoPorPortfolioAsync(model.PortfolioId);
            var saldoAtivo = saldos.FirstOrDefault(s => s.AtivoId == model.AtivoId);

            if (saldoAtivo?.Quantidade < model.Quantidade)
                throw new ApplicationException("Quantidade de ativos no portfólio insuficiente para venda");
        }

        await _transacaoRepository.Add(transacao);
        if (!await _transacaoRepository.UnitOfWork.Commit())
            throw new ApplicationException("Falha ao persistir transação.");

        return transacao;
    }

    public async Task<List<SaldoAtivoDTO>> ObterSaldoAtivoPorPortfolioAsync(Guid portfolioId)
    {
        var transacoes = await _transacaoRepository.ListarPorPortfolioAsync(portfolioId);

        var ativos = transacoes.GroupBy(t => t.AtivoId);

        var saldos = new List<SaldoAtivoDTO>();
        foreach (var ativo in ativos)
        {
            var compra = ativo.Where(a => a.Tipo == ETipoTransacao.Compra).ToList();
            var venda = ativo.Where(a => a.Tipo == ETipoTransacao.Venda).ToList();

            saldos.Add(new SaldoAtivoDTO
            {
                AtivoId = ativo.Key,
                Quantidade = compra.Sum(a => a.Quantidade) - venda.Sum(a => a.Quantidade)
            });
        }

        return saldos;
    }
}
