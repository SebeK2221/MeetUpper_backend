using System.Net;
using Domain.BaseException;

namespace Domain.Exceptions;

public class ConflictException:ApiException
{
    public string ErrorMessage { get; set; }

    public ConflictException(string message) : base(message, HttpStatusCode.Conflict)
    {
        ErrorMessage = message;
    }
}