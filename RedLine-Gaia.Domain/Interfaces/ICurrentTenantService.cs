namespace RedLine_Gaia.Domain.Interfaces;

/// <summary>
/// Scoped service to contain the requests Tenant information.
/// </summary>
public interface ICurrentTenantService
{
    string? ConnectionString { get; set; }
    int? TenantId { get; set; }
    bool SetTenant(int tenantId);
}
