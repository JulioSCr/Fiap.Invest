using Microsoft.AspNetCore.Components;

namespace Fiap.Invest.Blazor.WebApp.Extensions;
public static class NavigationExtension
{
    public static void RedirecionarParaLogin(this NavigationManager? navigation)
    {
        if (navigation != null && !navigation.Uri.ToLower().Contains("/login"))
            navigation.NavigateTo("/login", true);
    }

    public static void RedirecionarParaInicio(this NavigationManager navigation)
    {
        if (navigation.Uri != navigation.BaseUri)
            navigation.NavigateTo("/", true);
    }
}