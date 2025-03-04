using System.Diagnostics.CodeAnalysis;
using FluentResults;

namespace RedLine_Gaia.Domain.Errors;

[ExcludeFromCodeCoverage]
public class ProductCreateError : Error
{
    public ProductCreateError()
        : base("Issue creating product")
    {
        Metadata.Add("Code", 404);
    }
}
