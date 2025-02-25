namespace RedLine_Gaia.Application.ResultDto;

/// <summary>
/// A serializable return object wrapper for the FluentResults Result Object.
/// <seealso href="https://github.com/altmann/FluentResults/tree/master/src/FluentResults.Samples/WebController">FluentResults Docs</seealso>
/// </summary>
public class ResultDto
{
    public bool IsSuccess { get; set; }

    public IEnumerable<ErrorDto> Errors { get; set; }

    public ResultDto(bool isSuccess, IEnumerable<ErrorDto> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }
}

/// <summary>
/// A typed serializable return object wrapper for the FluentResults Result Object.
/// </summary>
public class ResultDto<T> : ResultDto
{
    public T? Data { get; set; }

    public ResultDto(bool isSuccess, IEnumerable<ErrorDto> errors, T? data)
        : base(isSuccess, errors)
    {
        this.Data = data;
    }
}

/// <summary>
/// A base error dto for the ResultDto.
/// </summary>
public class ErrorDto
{
    public string Message { get; set; }

    public string Code { get; set; }

    public ErrorDto(string message, string code)
    {
        Message = message;
        Code = code;
    }
}
