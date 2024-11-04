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
            NotFoundException notFoundException => new ErrorResponse
            {
                StatusCode = notFoundException.StatusCode,
                Message = notFoundException.Message,
            },
            BadRequestException badRequestException => new ErrorResponse()
            {
                StatusCode = badRequestException.StatusCode,
                Message = badRequestException.Message,
            },
            InvalidCredentialsException invalidCredentialsException => new ErrorResponse()
            {
                StatusCode = invalidCredentialsException.StatusCode,
                Message = invalidCredentialsException.Message,
            },
            ConflictException conflictException => new ErrorResponse()
            {
                StatusCode = conflictException.StatusCode,
                Message = conflictException.Message,
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