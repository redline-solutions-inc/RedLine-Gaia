using FluentResults;

namespace RedLine_Gaia.Domain.Errors;

public class TenantNotInHeaderError : Error
{
    public TenantNotInHeaderError()
        : base("{tenantId} not found in header")
    {
        Metadata.Add("Code", 404);
    }
}
