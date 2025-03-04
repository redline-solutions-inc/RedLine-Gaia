using FluentResults;
using Microsoft.EntityFrameworkCore;
using RedLine_Gaia.Domain.Entities;
using RedLine_Gaia.Domain.Interfaces;
using RedLine_Gaia.Infrastructure.Database;

namespace RedLine_Gaia.Infrastructure.Repositories;

internal sealed class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Product> _dbSet;

    public ProductRepository(IAppDbContextFactory<ApplicationDbContext> appDbContextFactory)
    {
        _context = appDbContextFactory.dbContext();
        _dbSet = _context.Set<Product>();
    }

    public void Add(Product entity)
    {
        _dbSet.Add(entity);
    }

    public async Task AddAsync(Product entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(Product entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Product entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Product?> GetById(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<bool> IsProductNameUnique(Product entity)
    {
        var product = await _dbSet.FirstOrDefaultAsync(x => x.Name == entity.Name);
        return product is null;
    }

    public Task<List<Product>> GetProductList()
    {
        throw new NotImplementedException();
    }
}
