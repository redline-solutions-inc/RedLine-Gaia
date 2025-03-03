using System.Diagnostics.CodeAnalysis;
using FluentResults;

namespace RedLine_Gaia.Domain.Errors;

[ExcludeFromCodeCoverage]
public class ProductNotFoundError : Error
{
    public ProductNotFoundError()
        : base("Product not found")
    {
        Metadata.Add("Code", 404);
    }
}
