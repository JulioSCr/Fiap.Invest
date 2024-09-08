using System.Diagnostics.CodeAnalysis;

namespace Fiap.Invest.Auth.Application.Extensions;
[ExcludeFromCodeCoverage]
public class AppTokenSettings : IAppTokenSettings
{
    public int HorasExpiracaoRefreshToken { get; set; }
}
