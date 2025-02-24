using RedLine_Gaia.Domain.Entities;
using RedLine_Gaia.Domain.Interfaces;
using RedLine_Gaia.Infrastructure.Database;


namespace RedLine_Gaia.Infrastructure.Repositories;

internal sealed class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Create(Product product)
    {
        _context.Add<Product>(product);
        var result = await _context.SaveChangesAsync();

        return result;
    }

    public async Task<Product?> GetProductById(int id)
    {
        return await _context.Set<Product>().FindAsync(id);
    }
}
