using FluentResults;
using Microsoft.AspNetCore.Mvc;
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
                context.Result = new BadRequestObjectResult(
                    Result.Fail(new TenantNotInHeaderError()).ToResultDto()
                );
                return;
            }
            else if (!currentTenantService.SetTenant(int.Parse(tenantFromHeader)))
            {
                context.Result = new BadRequestObjectResult(
                    Result.Fail(new TenantNotFoundError()).ToResultDto()
                );
                return;
            }
        }
        catch (Exception ex)
        {
            context.Result = new BadRequestObjectResult(
                Result.Fail(new TenantNotFoundError()).ToResultDto()
            );
            return;
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
