using System.Net;

namespace Domain.BaseException;

public abstract class ApiException : Exception
{
    public HttpStatusCode StatusCode { get; }

    protected ApiException(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}