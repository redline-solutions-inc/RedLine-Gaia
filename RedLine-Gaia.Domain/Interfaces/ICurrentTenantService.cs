using System;

namespace RedLine_Gaia.Domain.Interfaces;

public interface ICurrentTenantService
{
    string? ConnectionString { get; set; }
    int? TenantId { get; set; }
    bool SetTenant(int tenantId);
}
