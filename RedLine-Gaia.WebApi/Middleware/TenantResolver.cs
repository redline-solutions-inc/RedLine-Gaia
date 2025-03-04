using FluentResults;
using RedLine_Gaia.Application.ResultDto;
using RedLine_Gaia.Domain.Errors;
using RedLine_Gaia.Domain.Interfaces;

namespace multiTenantApp.Middleware
{
    /// <summary>
    /// Middleware responsible for setting the scoped Tenante information.
    /// </summary>
    public class TenantResolver
    {
        private readonly RequestDelegate _next;

        public TenantResolver(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            ICurrentTenantService currentTenantService
        )
        {
            try
            {
                context.Request.Headers.TryGetValue("tenantId", out var tenantFromHeader);

                if (string.IsNullOrEmpty(tenantFromHeader))
                {
                    await SetResponseErrorAsync(context.Response, new TenantNotInHeaderError());
                    return;
                }
                else if (!currentTenantService.SetTenant(int.Parse(tenantFromHeader)))
                {
                    await SetResponseErrorAsync(context.Response, new TenantNotFoundError());
                    return;
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                await SetResponseErrorAsync(context.Response, new TenantNotFoundError());
                return;
            }
        }

        private async Task SetResponseErrorAsync(HttpResponse response, Error error)
        {
            response.ContentType = "application/json";
            response.StatusCode = 404;
            await response.WriteAsJsonAsync(Result.Fail(error).ToResultDto());
        }
    }
}
