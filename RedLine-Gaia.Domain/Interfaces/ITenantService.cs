using System;

namespace RedLine_Gaia.Domain.Interfaces;

/// <summary>
/// Singleton service that manages the multi-tanant connection strings.
/// </summary>
public interface ITenantService
{
    /// <summary>
    /// Returns the MSSQL connection string for the supplied TenantId.
    /// </summary>
    /// <param name="tenantId">TenantId</param>
    /// <returns></returns>
    string? GetConnectionString(int tenantId);
}
