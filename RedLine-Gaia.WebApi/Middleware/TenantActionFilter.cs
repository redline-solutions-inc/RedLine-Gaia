using System.Security.Claims;
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
            var identity = context.HttpContext.User.Identity as ClaimsIdentity;

            if (identity is null)
            {
                context.Result = new BadRequestObjectResult(
                    Result.Fail(new MalformedTokenError()).ToResultDto()
                );
                return;
            }

            IEnumerable<Claim> claims = identity.Claims;
            var businessId = claims
                .Where(x => x.Type == ClaimTypes.GroupSid)
                .FirstOrDefault()
                ?.Value;

            var userId = claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
            var locale = claims.Where(x => x.Type == ClaimTypes.Locality).FirstOrDefault()?.Value;

            if (string.IsNullOrEmpty(businessId) || string.IsNullOrEmpty(userId))
            {
                context.Result = new BadRequestObjectResult(
                    Result.Fail(new MalformedTokenError()).ToResultDto()
                );
                return;
            }
            else if (!currentTenantService.SetTenant(int.Parse(businessId), userId, locale))
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
