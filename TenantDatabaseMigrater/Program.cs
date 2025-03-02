using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RedLine_Gaia.Domain.Entities.Master;
using RedLine_Gaia.Infrastructure.Database;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false);

IConfiguration config = builder.Build();

var masterDbConnectionString = config.GetValue<string>("MasterDBConnection");

DbContextOptions<MasterDbContext> masterDbContextOptions = CreateMasterDbContextOptions(
    masterDbConnectionString
);

try
{
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine($"Applying MasterDbContext Migrations.");

    using var masterDbContext = new MasterDbContext(masterDbContextOptions);
    await masterDbContext.Database.MigrateAsync();

    Console.WriteLine($"Building multi-tenant connections.");

    List<Tenant> tenants = masterDbContext.Tenants.ToList();
    IEnumerable<Task> tasks = tenants.Select(t => MigrateTenantDatabase(t));
    await Task.WhenAll(tasks);
}
catch (Exception ex)
{
    throw;
}

async Task MigrateTenantDatabase(Tenant tenant)
{
    DbContextOptions<ApplicationDbContext> dbContextOptions = CreateTenantDbContextOptions(
        tenant.ConnectionString
    );
    try
    {
        Console.WriteLine($"Applying Migrations for tenant: '{tenant.Name}'.");
        using var context = new ApplicationDbContext(dbContextOptions);
        await context.Database.MigrateAsync();
    }
    catch (Exception e)
    {
        throw;
    }
}

DbContextOptions<ApplicationDbContext> CreateTenantDbContextOptions(string connectionString) =>
    new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(connectionString).Options;

DbContextOptions<MasterDbContext> CreateMasterDbContextOptions(string connectionString) =>
    new DbContextOptionsBuilder<MasterDbContext>().UseSqlServer(connectionString).Options;
