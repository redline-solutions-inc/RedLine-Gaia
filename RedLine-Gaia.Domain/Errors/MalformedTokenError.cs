using FluentResults;

namespace RedLine_Gaia.Domain.Errors;

public class MalformedTokenError : Error
{
    public MalformedTokenError()
        : base("Malformed token")
    {
        Metadata.Add("Code", 404);
    }
}
