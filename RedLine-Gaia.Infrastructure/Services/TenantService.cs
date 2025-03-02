using Microsoft.Extensions.DependencyInjection;
using RedLine_Gaia.Domain.Entities.Master;
using RedLine_Gaia.Domain.Interfaces;
using RedLine_Gaia.Infrastructure.Database;

namespace RedLine_Gaia.Infrastructure.Services;

public class TenantService(IServiceProvider serviceProvider) : ITenantService
{
    private static Dictionary<int, string> _tenantConnectionStrings = new Dictionary<int, string>();
    private static object LOCK_tenantConnectionStrings = new object();

    private Tenant? GetTenant(int tenantId)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<MasterDbContext>();
            var tenant = context.Tenants.FirstOrDefault(p => p.Id == tenantId);

            return tenant;
        }
    }

    public string GetConnectionString(int tenantId)
    {
        string connectionString = null;

        lock (LOCK_tenantConnectionStrings)
        {
            if (_tenantConnectionStrings.ContainsKey(tenantId))
            {
                connectionString = _tenantConnectionStrings[tenantId];
            }
            else
            {
                var tenant = GetTenant(tenantId);
                if (tenant is not null)
                {
                    _tenantConnectionStrings.Add(tenant.Id, tenant.ConnectionString);
                    connectionString = tenant.ConnectionString;
                }
            }
        }
        return connectionString;
    }
}
