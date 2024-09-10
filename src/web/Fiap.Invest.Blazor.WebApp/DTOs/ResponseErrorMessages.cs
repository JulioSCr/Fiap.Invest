namespace Fiap.Invest.Blazor.WebApp.DTOs;
public class ResponseErrorMessages
{
    public ICollection<string> Mensagens { get; set; }

    public ResponseErrorMessages()
    {
        Mensagens = new List<string>();
    }
}