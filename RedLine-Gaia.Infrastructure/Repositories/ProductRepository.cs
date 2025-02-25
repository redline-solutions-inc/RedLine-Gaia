using FluentResults;
using Microsoft.EntityFrameworkCore;
using RedLine_Gaia.Domain.Entities;
using RedLine_Gaia.Domain.Errors;
using RedLine_Gaia.Domain.Interfaces;
using RedLine_Gaia.Infrastructure.Database;

namespace RedLine_Gaia.Infrastructure.Repositories;

internal sealed class ProductRepository(ApplicationDbContext context) : IProductRepository
{
    public async Task<Result<int>> Create(Product product)
    {
        try
        {
            context.Add<Product>(product);
            var result = await context.SaveChangesAsync();

            return Result.Ok(product.Id);
        }
        catch (Exception e)
        {
            return Result.Fail(new ProductCreateError());
        }
    }

    public async Task<Product?> GetProductById(int id)
    {
        return await context.Set<Product>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Result<List<Product>>> GetProductList()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsProductNameUnique(Product entity)
    {
        var product = await context.Set<Product>().FirstOrDefaultAsync(x => x.Name == entity.Name);
        return product is null;
    }

    public async Task<bool> UpdateProduct(Product entity)
    {
        throw new NotImplementedException();
    }
}
