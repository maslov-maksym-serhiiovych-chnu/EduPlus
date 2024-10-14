using Domain.Exceptions;

namespace Presentation.Middlewares;

public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (TaskNotFoundException exception)
        {
            string message = exception.Message;
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(message);

            logger.LogError(message);
        }
    }
}