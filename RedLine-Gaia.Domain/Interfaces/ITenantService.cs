using System;

namespace RedLine_Gaia.Domain.Interfaces;

public interface ITenantService
{
    string GetConnectionString(int tenantId);
}
