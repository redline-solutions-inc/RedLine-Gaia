using FluentResults;
using Microsoft.AspNetCore.Mvc.Filters;
using RedLine_Gaia.Application.ResultDto;
using RedLine_Gaia.Domain.Errors;
using RedLine_Gaia.Domain.Interfaces;

namespace RedLine_Gaia.WebApi.Middleware;

public class TenantActionFilter(ICurrentTenantService currentTenantService) : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        try
        {
            var request = context.HttpContext.Request;
            request.Headers.TryGetValue("tenantId", out var tenantFromHeader);

            if (string.IsNullOrEmpty(tenantFromHeader))
            {
                SetResponseErrorAsync(context.HttpContext.Response, new TenantNotInHeaderError());
                return;
            }
            else if (!currentTenantService.SetTenant(int.Parse(tenantFromHeader)))
            {
                SetResponseErrorAsync(context.HttpContext.Response, new TenantNotFoundError());
                return;
            }
        }
        catch (Exception ex)
        {
            SetResponseErrorAsync(context.HttpContext.Response, new TenantNotFoundError());
            return;
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }

    private void SetResponseErrorAsync(HttpResponse response, Error error)
    {
        response.ContentType = "application/json";
        response.StatusCode = 404;
        response.WriteAsJsonAsync(Result.Fail(error).ToResultDto());
    }
}
