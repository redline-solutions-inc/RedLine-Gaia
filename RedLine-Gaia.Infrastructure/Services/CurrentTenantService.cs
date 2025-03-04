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
        var foundConnectionString = _tenantService.GetConnectionString(tenantId);

        if (foundConnectionString is null)
            return false;

        TenantId = tenantId;
        ConnectionString = foundConnectionString;

        return true;
    }
}
