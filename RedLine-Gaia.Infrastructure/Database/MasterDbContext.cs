using Microsoft.EntityFrameworkCore;
using RedLine_Gaia.Domain.Entities.Master;

namespace RedLine_Gaia.Infrastructure.Database;

public class MasterDbContext(DbContextOptions<MasterDbContext> options) : DbContext(options)
{
    public DbSet<Tenant> Tenants { get; set; }
}
