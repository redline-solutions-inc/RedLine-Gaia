using Microsoft.EntityFrameworkCore;
using RedLine_Gaia.Domain.Entities;

namespace RedLine_Gaia.Infrastructure.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}

