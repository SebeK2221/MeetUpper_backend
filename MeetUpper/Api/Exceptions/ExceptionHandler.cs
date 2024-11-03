using System.Net;
using Domain.BaseException;
using Domain.ErrorResponse;
using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Exceptions;

public class ExceptionHandler:IExceptionHandler
{
    private readonly ILogger<ExceptionHandler> _logger;

    public ExceptionHandler(ILogger<ExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        var response = exception switch
        {
            NotFoundException apiException => new ErrorResponse
            {
                StatusCode = apiException.StatusCode,
                Message = apiException.Message,
            },
            BadRequestException badRequestException => new ErrorResponse()
            {
                StatusCode = badRequestException.StatusCode,
                Message = badRequestException.Message,
            },
            _ => new ErrorResponse()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "Wystąpił nieznany błąd",
            }
        };
        _logger.LogError(exception, exception.Message);
        httpContext.Response.StatusCode = (int)response.StatusCode;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
        
        return true;
    }
}