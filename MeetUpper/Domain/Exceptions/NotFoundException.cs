using System.Net;
using Domain.BaseException;

namespace Domain.Exceptions;

public class NotFoundException: ApiException
{
    public string ErrorMessage { get; set; }
    public NotFoundException(string message) : base(message, HttpStatusCode.NotFound)
    {
        ErrorMessage = message;
    }
}