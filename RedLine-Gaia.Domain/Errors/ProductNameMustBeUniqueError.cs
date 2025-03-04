using System.Diagnostics.CodeAnalysis;
using FluentResults;

namespace RedLine_Gaia.Domain.Errors;

[ExcludeFromCodeCoverage]
public class ProductNameMustBeUniqueError : Error
{
    public ProductNameMustBeUniqueError()
        : base("Product name must be unique.")
    {
        Metadata.Add("Code", 404);
    }
}
