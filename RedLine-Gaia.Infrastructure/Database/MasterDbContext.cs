using Microsoft.EntityFrameworkCore;
using RedLine_Gaia.Domain.Entities.Master;

namespace RedLine_Gaia.Infrastructure.Database;

/// <summary>
/// The DbContext for the Master database that contains all tenante information.
/// </summary>
/// <param name="options"></param>
public class MasterDbContext(DbContextOptions<MasterDbContext> options) : DbContext(options)
{
    public DbSet<Tenant> Tenants { get; set; }
}
