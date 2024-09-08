using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Fiap.Invest.Auth.Application.Extensions;
public static class AuthExtensions
{
    public static ClaimsIdentity ObterClaims(string usuarioId, ICollection<Claim> claims, IList<string> roles)
    {
        claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, usuarioId));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.UtcNow.ToUnixEpochDate().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64));

        foreach (var roleUsuario in roles)
        {
            claims.Add(new Claim("role", roleUsuario));
        }

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);
        return identityClaims;
    }

    public static string GerarToken(this ClaimsIdentity identityClaims, SigningCredentials credenciais, IHttpContextAccessor accessor)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = $"{accessor.HttpContext?.Request.Scheme}://{accessor.HttpContext?.Request.Host}",
            SigningCredentials = credenciais
        };

        return tokenHandler.CreateEncodedJwt(tokenDescriptor);
    }
}
