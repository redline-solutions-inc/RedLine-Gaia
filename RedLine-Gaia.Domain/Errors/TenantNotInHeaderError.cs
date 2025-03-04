using System.Diagnostics.CodeAnalysis;
using FluentResults;

namespace RedLine_Gaia.Domain.Errors;

[ExcludeFromCodeCoverage]
public class TenantNotInHeaderError : Error
{
    public TenantNotInHeaderError()
        : base("{tenantId} not found in header")
    {
        Metadata.Add("Code", 404);
    }
}
