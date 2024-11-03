using System.Net;

namespace Domain.ErrorResponse;

public class ErrorResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
}