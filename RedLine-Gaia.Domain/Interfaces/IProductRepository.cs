using FluentResults;
using RedLine_Gaia.Domain.Entities;

namespace RedLine_Gaia.Domain.Interfaces;

public interface IProductRepository
{
    Task<Result<int>> Create(Product entity);
    Task<bool> UpdateProduct(Product entity);
    Task<bool> IsProductNameUnique(Product entity);
    Task<Product?> GetProductById(int id);
    Task<Result<List<Product>>> GetProductList();
}
