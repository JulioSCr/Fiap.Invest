using Fiap.Invest.Transacoes.Api.Extensions;
using Fiap.Invest.Transacoes.Domain.DTOs;
using Fiap.Invest.Transacoes.Domain.Interfaces.Clients;
using System.Net;

namespace Fiap.Invest.Transacoes.Api.Clients
{
    public class PortfolioClient : Client, IPortfolioClient
    {
        private readonly HttpClient _httpClient;

        public PortfolioClient(HttpClient httpClient, IAppClientsSettings appSettings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(appSettings.PortfolioUrl);
        }

        public async Task<List<PortfolioDTO>> ListarPortfolioPorUsuario()
        {
            var response = await _httpClient.GetAsync("");

            response.EnsureSuccessStatusCode();

            if (response.StatusCode == HttpStatusCode.NoContent)
                return new List<PortfolioDTO>();

            return await DeserializarObjetoResponse<List<PortfolioDTO>>(response) ?? new List<PortfolioDTO>();
        }
    }
}
