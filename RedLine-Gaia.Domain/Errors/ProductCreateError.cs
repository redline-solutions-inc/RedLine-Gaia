using System;
using FluentResults;

namespace RedLine_Gaia.Domain.Errors;

public class ProductCreateError : Error
{
    public ProductCreateError()
        : base("Issue creating product")
    {
        Metadata.Add("Code", 404);
    }
}
