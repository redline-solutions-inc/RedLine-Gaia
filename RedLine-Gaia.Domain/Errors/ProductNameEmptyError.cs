using System.Diagnostics.CodeAnalysis;
using FluentResults;

namespace RedLine_Gaia.Domain.Errors;

[ExcludeFromCodeCoverage]
public class ProductNameEmptyError : Error
{
    public ProductNameEmptyError()
        : base("Product name can not be empty.")
    {
        Metadata.Add("Code", 404);
    }
}
