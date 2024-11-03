using System.Net;
using Domain.BaseException;

namespace Domain.Exceptions;

public class BadRequestException:ApiException
{
    
    public string ErrorMessage { get; set; }
    public BadRequestException(string message) : base(message, HttpStatusCode.BadRequest)
    {
        ErrorMessage = message;
    }
}