using Delivery.WebAPI.Core.User;
using Fiap.Invest.Transacoes.Domain.DTOs;
using Fiap.Invest.Transacoes.Domain.Interfaces.Clients;
using Fiap.Invest.Transacoes.Infrastructure.Extensions;
using System.Net;

namespace Fiap.Invest.Transacoes.Infrastructure.Clients
{
    public class PortfolioClient : Client, IPortfolioClient
    {
        private readonly HttpClient _httpClient;
        private readonly IAspNetUser _user;

        public PortfolioClient(HttpClient httpClient, IAppClientsSettings appSettings, IAspNetUser user)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(appSettings.PortfolioUrl);
            _user = user;
        }

        public async Task<List<PortfolioDTO>> ListarPortfolioPorUsuario()
        {
            var response = await _httpClient.GetAsync($"api/Portfolio/Usuario");

            response.EnsureSuccessStatusCode();

            if (response.StatusCode == HttpStatusCode.NoContent)
                return new List<PortfolioDTO>();

            return await DeserializarObjetoResponse<List<PortfolioDTO>>(response) ?? new List<PortfolioDTO>();
        }
    }
}
