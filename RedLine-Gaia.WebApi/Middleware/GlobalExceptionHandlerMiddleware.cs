using FluentResults;
using FluentValidation;
using RedLine_Gaia.Application.ResultDto;
using RedLine_Gaia.Domain.Errors;
using Serilog.Context;

namespace RedLine_Gaia.WebApi.Middleware;

public class GlobalExceptionHandlerMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionHandlerMiddleware> logger
)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            if (ex is ValidationException validationException)
            {
                var errors = validationException.Errors.Select(x => x.ErrorMessage);
                logger.LogWarning("Validation Exception {@errors}", errors);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 200;
                await context.Response.WriteAsJsonAsync(Result.Fail(errors).ToResultDto());
            }
            else
            {
                logger.LogError("Internal Service Error thrown with exception {ex}", ex);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(
                    Result.Fail(new InternalServiceError()).ToResultDto()
                );
            }

            return;
        }
    }
}
