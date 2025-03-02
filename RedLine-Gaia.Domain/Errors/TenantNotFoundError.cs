using FluentResults;

namespace RedLine_Gaia.Domain.Errors;

public class TenantNotFoundError : Error
{
    public TenantNotFoundError()
        : base("Tenant not found with supplied Tenant Id")
    {
        Metadata.Add("Code", 404);
    }
}
