namespace Fiap.Invest.Blazor.WebApp.DTOs;
public class ErroDTO
{
    public string Title { get; set; }
    public int Status { get; set; }
    public ErroMessageDTO Errors { get; set; }
}

public class ErroMessageDTO
{
    public string[] Messages { get; set; }
}


