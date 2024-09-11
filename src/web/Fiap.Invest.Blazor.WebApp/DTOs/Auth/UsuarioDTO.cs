using Fiap.Invest.Blazor.WebApp.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace Fiap.Invest.Blazor.WebApp.DTOs.Auth;
public class UsuarioDTO
{
    public Guid Id { get; set; }
    public string Cpf { get; set; }
    public string Nome { get; set; }

    [JsonIgnore]
    public ResponseResult? ResponseResult { get; set; }

    [JsonConstructor]
    public UsuarioDTO()
    {
        
    }
    public UsuarioDTO(string? token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

        if (jwtToken == null)
            throw new Exception("Falha ao obter token");

        if (TokenValidation(token) == false)
            throw new Exception("Token inválido");

        var id = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;
        var nome = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
        var perfil = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;

        Id = string.IsNullOrWhiteSpace(id)
                ? Guid.Empty
                : Guid.Parse(id);
        Nome = nome ?? string.Empty;
    }

    public UsuarioDTO(HttpResponseMessage response)
    {
        ResponseResult = new ResponseResult
        {
            Status = (int)response.StatusCode,
            Erros = new ResponseErrorMessages { Mensagens = new string[] { "Usuário não está autenticado." } }
        };
    }

    public UsuarioDTO(ResponseResult? responseResult)
    {
        ResponseResult = responseResult;
    }

    public ClaimsPrincipal ObterClaims()
    {
        var claims = new ClaimsPrincipal(new ClaimsIdentity(
            new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                    new Claim(ClaimTypes.Name, Nome),
            }, "AuthCookie"));

        return claims;
    }

    public static bool TokenValidation(string? token)
    {
        if (string.IsNullOrWhiteSpace(token)) return false;

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            if (tokenHandler.ReadToken(token) is not JwtSecurityToken jwtToken)
                return false;

            var dataExpiracao = jwtToken.ValidTo;
            return dataExpiracao.ToUniversalTime() > DateTime.UtcNow;
        }
        catch
        {
            return false;
        }
    }
}
