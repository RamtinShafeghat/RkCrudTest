using Mc2.CrudTest.Application.Exceptions;
using Serilog;
using System.Net;
using System.Text.Json;

namespace Mc2.CrudTest.Api.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await ConvertException(context, ex);
        }
    }

    private Task ConvertException(HttpContext context, Exception exception)
    {
        var (code, errors) = GetErrors(exception);

        Log.Error(JsonSerializer.Serialize(errors));

        var response = JsonSerializer.Serialize(new
        {
            Code = code,
            Result = errors,
        });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(response);
    }

    private static (HttpStatusCode code, object errors) GetErrors(Exception exception)
    {
        return exception switch
        {
            ValidationException validationException => (HttpStatusCode.BadRequest, validationException.ValidationErrors),
            BadRequestException badRequestException => (HttpStatusCode.BadRequest, badRequestException.Message),
            NotFoundException _ => (HttpStatusCode.NotFound, exception.Message),
            _ => (HttpStatusCode.InternalServerError, exception.Message)
        };
    }
}
