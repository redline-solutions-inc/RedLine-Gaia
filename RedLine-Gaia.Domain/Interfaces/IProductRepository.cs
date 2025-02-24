using RedLine_Gaia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedLine_Gaia.Domain.Interfaces;

public interface IProductRepository
{
    Task<int> Create(Product entity);
    Task<Product?> GetProductById(int id);
}
