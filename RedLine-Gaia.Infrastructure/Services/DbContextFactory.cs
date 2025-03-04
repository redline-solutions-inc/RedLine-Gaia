using Microsoft.EntityFrameworkCore;
using RedLine_Gaia.Domain.Interfaces;
using RedLine_Gaia.Infrastructure.Database;

namespace RedLine_Gaia.Infrastructure.Services;

public class DbContextFactory : IAppDbContextFactory<ApplicationDbContext>
{
    ICurrentTenantService _currentTenantService;
    ApplicationDbContext _appDbContext;

    public DbContextFactory(ICurrentTenantService currentTenantService)
    {
        _currentTenantService = currentTenantService;
        var connectionString = _currentTenantService.ConnectionString;
        DbContextOptions<ApplicationDbContext> dbContextOptions =
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;

        _appDbContext = new ApplicationDbContext(dbContextOptions);
    }

    public ApplicationDbContext dbContext()
    {
        return _appDbContext;
    }
}
