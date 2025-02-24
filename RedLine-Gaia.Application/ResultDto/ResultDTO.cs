using System;

namespace RedLine_Gaia.Application.ResultDto;

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

public class ResultDto<T> : ResultDto
{
    public T? Data { get; set; }

    public ResultDto(bool isSuccess, IEnumerable<ErrorDto> errors, T? data)
        : base(isSuccess, errors)
    {
        this.Data = data;
    }
}

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
