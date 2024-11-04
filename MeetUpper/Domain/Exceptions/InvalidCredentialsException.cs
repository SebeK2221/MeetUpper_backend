using System.Net;
using Domain.BaseException;

namespace Domain.Exceptions;

public class InvalidCredentialsException:ApiException
{
    public string ErrorMessage { get; set; }
    public InvalidCredentialsException(string message) : base(message, HttpStatusCode.Unauthorized)
    {
        ErrorMessage = message;
    }
}