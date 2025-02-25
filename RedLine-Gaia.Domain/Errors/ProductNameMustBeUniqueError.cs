using FluentResults;

namespace RedLine_Gaia.Domain.Errors;

public class ProductNameMustBeUniqueError : Error
{
    public ProductNameMustBeUniqueError()
        : base("Product name must be unique.")
    {
        Metadata.Add("Code", 404);
    }
}
