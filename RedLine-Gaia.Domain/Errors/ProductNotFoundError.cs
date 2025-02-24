using FluentResults;

namespace RedLine_Gaia.Domain.Errors;

public class ProductNotFoundError : Error
{
    public ProductNotFoundError()
        : base("Product not found")
    {
        Metadata.Add("Code", 404);
    }
}
