using FluentResults;
using FluentValidation;
using RedLine_Gaia.Application.ResultDto;
using RedLine_Gaia.Domain.Errors;

namespace RedLine_Gaia.WebApi.Middleware;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            // Add logger logic here to log thrown exception.

            if (ex is ValidationException validationException)
            {
                var errors = validationException.Errors.Select(x => x.ErrorMessage);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 200;
                await context.Response.WriteAsJsonAsync(Result.Fail(errors).ToResultDto());
            }
            else
            {
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
