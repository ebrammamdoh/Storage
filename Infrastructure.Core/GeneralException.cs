namespace Infrastructure.Core;

public class GeneralException : Exception
{
    public string ErrorCode { get; set; }
    public GeneralException(string errorCode, string message)
            : base(message)
    {
        ErrorCode = errorCode;
    }
}
