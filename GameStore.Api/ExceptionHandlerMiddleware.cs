using System.Net;
using System.Text.Json;

namespace GameStore.Api;

public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
{
    private RequestDelegate Next => next;

    private ILogger Logger => logger;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await Next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        Logger.LogError(ex, "An unexpected error occurred.");

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = "An unexpected error occurred. Please try again later.",
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}