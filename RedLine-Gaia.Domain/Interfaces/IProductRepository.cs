using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using RedLine_Gaia.Domain.Entities;

namespace RedLine_Gaia.Domain.Interfaces;

public interface IProductRepository
{
    Task<Result<int>> Create(Product entity);
    Task<Result<Product>> GetProductById(int id);
    Task<bool> IsProductNameUnique(Product entity);
}
