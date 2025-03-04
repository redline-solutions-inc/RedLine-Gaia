using RedLine_Gaia.Domain.Entities;

namespace RedLine_Gaia.Domain.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<bool> IsProductNameUnique(Product entity);
    Task<List<Product>> GetProductList();
}
