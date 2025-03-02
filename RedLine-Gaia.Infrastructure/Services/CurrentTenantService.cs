using System;
using RedLine_Gaia.Domain.Interfaces;

namespace RedLine_Gaia.Infrastructure.Services;

public class CurrentTenantService : ICurrentTenantService
{
    private readonly ITenantService _tenantService;
    public int? TenantId { get; set; }
    public string? ConnectionString { get; set; }

    public CurrentTenantService(ITenantService tenantService)
    {
        _tenantService = tenantService;
    }

    public bool SetTenant(int tenantId)
    {
        var connectionsString = _tenantService.GetConnectionString(tenantId);
        if (connectionsString != null)
        {
            TenantId = tenantId;
            ConnectionString = connectionsString;
            return true;
        }
        else
        {
            throw new Exception("Tenant invalid");
        }
    }
}
