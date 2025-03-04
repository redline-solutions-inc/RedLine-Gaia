using System.Diagnostics.CodeAnalysis;
using FluentResults;

namespace RedLine_Gaia.Domain.Errors;

[ExcludeFromCodeCoverage]
public class InternalServiceError : Error
{
    public InternalServiceError()
        : base("Internal Service Error")
    {
        Metadata.Add("Code", 500);
    }
}
