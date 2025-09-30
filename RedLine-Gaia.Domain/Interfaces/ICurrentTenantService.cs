using RedLine_Gaia.Domain.Models;

namespace RedLine_Gaia.Domain.Interfaces;

/// <summary>
/// Scoped service to contain the requests Tenant information.
/// </summary>
public interface ICurrentTenantService
{
    string? ConnectionString { get; set; }
    int? TenantId { get; set; }
    Locale Locale { get; set; }
    Guid? UserId { get; set; }
    bool SetTenant(int tenantId, string? userId, string? locale);
}
