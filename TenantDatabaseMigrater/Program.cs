using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RedLine_Gaia.Domain.Entities.Master;
using RedLine_Gaia.Infrastructure.Database;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false);

IConfiguration config = builder.Build();

var connectionString = config.GetValue<string>("MasterDBConnection");

DbContextOptions<MasterDbContext> dbContextOptions = CreateDefaultDbContextOptions(
    connectionString
);

try
{
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine($"Applying MasterDbContext Migrations.");
    using var context = new MasterDbContext(dbContextOptions);
    await context.Database.MigrateAsync();
    List<Tenant> tenants = context.Tenants.ToList();
    // Console.ResetColor();

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
        // Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"Applying ApplicationDB Migrations for '{tenant.Name}' tenant.");
        // Console.ResetColor();
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

DbContextOptions<MasterDbContext> CreateDefaultDbContextOptions(string connectionString) =>
    new DbContextOptionsBuilder<MasterDbContext>().UseSqlServer(connectionString).Options;
