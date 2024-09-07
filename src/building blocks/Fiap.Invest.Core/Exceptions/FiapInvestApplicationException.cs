namespace Fiap.Invest.Core.Exceptions;
[Serializable]
public class FiapInvestApplicationException : Exception
{
    public FiapInvestApplicationException() { }

    public FiapInvestApplicationException(string message)
        : base(message)
    {
    }

    public FiapInvestApplicationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
