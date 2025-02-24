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

    public async Task<Result<Product>> GetProductById(int id)
    {
        var product = await context.Set<Product>().FirstOrDefaultAsync(x => x.Id == id);

        if (product == null)
            return Result.Fail(new ProductNotFoundError());

        return Result.Ok(product);
    }
}
